namespace YouTubeDownloader
{
    partial class frmAbout
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
            btnCheckForUpdates = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            gBoxUpdate = new GroupBox();
            lblUpdateStatus = new Label();
            btnDownload = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            gBoxUpdate.SuspendLayout();
            SuspendLayout();
            // 
            // btnCheckForUpdates
            // 
            btnCheckForUpdates.Anchor = AnchorStyles.Bottom;
            btnCheckForUpdates.Location = new Point(97, 188);
            btnCheckForUpdates.Name = "btnCheckForUpdates";
            btnCheckForUpdates.Size = new Size(122, 23);
            btnCheckForUpdates.TabIndex = 0;
            btnCheckForUpdates.Text = "Check for Updates";
            btnCheckForUpdates.UseVisualStyleBackColor = true;
            btnCheckForUpdates.Click += btnCheckForUpdates_ClickAsync;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Tatice_Browsers_Download_256;
            pictureBox1.Location = new Point(124, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(68, 68);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(25, 105);
            label1.Name = "label1";
            label1.Size = new Size(266, 32);
            label1.TabIndex = 2;
            label1.Text = "RZ's Video Downloader";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(96, 137);
            label2.Name = "label2";
            label2.Size = new Size(124, 15);
            label2.TabIndex = 3;
            label2.Text = "By RavenholmZombie";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom;
            button1.Location = new Point(98, 217);
            button1.Name = "button1";
            button1.Size = new Size(122, 23);
            button1.TabIndex = 4;
            button1.Text = "Credits";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // gBoxUpdate
            // 
            gBoxUpdate.Controls.Add(lblUpdateStatus);
            gBoxUpdate.Controls.Add(btnDownload);
            gBoxUpdate.Location = new Point(25, 167);
            gBoxUpdate.Name = "gBoxUpdate";
            gBoxUpdate.Size = new Size(266, 100);
            gBoxUpdate.TabIndex = 5;
            gBoxUpdate.TabStop = false;
            gBoxUpdate.Text = "Update:";
            // 
            // lblUpdateStatus
            // 
            lblUpdateStatus.AutoSize = true;
            lblUpdateStatus.BackColor = Color.Transparent;
            lblUpdateStatus.Location = new Point(11, 26);
            lblUpdateStatus.Name = "lblUpdateStatus";
            lblUpdateStatus.Size = new Size(0, 15);
            lblUpdateStatus.TabIndex = 1;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(144, 71);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(116, 23);
            btnDownload.TabIndex = 0;
            btnDownload.Text = "Download Update";
            btnDownload.UseVisualStyleBackColor = true;
            // 
            // frmAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(316, 268);
            Controls.Add(gBoxUpdate);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(btnCheckForUpdates);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAbout";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "About RZ's Video Downloader";
            Load += frmAbout_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            gBoxUpdate.ResumeLayout(false);
            gBoxUpdate.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCheckForUpdates;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Button button1;
        private GroupBox gBoxUpdate;
        private Button btnDownload;
        private Label lblUpdateStatus;
    }
}