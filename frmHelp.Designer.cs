namespace RZVD
{
    partial class frmHelp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webViewHelp = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webViewHelp).BeginInit();
            SuspendLayout();
            // 
            // webViewHelp
            // 
            webViewHelp.AllowExternalDrop = true;
            webViewHelp.CreationProperties = null;
            webViewHelp.DefaultBackgroundColor = Color.White;
            webViewHelp.Dock = DockStyle.Fill;
            webViewHelp.Location = new Point(0, 0);
            webViewHelp.Name = "webViewHelp";
            webViewHelp.Size = new Size(510, 479);
            webViewHelp.TabIndex = 0;
            webViewHelp.ZoomFactor = 1D;
            // 
            // frmHelp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(510, 479);
            Controls.Add(webViewHelp);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHelp";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Help";
            Load += frmHelp_Load;
            ((System.ComponentModel.ISupportInitialize)webViewHelp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewHelp;
    }
}