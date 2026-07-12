using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RZVD
{
    internal static class DownloadHistoryManager
    {
        private static readonly string HistoryDirectory =
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
            "RZ's Video Downloader");

        private static readonly string HistoryPath =
            Path.Combine(
                HistoryDirectory,
                "download-history.json");

        private static readonly JsonSerializerOptions JsonOptions =
            new()
            {
                WriteIndented = true
            };

        public static async Task<List<DownloadHistoryEntry>> LoadAsync()
        {
            if (!File.Exists(HistoryPath))
                return new List<DownloadHistoryEntry>();

            try
            {
                await using FileStream stream = File.OpenRead(HistoryPath);

                return await JsonSerializer
                    .DeserializeAsync<List<DownloadHistoryEntry>>(
                        stream,
                        JsonOptions)
                    ?? new List<DownloadHistoryEntry>();
            }
            catch
            {
                return new List<DownloadHistoryEntry>();
            }
        }

        public static async Task AddAsync(
        string url,
        string title)
        {
            List<DownloadHistoryEntry> history =
                await LoadAsync();

            history.Add(new DownloadHistoryEntry
            {
                DownloadedAt = DateTime.Now,
                Title = title,
                Url = url,
                Source = GetSourceName(url)
            });

            await SaveAsync(history);
        }

        public static async Task ClearAsync()
        {
            await SaveAsync(
                new List<DownloadHistoryEntry>());
        }

        private static async Task SaveAsync(
            List<DownloadHistoryEntry> history)
        {
            Directory.CreateDirectory(HistoryDirectory);

            string temporaryPath = HistoryPath + ".tmp";

            try
            {
                await using (var stream = new FileStream(
                    temporaryPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    await JsonSerializer.SerializeAsync(
                        stream,
                        history,
                        JsonOptions);

                    await stream.FlushAsync();
                }

                File.Move(
                    temporaryPath,
                    HistoryPath,
                    overwrite: true);
            }
            finally
            {
                try
                {
                    if (File.Exists(temporaryPath))
                        File.Delete(temporaryPath);
                }
                catch
                {
                    // Cleanup failure is nonfatal.
                }
            }
        }

        private static string GetSourceName(string url)
        {
            if (!Uri.TryCreate(
                    url,
                    UriKind.Absolute,
                    out Uri? uri))
            {
                return "Unknown";
            }

            string host = uri.Host
                .ToLowerInvariant();

            if (host.StartsWith("www."))
                host = host[4..];

            return host switch
            {
                "youtube.com" => "YouTube",
                "youtu.be" => "YouTube",
                "tiktok.com" => "TikTok",
                "vimeo.com" => "Vimeo",
                "twitter.com" => "Twitter",
                "x.com" => "X",
                "facebook.com" => "Facebook",
                "instagram.com" => "Instagram",
                _ => host
            };
        }
    }
}
