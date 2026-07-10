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
                await webViewHelp.EnsureCoreWebView2Async();

                string helpPath = Path.Combine(
                    AppContext.BaseDirectory,
                    "Resources",
                    "RZVD_Help.html");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The Help file could not be found.",
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
            }
        }
    }
}
