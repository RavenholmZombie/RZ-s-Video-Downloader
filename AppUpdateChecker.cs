using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RZVD
{
    internal class AppUpdateChecker
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private const string DefaultUpdateUrl =
            "https://raw.githubusercontent.com/RavenholmZombie/RavenholmZombie/refs/heads/main/rzvd_ver.json";

        public string UpdateInfoUrl { get; set; } = DefaultUpdateUrl;

        // Parsed Data from remote JSON.
        public Version CurrentVersion { get; private set; }
        public Version RemoteVersion { get; private set; }
        public string DownloadUrl { get; private set; }
        public string Changelog { get; private set; }

        public bool HasChecked { get; private set; }
        public bool IsUpdateAvailable => HasChecked && RemoteVersion != null && RemoteVersion > CurrentVersion;

        public AppUpdateChecker(string currentVersionString)
        {
            // Strip git build metadata from app version to keep things clean.
            currentVersionString = currentVersionString.Split('+')[0];
            CurrentVersion = new Version(currentVersionString);
        }

        public async Task CheckAsync()
        {
            string json = await _httpClient.GetStringAsync(UpdateInfoUrl);
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            // --- VERSION ---
            string remoteVersionString = root.GetProperty("version").GetString();
            if (string.IsNullOrWhiteSpace(remoteVersionString)) throw new InvalidOperationException("Update JSON missing 'version' field.");

            remoteVersionString = remoteVersionString.Split('+')[0];
            RemoteVersion = new Version(remoteVersionString);

            // --- DOWNLOAD URL ---
            if (root.TryGetProperty("downloadUrl", out JsonElement nodeDownload))
                DownloadUrl = nodeDownload.GetString();
            else
                DownloadUrl = null;

            // --- CHANGELOG / NOTES ---
            if (root.TryGetProperty("notes", out JsonElement nodeNotes))
                Changelog = nodeNotes.GetString();
            else
                Changelog = null;

            HasChecked = true;
        }

        // --------------------------------------------------------
        //  Actions the form can call
        // --------------------------------------------------------

        public void OpenDownloadPage()
        {
            if (string.IsNullOrWhiteSpace(DownloadUrl)) throw new InvalidOperationException("Update JSON missing 'downloadUrl' field.");

            // TODO: There's probably a cleaner way of doing this.
            Process.Start("explorer.exe", DownloadUrl);
        }

        public void ShowChangelog(IWin32Window owner)
        {
            if (string.IsNullOrWhiteSpace(Changelog)) throw new InvalidOperationException("Update JSON missing 'notes' field.");

            // TODO: Works for now, might move to a dedicated form later idk.
            MessageBox.Show(
                owner,
                $"Changelog for RZ's Video Downloader {RemoteVersion}\n\n{Changelog}",
                "Changelog",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
