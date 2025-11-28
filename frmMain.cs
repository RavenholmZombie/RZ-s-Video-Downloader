using System.Diagnostics;
using System.Media;
using System.Reflection;
using System.Text;

namespace YouTubeDownloader
{
    public partial class frmMain : Form
    {
        private readonly string _ytDlpPath;
        private readonly string _ffmpegPath;
        private int brotato = 0;
        public frmMain()
        {
            InitializeComponent();

            ToolExtractor.Log = AppendToConsole;
            _ytDlpPath = ToolExtractor.EnsureYtDlp();
            _ffmpegPath = ToolExtractor.EnsureFfmpeg();

            Environment.SetEnvironmentVariable("FFMPEG_LOCATION", Path.GetDirectoryName(_ffmpegPath));

        }

        internal static class ToolExtractor
        {
            // Hook this from Form1: ToolExtractor.Log = AppendToConsole;
            public static Action<string> Log { get; set; } = _ => { };

            // Change these to your actual namespace/resource names:
            private const string YtDlpResourceName = "YouTubeDownloader.ToolsRaw.yt-dlp.exe";
            private const string FfmpegResourceName = "YouTubeDownloader.ToolsRaw.ffmpeg.exe";

            public static string EnsureYtDlp()
            {
                string toolsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools");
                Directory.CreateDirectory(toolsDir);

                string targetPath = Path.Combine(toolsDir, "yt-dlp.exe");

                Log($"[Tools] Checking yt-dlp at: {targetPath}{Environment.NewLine}");
                ExtractIfNeeded(YtDlpResourceName, targetPath);

                return targetPath;
            }

            public static string EnsureFfmpeg()
            {
                string toolsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools");
                Directory.CreateDirectory(toolsDir);

                string targetPath = Path.Combine(toolsDir, "ffmpeg.exe");

                Log($"[Tools] Checking ffmpeg at: {targetPath}{Environment.NewLine}");
                ExtractIfNeeded(FfmpegResourceName, targetPath);

                return targetPath;
            }

            private static void ExtractIfNeeded(string resourceName, string targetPath)
            {
                if (File.Exists(targetPath) && new FileInfo(targetPath).Length > 0)
                {
                    Log($"[Tools] Found existing {Path.GetFileName(targetPath)}; skipping extraction.{Environment.NewLine}");
                    return;
                }

                Log($"[Tools] Extracting {Path.GetFileName(targetPath)} from resources...{Environment.NewLine}");

                using (Stream resStream = Assembly.GetExecutingAssembly()
                                                  .GetManifestResourceStream(resourceName)
                       ?? throw new InvalidOperationException($"Could not find resource {resourceName}"))
                using (FileStream fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write))
                {
                    resStream.CopyTo(fileStream);
                }

                Log($"[Tools] Finished extracting {Path.GetFileName(targetPath)}.{Environment.NewLine}");
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            AppendToConsole("[INFO] Video Downloader " + Application.ProductVersion.Split('+')[0] + " by RavenholmZombie \n");
            AppendToConsole("[INFO] READY \n");

            if (!string.IsNullOrEmpty(RZVD.Properties.Settings.Default.prevDLLocation))
            {
                AppendToConsole("[INFO] Previous Download Location Found and Set \n");
                txtPath.Text = RZVD.Properties.Settings.Default.prevDLLocation;
            }
            rbMp4.Checked = true;
        }

        public void AppendToConsole(string text)
        {
            if (rtbConsole.InvokeRequired)
            {
                rtbConsole.Invoke(new Action<string>(AppendToConsole), text);
                return;
            }

            rtbConsole.AppendText(text);
            rtbConsole.ScrollToCaret();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select download folder";

                if (!string.IsNullOrWhiteSpace(txtPath.Text) &&
                    Directory.Exists(txtPath.Text))
                {
                    dlg.SelectedPath = txtPath.Text;
                }
                else
                {
                    dlg.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                }

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtPath.Text = dlg.SelectedPath;
                    RZVD.Properties.Settings.Default.prevDLLocation = dlg.SelectedPath;
                    RZVD.Properties.Settings.Default.Save();
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter a valid URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                AppendToConsole("[ERROR] Unable to start download: no valid url provided. \n");
                brotato++;

                if(brotato == 3)
                {
                    AppendToConsole("[ERROR] Brotato chip, you need to actually put in a URL 💀🥀 \n");
                }
                if (brotato == 6)
                {
                    Opacity = 0;
                    MessageBox.Show("ight that's it, imma head out", "Dude, stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    RZVD.Properties.Settings.Default.Save();
                    Application.Exit();
                }

                return;
            }

            string outputFolder = txtPath.Text.Trim();
            if (string.IsNullOrEmpty(outputFolder)) outputFolder = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create/access download directory:\n" + ex.Message, "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToConsole("[ERROR] Process failed:"+ ex.Message +"\n");
                return;
            }

