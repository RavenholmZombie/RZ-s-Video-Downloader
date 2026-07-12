namespace RZVD
{
    partial class frmHistory
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
            dgvHistory = new DataGridView();
            colTimeDate = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colUrl = new DataGridViewTextBoxColumn();
            colSource = new DataGridViewTextBoxColumn();
            colDownload = new DataGridViewButtonColumn();
            colOpenBrowser = new DataGridViewButtonColumn();
            menuStrip1 = new MenuStrip();
            manageHistoryToolStripMenuItem = new ToolStripMenuItem();
            clearHistoryToolStripMenuItem = new ToolStripMenuItem();
            saveAsCSVToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.BorderStyle = BorderStyle.Fixed3D;
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Columns.AddRange(new DataGridViewColumn[] { colTimeDate, colTitle, colUrl, colSource, colDownload, colOpenBrowser });
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.Location = new Point(0, 24);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(658, 356);
            dgvHistory.TabIndex = 0;
            dgvHistory.CellContentClick += dgvHistory_CellContentClick;
            // 
            // colTimeDate
            // 
            colTimeDate.HeaderText = "Time/Date";
            colTimeDate.Name = "colTimeDate";
            colTimeDate.ReadOnly = true;
            // 
            // colTitle
            // 
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTitle.HeaderText = "Title";
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colUrl
            // 
            colUrl.HeaderText = "URL";
            colUrl.Name = "colUrl";
            colUrl.ReadOnly = true;
            // 
            // colSource
            // 
            colSource.HeaderText = "Source";
            colSource.Name = "colSource";
            colSource.ReadOnly = true;
            // 
            // colDownload
            // 
            colDownload.HeaderText = "Download";
            colDownload.Name = "colDownload";
            colDownload.ReadOnly = true;
            colDownload.Text = "Download Again";
            colDownload.UseColumnTextForButtonValue = true;
            // 
            // colOpenBrowser
            // 
            colOpenBrowser.HeaderText = "Open in Browser";
            colOpenBrowser.Name = "colOpenBrowser";
            colOpenBrowser.ReadOnly = true;
            colOpenBrowser.Text = "Click to Open";
            colOpenBrowser.UseColumnTextForButtonValue = true;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { manageHistoryToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(658, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // manageHistoryToolStripMenuItem
            // 
            manageHistoryToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clearHistoryToolStripMenuItem, saveAsCSVToolStripMenuItem });
            manageHistoryToolStripMenuItem.Image = Properties.Resources.book_open1;
            manageHistoryToolStripMenuItem.ImageAlign = ContentAlignment.MiddleLeft;
            manageHistoryToolStripMenuItem.Name = "manageHistoryToolStripMenuItem";
            manageHistoryToolStripMenuItem.Size = new Size(119, 20);
            manageHistoryToolStripMenuItem.Text = "Manage History";
            // 
            // clearHistoryToolStripMenuItem
            // 
            clearHistoryToolStripMenuItem.Image = Properties.Resources.book_delete;
            clearHistoryToolStripMenuItem.ImageAlign = ContentAlignment.MiddleLeft;
            clearHistoryToolStripMenuItem.Name = "clearHistoryToolStripMenuItem";
            clearHistoryToolStripMenuItem.Size = new Size(180, 22);
            clearHistoryToolStripMenuItem.Text = "Clear History";
            clearHistoryToolStripMenuItem.Click += clearHistoryToolStripMenuItem_Click;
            // 
            // saveAsCSVToolStripMenuItem
            // 
            saveAsCSVToolStripMenuItem.Image = Properties.Resources.page_excel;
            saveAsCSVToolStripMenuItem.ImageAlign = ContentAlignment.MiddleLeft;
            saveAsCSVToolStripMenuItem.Name = "saveAsCSVToolStripMenuItem";
            saveAsCSVToolStripMenuItem.Size = new Size(180, 22);
            saveAsCSVToolStripMenuItem.Text = "Save as CSV";
            saveAsCSVToolStripMenuItem.Click += saveAsCSVToolStripMenuItem_Click;
            // 
            // frmHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(658, 380);
            Controls.Add(dgvHistory);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmHistory";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Download History";
            Load += frmHistory_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvHistory;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem manageHistoryToolStripMenuItem;
        private ToolStripMenuItem clearHistoryToolStripMenuItem;
        private ToolStripMenuItem saveAsCSVToolStripMenuItem;
        private DataGridViewTextBoxColumn colTimeDate;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colUrl;
        private DataGridViewTextBoxColumn colSource;
        private DataGridViewButtonColumn colDownload;
        private DataGridViewButtonColumn colOpenBrowser;
    }
}