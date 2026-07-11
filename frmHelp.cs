using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RZVD
{
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
        }

        private async void frmHelp_Load(object sender, EventArgs e)
        {
            try
            {
                string webViewDataFolder = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    "RZ's Video Downloader",
                    "WebView2");

                Directory.CreateDirectory(webViewDataFolder);

                CoreWebView2Environment environment =
                    await CoreWebView2Environment.CreateAsync(
                        browserExecutableFolder: null,
                        userDataFolder: webViewDataFolder);

                await webViewHelp.EnsureCoreWebView2Async(environment);

                string helpPath = Path.Combine(
                    AppContext.BaseDirectory,
                    "Resources",
                    "RZVD_Help.html");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        $"The Help file could not be found.\n\n" +
                        $"Expected location:\n{helpPath}",
                        "Help Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                webViewHelp.Source = new Uri(helpPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"The Help window could not be opened.\n\n{ex.Message}",
                    "WebView2 Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
