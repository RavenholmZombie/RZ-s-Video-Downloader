using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouTubeDownloader;

namespace RZVD
{
    public partial class frmHistory : Form
    {
        private readonly frmMain _mainForm;
        public frmHistory(frmMain mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private async void frmHistory_Load(object sender, EventArgs e)
        {
            await LoadHistoryAsync();
        }

        private async Task LoadHistoryAsync()
        {
            dgvHistory.Rows.Clear();

            List<DownloadHistoryEntry> history =
                await DownloadHistoryManager.LoadAsync();

            foreach (DownloadHistoryEntry entry in history
                         .OrderByDescending(x => x.DownloadedAt))
            {
                dgvHistory.Rows.Add(
                    entry.DownloadedAt.ToString(
                        "MM/dd/yyyy h:mm tt"),
                    entry.Title,
                    entry.Url,
                    entry.Source,
                    "Download Again");
            }
        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string columnName =
                dgvHistory.Columns[e.ColumnIndex].Name;

            string? url = dgvHistory
                .Rows[e.RowIndex]
                .Cells["colUrl"]
                .Value?
                .ToString();

            if (string.IsNullOrWhiteSpace(url))
                return;

            switch (columnName)
            {
                case "colDownload":
                    _mainForm.SetDownloadUrl(url);
                    Close();
                    break;

                case "colOpenBrowser":
                    OpenUrlInBrowser(url);
                    break;
            }
        }

        private async void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure you want to clear your download history?",
            "Clear Download History",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            await DownloadHistoryManager.ClearAsync();

            await LoadHistoryAsync();
        }

        private async void saveAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DownloadHistoryEntry> history =
            await DownloadHistoryManager.LoadAsync();

            if (history.Count == 0)
            {
                MessageBox.Show(
                    "There is no download history to export.",
                    "Export History",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Export Download History",
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = "RZVD Download History.csv"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            var csv = new StringBuilder();

            csv.AppendLine(
                "\"Time/Date\",\"Title\",\"URL\",\"Source\"");

            foreach (DownloadHistoryEntry entry in history
                         .OrderByDescending(x => x.DownloadedAt))
            {
                csv.AppendLine(
                    $"\"{EscapeCsv(entry.DownloadedAt.ToString("O"))}\"," +
                    $"\"{EscapeCsv(entry.Title)}\"," +
                    $"\"{EscapeCsv(entry.Url)}\"," +
                    $"\"{EscapeCsv(entry.Source)}\"");
            }

            await File.WriteAllTextAsync(
                dialog.FileName,
                csv.ToString(),
                Encoding.UTF8);

            MessageBox.Show(
                "Download history was exported successfully.",
                "Export Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static string EscapeCsv(string value)
        {
            return value.Replace("\"", "\"\"");
        }

        private static void OpenUrlInBrowser(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"The URL could not be opened.\n\n{ex.Message}",
                    "Browser Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
