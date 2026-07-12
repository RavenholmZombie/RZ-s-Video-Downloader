using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RZVD
{
    public partial class frmConverter : Form
    {
        private readonly string _ffmpegPath;

        private string _inputPath = string.Empty;
        private string _outputPath = string.Empty;

        private bool _hasAudio;
        private bool _hasVideo;
        private bool _isConverting;
        public frmConverter(string ffmpegPath)
        {
            InitializeComponent();

            _ffmpegPath = ffmpegPath;
        }

        private void frmConverter_Load(object sender, EventArgs e)
        {
            lblInputStatus.Text = "No input file selected.";
            btnConvert.Enabled = false;

            SetOutputFormatsEnabled(false);
        }

        private async void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Title = "Choose Media File",
                Filter =
                "Media Files|*.mp4;*.mkv;*.webm;*.avi;*.mov;*.wmv;*.flv;" +
                "*.mp3;*.wav;*.ogg;*.opus;*.m4a;*.aac;*.flac|" +
                "All Files (*.*)|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            _inputPath = dialog.FileName;
            txtInput.Text = _inputPath;

            _outputPath = string.Empty;
            txtDestination.Clear();

            ClearOutputSelection();

            lblInputStatus.Text = "Checking file...";

            btnBrowseInput.Enabled = false;
            btnConvert.Enabled = false;

            try
            {
                MediaProbeResult result =
                    await ProbeMediaAsync(_inputPath);

                _hasAudio = result.HasAudio;
                _hasVideo = result.HasVideo;

                string format =
                    Path.GetExtension(_inputPath)
                        .TrimStart('.')
                        .ToUpperInvariant();

                if (!_hasAudio && !_hasVideo)
                {
                    lblInputStatus.Text =
                        $"File is an {format} and cannot be converted.";

                    SetOutputFormatsEnabled(false);

                    return;
                }

                lblInputStatus.Text =
                    $"File is an {format} and can be converted.";

                ConfigureOutputFormats();
            }
            catch (Exception ex)
            {
                _hasAudio = false;
                _hasVideo = false;

                SetOutputFormatsEnabled(false);

                string format =
                    Path.GetExtension(_inputPath)
                        .TrimStart('.')
                        .ToUpperInvariant();

                if (string.IsNullOrWhiteSpace(format))
                    format = "unknown format";

                lblInputStatus.Text =
                    $"File is an {format} and cannot be converted.";

                AppendLog(
                    "[ERROR] Media probe failed." +
                    Environment.NewLine);

                AppendLog(
                    ex.Message +
                    Environment.NewLine);
            }
            finally
            {
                btnBrowseInput.Enabled = true;

                UpdateConvertButton();
            }
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            OutputFormat? selectedFormat =
            GetSelectedOutputFormat();

            if (selectedFormat is null)
            {
                MessageBox.Show(
                    "Choose an output format first.",
                    "Output Format Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Choose Conversion Destination",
                Filter = selectedFormat.Value.FileFilter,
                DefaultExt = selectedFormat.Value.Extension,
                AddExtension = true,
                OverwritePrompt = true
            };

            if (!string.IsNullOrWhiteSpace(_inputPath))
            {
                dialog.InitialDirectory =
                    Path.GetDirectoryName(_inputPath);

                dialog.FileName =
                    Path.GetFileNameWithoutExtension(_inputPath);
            }

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            _outputPath = dialog.FileName;
            txtDestination.Text = _outputPath;

            UpdateConvertButton();
        }

        private void OutputFormat_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is not RadioButton radioButton ||
                !radioButton.Checked)
            {
                return;
            }

            /*
             * The existing destination may have the extension for a
             * previously selected format. Clear it so the user explicitly
             * chooses the destination for the new output format.
             */
            _outputPath = string.Empty;
            txtDestination.Clear();

            UpdateConvertButton();
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (_isConverting)
                return;

            OutputFormat? format =
                GetSelectedOutputFormat();

            if (format is null)
                return;

            if (string.IsNullOrWhiteSpace(_inputPath) ||
                !File.Exists(_inputPath))
            {
                MessageBox.Show(
                    "The selected input file could not be found.",
                    "Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(_outputPath))
            {
                MessageBox.Show(
                    "Choose an output destination.",
                    "Destination Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (string.Equals(
                    Path.GetFullPath(_inputPath),
                    Path.GetFullPath(_outputPath),
                    StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "The input and output files cannot be the same file.",
                    "Conversion Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            _isConverting = true;

            SetConversionControlsEnabled(false);

            rtbLog.Clear();

            try
            {
                AppendLog(
                    "[INFO] Starting conversion..." +
                    Environment.NewLine);

                AppendLog(
                    $"[INFO] Input: {_inputPath}" +
                    Environment.NewLine);

                AppendLog(
                    $"[INFO] Output: {_outputPath}" +
                    Environment.NewLine);

                AppendLog(
                    $"[INFO] Format: {format.Value.DisplayName}" +
                    Environment.NewLine +
                    Environment.NewLine);

                await ConvertMediaAsync(
                    format.Value);

                AppendLog(
                    Environment.NewLine +
                    "[INFO] Conversion complete." +
                    Environment.NewLine);

                MessageBox.Show(
                    "The media file was converted successfully.",
                    "Conversion Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AppendLog(
                    Environment.NewLine +
                    "[ERROR] Conversion failed." +
                    Environment.NewLine);

                AppendLog(
                    "[ERROR] " +
                    ex.Message +
                    Environment.NewLine);

                MessageBox.Show(
                    $"The media file could not be converted.\n\n{ex.Message}",
                    "Conversion Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _isConverting = false;

                SetConversionControlsEnabled(true);

                UpdateConvertButton();
            }
        }

        private async Task<MediaProbeResult> ProbeMediaAsync(string inputPath)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _ffmpegPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            startInfo.ArgumentList.Add("-hide_banner");
            startInfo.ArgumentList.Add("-i");
            startInfo.ArgumentList.Add(inputPath);

            using var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();

            string standardOutput =
            await process.StandardOutput.ReadToEndAsync();

            string standardError =
                await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();

            string output =
                standardOutput +
                Environment.NewLine +
                standardError;

            bool hasAudio = Regex.IsMatch(
                output,
                @"Stream\s+#.*:\s+Audio:",
                RegexOptions.IgnoreCase);

            bool hasVideo = Regex.IsMatch(
                output,
                @"Stream\s+#.*:\s+Video:",
                RegexOptions.IgnoreCase);

            return new MediaProbeResult(
                hasAudio,
                hasVideo);
        }

        private async Task ConvertMediaAsync(OutputFormat format)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = _ffmpegPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            startInfo.ArgumentList.Add("-hide_banner");

            startInfo.ArgumentList.Add("-y");

            startInfo.ArgumentList.Add("-i");
            startInfo.ArgumentList.Add(_inputPath);

            foreach (string argument in format.FfmpegArguments)
            {
                startInfo.ArgumentList.Add(argument);
            }

            startInfo.ArgumentList.Add(_outputPath);

            using var process = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            process.OutputDataReceived += (_, eventArgs) =>
            {
                if (!string.IsNullOrWhiteSpace(eventArgs.Data))
                {
                    AppendLogThreadSafe(
                        eventArgs.Data +
                        Environment.NewLine);
                }
            };

            process.ErrorDataReceived += (_, eventArgs) =>
            {
                if (!string.IsNullOrWhiteSpace(eventArgs.Data))
                {
                    AppendLogThreadSafe(
                        eventArgs.Data +
                        Environment.NewLine);
                }
            };

            if (!process.Start())
            {
                throw new InvalidOperationException(
                    "FFmpeg could not be started.");
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException(
                    $"FFmpeg exited with code {process.ExitCode}.");
            }

            if (!File.Exists(_outputPath))
            {
                throw new FileNotFoundException(
                    "FFmpeg completed but the output file was not created.",
                    _outputPath);
            }
        }

        private void ConfigureOutputFormats()
        {
            /*
         * Audio can be extracted from either an audio file or a video
         * containing an audio stream.
         */
            rbMp3.Enabled = _hasAudio;
            rbWav.Enabled = _hasAudio;
            rbOpus.Enabled = _hasAudio;
            rbOggVorbis.Enabled = _hasAudio;

            /*
             * Video output requires an actual video stream.
             */
            rbMp4.Enabled = _hasVideo;
            rbMkv.Enabled = _hasVideo;
            rbWebM.Enabled = _hasVideo;
            rbWmv.Enabled = _hasVideo;

            ClearOutputSelection();
        }

        private void SetOutputFormatsEnabled(bool enabled)
        {
            rbMp3.Enabled = enabled;
            rbWav.Enabled = enabled;
            rbOpus.Enabled = enabled;
            rbOggVorbis.Enabled = enabled;

            rbMp4.Enabled = enabled;
            rbMkv.Enabled = enabled;
            rbWebM.Enabled = enabled;
            rbWmv.Enabled = enabled;
        }

        private void ClearOutputSelection()
        {
            rbMp3.Checked = false;
            rbWav.Checked = false;
            rbOpus.Checked = false;
            rbOggVorbis.Checked = false;

            rbMp4.Checked = false;
            rbMkv.Checked = false;
            rbWebM.Checked = false;
            rbWmv.Checked = false;
        }

        private OutputFormat? GetSelectedOutputFormat()
        {
            if (rbMp3.Checked)
            {
                return new OutputFormat(
                    "MP3",
                    "mp3",
                    "MP3 Audio (*.mp3)|*.mp3",
                    ["-vn", "-c:a", "libmp3lame", "-q:a", "2"]);
            }

            if (rbWav.Checked)
            {
                return new OutputFormat(
                    "WAV",
                    "wav",
                    "WAV Audio (*.wav)|*.wav",
                    ["-vn", "-c:a", "pcm_s16le"]);
            }

            if (rbOpus.Checked)
            {
                return new OutputFormat(
                    "Opus",
                    "opus",
                    "Opus Audio (*.opus)|*.opus",
                    ["-vn", "-c:a", "libopus", "-b:a", "192k"]);
            }

            if (rbOggVorbis.Checked)
            {
                return new OutputFormat(
                    "Ogg Vorbis",
                    "ogg",
                    "Ogg Vorbis Audio (*.ogg)|*.ogg",
                    ["-vn", "-c:a", "libvorbis", "-q:a", "6"]);
            }

            if (rbMp4.Checked)
            {
                return new OutputFormat(
                    "MP4",
                    "mp4",
                    "MP4 Video (*.mp4)|*.mp4",
                    [
                        "-c:v", "libx264",
                    "-preset", "medium",
                    "-crf", "23",
                    "-c:a", "aac",
                    "-b:a", "192k"
                    ]);
            }

            if (rbMkv.Checked)
            {
                return new OutputFormat(
                    "MKV",
                    "mkv",
                    "Matroska Video (*.mkv)|*.mkv",
                    [
                        "-c:v", "libx264",
                    "-preset", "medium",
                    "-crf", "23",
                    "-c:a", "aac",
                    "-b:a", "192k"
                    ]);
            }

            if (rbWebM.Checked)
            {
                return new OutputFormat(
                    "WebM",
                    "webm",
                    "WebM Video (*.webm)|*.webm",
                    [
                        "-c:v", "libvpx-vp9",
                    "-crf", "31",
                    "-b:v", "0",
                    "-c:a", "libopus",
                    "-b:a", "192k"
                    ]);
            }

            if (rbWmv.Checked)
            {
                return new OutputFormat(
                    "WMV",
                    "wmv",
                    "Windows Media Video (*.wmv)|*.wmv",
                    [
                        "-c:v", "wmv2",
                        "-q:v", "3",
                        "-c:a", "wmav2",
                        "-b:a", "192k"
                    ]);
            }

            return null;
        }

        private void UpdateConvertButton()
        {
            btnConvert.Enabled =
                !_isConverting &&
                (_hasAudio || _hasVideo) &&
                GetSelectedOutputFormat() is not null &&
                !string.IsNullOrWhiteSpace(_inputPath) &&
                !string.IsNullOrWhiteSpace(_outputPath);
        }

        private void SetConversionControlsEnabled(bool enabled)
        {
            btnBrowseInput.Enabled = enabled;
            btnBrowseDestination.Enabled = enabled;

            tabFormats.Enabled = enabled;

            btnConvert.Enabled = enabled;
        }

        private void AppendLog(string text)
        {
            rtbLog.AppendText(text);

            rtbLog.SelectionStart =
                rtbLog.TextLength;

            rtbLog.ScrollToCaret();
        }

        private void AppendLogThreadSafe(string text)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.BeginInvoke(
                    new Action<string>(
                        AppendLogThreadSafe),
                    text);

                return;
            }

            AppendLog(text);
        }

        private readonly record struct MediaProbeResult(
        bool HasAudio,
        bool HasVideo);

        private readonly record struct OutputFormat(
            string DisplayName,
            string Extension,
            string FileFilter,
            string[] FfmpegArguments);
    }
}
