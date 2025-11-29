using RZVD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private readonly AppUpdateChecker _updateChecker;
        public frmAbout()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            _updateChecker = new AppUpdateChecker(Application.ProductVersion);
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
            await TriggerUpdateCheckAsync();
        }

        public async void RemoteUpdateTrigger()
        {
            try
            {
                await RunUpdateCheckAsync();
            }
            catch(Exception ex)
            {
                lblUpdateStatus.Text = "Unable to check for updates.\n" + ex;
            }
            finally
            {
                btnCheckForUpdates.Enabled = true;
                ControlBox = true;
            }
        }

        public async Task TriggerUpdateCheckAsync()
        {
            // Set up UI state before starting the check
            Height = 407;
            gBoxUpdate.Show();
            btnDownload.Hide();
            lnkLblChangelog.Hide();
            lblUpdateStatus.Text = "Launching update check request...";
            btnCheckForUpdates.Enabled = false;
            ControlBox = false;

            try
            {
                await RunUpdateCheckAsync();
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = "Unable to check for updates.\n" + ex;
            }
            finally
            {
                btnCheckForUpdates.Enabled = true;
                ControlBox = true;
            }
        }

        private async Task RunUpdateCheckAsync()
        {
            await _updateChecker.CheckAsync();

            var current = _updateChecker.CurrentVersion;
            var remote = _updateChecker.RemoteVersion;

            if (!_updateChecker.IsUpdateAvailable)
            {
                lblUpdateStatus.Text =
                    "You are running the latest version.\n\n" +
                    "Your Version: " + current + "\n" +
                    "Latest Version: " + remote;
            }
            else
            {
                lblUpdateStatus.Text =
                    "A new version is available.\n\n" +
                    "Your Version: " + current + "\n" +
                    "Latest Version: " + remote;

                btnDownload.Show();
                btnDownload.Enabled = true;
                btnDownload.BringToFront();

                lnkLblChangelog.Show();
                lnkLblChangelog.BringToFront();
            }
        }

        private async void btnDownload_ClickAsync(object sender, EventArgs e)
        {
            _updateChecker.OpenDownloadPage();
            RZVD.Properties.Settings.Default.Save();
            Application.Exit();
        }

        private async void lnkLblChangelog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _updateChecker.ShowChangelog(this);
        }
    }
}