using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeDownloader
{
    public partial class frmAbout : Form
    {
        private const string UpdateInfoUrl = "https://raw.githubusercontent.com/RavenholmZombie/RavenholmZombie/refs/heads/main/rzvd_ver.json";

        public frmAbout()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            gBoxUpdate.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                            "This app uses the following third-party utilities:\n\n" +
                            "- FFMPEG: Software in the Public Interest (SPI)\n" +
                            "- YT-DLP: YT-DLP GitHub Team",
                            "Credits",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
        }

        private async void btnCheckForUpdates_ClickAsync(object sender, EventArgs e)
        {
            Height = 407;
            gBoxUpdate.Show();
            btnDownload.Hide();
            lblUpdateStatus.Text = "Launching update check request...";
            btnCheckForUpdates.Enabled = false;
            ControlBox = false;

            try
            {
                await CheckForUpdatesAsync();
            }
            catch
            {
                lblUpdateStatus.Text = "Unable to check for updates.";
            }
            finally
            {
                btnCheckForUpdates.Enabled = true;
                ControlBox = true;
            }
        }

        private async Task CheckForUpdatesAsync()
        {
            HttpClient _httpClient = new HttpClient();
            string json = await _httpClient.GetStringAsync(UpdateInfoUrl);

            using JsonDocument doc = JsonDocument.Parse(json);
            string remoteVersionString = doc.RootElement.GetProperty("version").GetString();

            if (string.IsNullOrWhiteSpace(remoteVersionString))
                throw new InvalidOperationException("Update JSON missing 'version' field.");

            Version currentVersion = new Version(Application.ProductVersion);
            Version remoteVersion = new Version(remoteVersionString);

            if (remoteVersion <= currentVersion)
            {
                lblUpdateStatus.Text = "You are running the latest version. \n\nYour Version: " + currentVersion + "\nLatest Version: " + remoteVersion;
            }
            else
            {
                lblUpdateStatus.Text = "A new version is available. \n\nYour Version: " + currentVersion + "\nLatest Version: " + remoteVersion;
                btnDownload.Show();
                btnDownload.Enabled = true;
                btnDownload.BringToFront();
            }
        }
    }
}