            btnStart.Enabled = false;
            btnBrowse.Enabled = false;
            AppendToConsole($"[INFO] Starting download for: {url}{Environment.NewLine}");
            AppendToConsole($"[INFO] Output folder: {outputFolder}{Environment.NewLine}{Environment.NewLine}");

            try
            {
                var (isAudioOnly, format) = GetSelectedFormat();
                if (format == null)
                {
                    MessageBox.Show("Please select an output format.");
                    return;
                }

                string args = BuildYtDlpArguments(url, isAudioOnly, format, outputFolder);

                AppendToConsole($"[CMD] yt-dlp {args}{Environment.NewLine}{Environment.NewLine}");

                await RunProcessAsync(_ytDlpPath, args);

                AppendToConsole($"{Environment.NewLine}[INFO] Download finished.{Environment.NewLine}");
                AppendToConsole("[INFO] READY \n");
                PlaySoundFromResources(RZVD.Properties.Resources.done);
            }
            catch (Exception ex)
            {
                AppendToConsole($"{Environment.NewLine}[ERROR] {ex.Message}{Environment.NewLine}");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PlaySoundFromResources(RZVD.Properties.Resources.moo);
                AppendToConsole("[ERROR] Process failed:" + ex.Message + "\n");
            }
            finally
            {
                btnStart.Enabled = true;
                btnBrowse.Enabled = true;
            }
        }

        public void PlaySoundFromResources(Stream stream)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(stream);
                player.PlaySync();
            }
            catch (Exception ex)
            {
                AppendToConsole($"{Environment.NewLine}[SP-ERROR] {ex.Message}{Environment.NewLine}");
            }
        }

        private (bool isAudioOnly, string format) GetSelectedFormat()
        {
            // Audio-only
            if (rbMP3.Checked) return (true, "mp3");
            if (rbWAV.Checked) return (true, "wav");
            if (rbOggVorbis.Checked) return (true, "vorbis"); // yt-dlp uses "vorbis"
            if (rbOpus.Checked) return (true, "opus");

            // Video
            if (rbMp4.Checked) return (false, "mp4");
            if (rbMKV.Checked) return (false, "mkv");
            if (rbWebM.Checked) return (false, "webm");
            if (rbWMV.Checked) return (false, "wmv");

            return (false, null);
        }

        private string BuildYtDlpArguments(string url, bool isAudioOnly, string format, string outputFolder)
        {
            var sb = new StringBuilder();

            // Output template: <folder>\%(title)s.%(ext)s
            string template = Path.Combine(outputFolder, "%(title)s.%(ext)s");
            sb.Append($"-o \"{template}\" ");

            // Make sure yt-dlp uses our ffmpeg
            sb.Append($"--ffmpeg-location \"{Path.GetDirectoryName(_ffmpegPath)}\" ");

            if (isAudioOnly)
            {
                sb.Append("-x ");                        // extract audio
                sb.Append($"--audio-format {format} ");  // mp3, wav, vorbis, opus...
                sb.Append("-f bestaudio ");              // best audio quality
            }
            else
            {
                // best video+audio, merge
                sb.Append("-f \"bv*+ba/b\" ");
                // recode to chosen container
                sb.Append($"--recode-video {format} ");
            }

            sb.Append($"\"{url}\"");

            return sb.ToString();
        }

        private async Task RunProcessAsync(string fileName, string arguments)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            };

            using (var process = new Process { StartInfo = psi, EnableRaisingEvents = true })
            {
                process.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        AppendToConsole(e.Data + Environment.NewLine);
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                        AppendToConsole(e.Data + Environment.NewLine);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await Task.Run(() => process.WaitForExit());
            }
        }

        private void rtbConsole_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            rtbConsole.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Known Supported Platforms:\n\n" +
                "- YouTube\n" +
                "- YouTube Shorts\n" +
                "- TikTok\n\n" +
                "This application utilizes FFMpeg and yt-dlp for media downloading and conversion. " +
                "Therefore, any website supported by these tools should also be compatible.",
                "Supported Platforms",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RZVD.Properties.Settings.Default.Save();
            Application.Exit();
        }

        private void verifyDependenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppendToConsole("[INFO] Verifying embedded dependencies... \n");
            ToolExtractor.EnsureYtDlp();
            ToolExtractor.EnsureFfmpeg();
        }

        private void aboutRZsVideoDownloaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();
            frmAbout.ShowDialog(this);
        }
    }
}
