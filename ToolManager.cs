using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RZVD;

internal sealed class ToolManager
{
    private const string YtDlpDownloadUrl =
        "https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe";

    private const string FfmpegDownloadUrl =
        "https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip";

    private readonly HttpClient _httpClient;
    private readonly string _toolsDirectory;
    private readonly string _metadataPath;
    private readonly Action<string> _log;

    private ToolMetadata _metadata = new();

    public string YtDlpPath =>
        Path.Combine(_toolsDirectory, "yt-dlp.exe");

    public string FfmpegPath =>
        Path.Combine(_toolsDirectory, "ffmpeg.exe");

    public ToolManager(Action<string> log)
    {
        _log = log;

        _toolsDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Tools");

        _metadataPath = Path.Combine(
            _toolsDirectory,
            "tool-metadata.json");

        _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression =
                DecompressionMethods.GZip |
                DecompressionMethods.Deflate,
            AllowAutoRedirect = true
        });

        _httpClient.Timeout = TimeSpan.FromMinutes(10);

        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
            "RZ-Video-Downloader/" +
            Application.ProductVersion.Split('+')[0]);
    }

    public async Task EnsureToolsAsync(
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_toolsDirectory);

        await LoadMetadataAsync(cancellationToken);

        _log("[Tools] Checking yt-dlp for updates..." +
             Environment.NewLine);

        await UpdateYtDlpAsync(cancellationToken);

        _log("[Tools] Checking FFmpeg for updates..." +
             Environment.NewLine);

        await UpdateFfmpegAsync(cancellationToken);

        await SaveMetadataAsync(cancellationToken);

        ValidateTools();

        _log("[Tools] All dependencies are ready." +
             Environment.NewLine);
    }

    private async Task UpdateYtDlpAsync(
        CancellationToken cancellationToken)
    {
        string temporaryPath = Path.Combine(
            _toolsDirectory,
            $"yt-dlp-{Guid.NewGuid():N}.tmp");

        try
        {
            DownloadResult result = await DownloadConditionallyAsync(
                YtDlpDownloadUrl,
                temporaryPath,
                _metadata.YtDlp,
                cancellationToken);

            if (result.NotModified && FileIsValid(YtDlpPath))
            {
                _log("[Tools] yt-dlp is already current." +
                     Environment.NewLine);

                return;
            }

            if (result.NotModified)
            {
                // The metadata says it is current, but the executable
                // is missing or damaged. Download it without conditions.
                result = await DownloadUnconditionallyAsync(
                    YtDlpDownloadUrl,
                    temporaryPath,
                    cancellationToken);
            }

            ReplaceFile(temporaryPath, YtDlpPath);

            _metadata.YtDlp = result.Metadata;

            _log("[Tools] yt-dlp was installed or updated." +
                 Environment.NewLine);
        }
        finally
        {
            DeleteFileQuietly(temporaryPath);
        }
    }

    private async Task UpdateFfmpegAsync(
        CancellationToken cancellationToken)
    {
        string zipPath = Path.Combine(
            _toolsDirectory,
            $"ffmpeg-{Guid.NewGuid():N}.zip");

        string extractionDirectory = Path.Combine(
            _toolsDirectory,
            $"ffmpeg-extract-{Guid.NewGuid():N}");

        try
        {
            DownloadResult result = await DownloadConditionallyAsync(
                FfmpegDownloadUrl,
                zipPath,
                _metadata.Ffmpeg,
                cancellationToken);

            if (result.NotModified && FileIsValid(FfmpegPath))
            {
                _log("[Tools] FFmpeg is already current." +
                     Environment.NewLine);

                return;
            }

            if (result.NotModified)
            {
                result = await DownloadUnconditionallyAsync(
                    FfmpegDownloadUrl,
                    zipPath,
                    cancellationToken);
            }

            Directory.CreateDirectory(extractionDirectory);

            ZipFile.ExtractToDirectory(
                zipPath,
                extractionDirectory,
                overwriteFiles: true);

            string? extractedFfmpeg = Directory
                .EnumerateFiles(
                    extractionDirectory,
                    "ffmpeg.exe",
                    SearchOption.AllDirectories)
                .FirstOrDefault();

            if (extractedFfmpeg is null)
            {
                throw new InvalidDataException(
                    "The downloaded FFmpeg archive did not contain ffmpeg.exe.");
            }

            string temporaryExe = Path.Combine(
                _toolsDirectory,
                $"ffmpeg-{Guid.NewGuid():N}.tmp");

            File.Copy(extractedFfmpeg, temporaryExe, overwrite: true);

            try
            {
                ReplaceFile(temporaryExe, FfmpegPath);
            }
            finally
            {
                DeleteFileQuietly(temporaryExe);
            }

            _metadata.Ffmpeg = result.Metadata;

            _log("[Tools] FFmpeg was installed or updated." +
                 Environment.NewLine);
        }
        finally
        {
            DeleteFileQuietly(zipPath);
            DeleteDirectoryQuietly(extractionDirectory);
        }
    }

    private async Task<DownloadResult> DownloadConditionallyAsync(
        string url,
        string destinationPath,
        RemoteFileMetadata existingMetadata,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            url);

        if (!string.IsNullOrWhiteSpace(existingMetadata.ETag))
        {
            request.Headers.IfNoneMatch.Add(
                EntityTagHeaderValue.Parse(existingMetadata.ETag));
        }

        if (existingMetadata.LastModifiedUtc.HasValue)
        {
            request.Headers.IfModifiedSince =
                existingMetadata.LastModifiedUtc.Value;
        }

        using HttpResponseMessage response =
            await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotModified)
        {
            return new DownloadResult(
                NotModified: true,
                Metadata: existingMetadata);
        }

        response.EnsureSuccessStatusCode();

        await WriteResponseToFileAsync(
            response,
            destinationPath,
            cancellationToken);

        return new DownloadResult(
            NotModified: false,
            Metadata: CreateMetadata(response));
    }

    private async Task<DownloadResult> DownloadUnconditionallyAsync(
        string url,
        string destinationPath,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            url);

        using HttpResponseMessage response =
            await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

        response.EnsureSuccessStatusCode();

        await WriteResponseToFileAsync(
            response,
            destinationPath,
            cancellationToken);

        return new DownloadResult(
            NotModified: false,
            Metadata: CreateMetadata(response));
    }

    private async Task WriteResponseToFileAsync(
        HttpResponseMessage response,
        string destinationPath,
        CancellationToken cancellationToken)
    {
        long? totalBytes =
            response.Content.Headers.ContentLength;

        await using Stream source =
            await response.Content.ReadAsStreamAsync(
                cancellationToken);

        await using var destination = new FileStream(
            destinationPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 81920,
            useAsync: true);

        byte[] buffer = new byte[81920];
        long downloadedBytes = 0;
        int lastReportedPercentage = -1;

        while (true)
        {
            int bytesRead = await source.ReadAsync(
                buffer.AsMemory(0, buffer.Length),
                cancellationToken);

            if (bytesRead == 0)
                break;

            await destination.WriteAsync(
                buffer.AsMemory(0, bytesRead),
                cancellationToken);

            downloadedBytes += bytesRead;

            if (totalBytes is > 0)
            {
                int percentage = (int)(
                    downloadedBytes * 100L / totalBytes.Value);

                // Avoid flooding the console with progress lines.
                if (percentage >= lastReportedPercentage + 10)
                {
                    lastReportedPercentage = percentage;

                    _log(
                        $"[Tools] Downloading: {percentage}% " +
                        $"({FormatBytes(downloadedBytes)} / " +
                        $"{FormatBytes(totalBytes.Value)})" +
                        Environment.NewLine);
                }
            }
        }

        await destination.FlushAsync(cancellationToken);

        if (destination.Length == 0)
        {
            throw new InvalidDataException(
                "The server returned an empty download.");
        }
    }

    private static RemoteFileMetadata CreateMetadata(
        HttpResponseMessage response)
    {
        return new RemoteFileMetadata
        {
            ETag = response.Headers.ETag?.ToString(),
            LastModifiedUtc = response.Content.Headers.LastModified
        };
    }

    private static void ReplaceFile(
        string temporaryPath,
        string destinationPath)
    {
        if (!FileIsValid(temporaryPath))
        {
            throw new InvalidDataException(
                $"The downloaded file {Path.GetFileName(temporaryPath)} " +
                "is missing or empty.");
        }

        string backupPath = destinationPath + ".old";

        DeleteFileQuietly(backupPath);

        if (File.Exists(destinationPath))
        {
            File.Move(
                destinationPath,
                backupPath,
                overwrite: true);
        }

        try
        {
            File.Move(
                temporaryPath,
                destinationPath,
                overwrite: true);

            DeleteFileQuietly(backupPath);
        }
        catch
        {
            DeleteFileQuietly(destinationPath);

            if (File.Exists(backupPath))
            {
                File.Move(
                    backupPath,
                    destinationPath,
                    overwrite: true);
            }

            throw;
        }
    }

    private void ValidateTools()
    {
        if (!FileIsValid(YtDlpPath))
        {
            throw new FileNotFoundException(
                "yt-dlp.exe is unavailable.",
                YtDlpPath);
        }

        if (!FileIsValid(FfmpegPath))
        {
            throw new FileNotFoundException(
                "ffmpeg.exe is unavailable.",
                FfmpegPath);
        }
    }

    private async Task LoadMetadataAsync(
        CancellationToken cancellationToken)
    {
        if (!File.Exists(_metadataPath))
        {
            _metadata = new ToolMetadata();
            return;
        }

        try
        {
            await using FileStream stream = File.OpenRead(_metadataPath);

            _metadata =
                await JsonSerializer.DeserializeAsync<ToolMetadata>(
                    stream,
                    cancellationToken: cancellationToken)
                ?? new ToolMetadata();
        }
        catch (Exception ex)
        {
            _log(
                "[Tools] Could not read tool update metadata. " +
                "A fresh check will be performed. " +
                ex.Message +
                Environment.NewLine);

            _metadata = new ToolMetadata();
        }
    }

    private async Task SaveMetadataAsync(
        CancellationToken cancellationToken)
    {
        string temporaryPath = _metadataPath + ".tmp";

        try
        {
            await using var stream = new FileStream(
                temporaryPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            await JsonSerializer.SerializeAsync(
                stream,
                _metadata,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                },
                cancellationToken);

            await stream.FlushAsync(cancellationToken);

            File.Move(
                temporaryPath,
                _metadataPath,
                overwrite: true);
        }
        finally
        {
            DeleteFileQuietly(temporaryPath);
        }
    }

    private static bool FileIsValid(string path)
    {
        return File.Exists(path) &&
               new FileInfo(path).Length > 0;
    }

    private static void DeleteFileQuietly(string path)
    {
        try
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        catch
        {
            // Cleanup failures are nonfatal.
        }
    }

    private static void DeleteDirectoryQuietly(string path)
    {
        try
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive: true);
        }
        catch
        {
            // Cleanup failures are nonfatal.
        }
    }

    private static string FormatBytes(long bytes)
    {
        string[] units = ["B", "KB", "MB", "GB"];
        double value = bytes;
        int unitIndex = 0;

        while (value >= 1024 && unitIndex < units.Length - 1)
        {
            value /= 1024;
            unitIndex++;
        }

        return $"{value:0.##} {units[unitIndex]}";
    }

    private sealed class ToolMetadata
    {
        public RemoteFileMetadata YtDlp { get; set; } = new();
        public RemoteFileMetadata Ffmpeg { get; set; } = new();
    }

    private sealed class RemoteFileMetadata
    {
        public string? ETag { get; set; }
        public DateTimeOffset? LastModifiedUtc { get; set; }
    }

    private readonly record struct DownloadResult(
        bool NotModified,
        RemoteFileMetadata Metadata);
}