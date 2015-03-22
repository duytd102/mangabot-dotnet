namespace MangaDownloader.GUIs
{
    partial class Settings
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
            this.btBrowse = new System.Windows.Forms.Button();
            this.cbAutoClean = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbDefaultFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAutoCreatePDF = new System.Windows.Forms.CheckBox();
            this.cbAutoCreateZip = new System.Windows.Forms.CheckBox();
            this.cbAutoCreateShortcut = new System.Windows.Forms.CheckBox();
            this.cbAutoUpdate = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTotalWorkers = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalWorkers)).BeginInit();
            this.SuspendLayout();
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Image = global::MangaDownloader.Properties.Resources.folder_explore;
            this.btBrowse.Location = new System.Drawing.Point(251, 184);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(35, 22);
            this.btBrowse.TabIndex = 28;
            this.btBrowse.UseVisualStyleBackColor = false;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // cbAutoClean
            // 
            this.cbAutoClean.AutoSize = true;
            this.cbAutoClean.Enabled = false;
            this.cbAutoClean.Location = new System.Drawing.Point(12, 129);
            this.cbAutoClean.Name = "cbAutoClean";
            this.cbAutoClean.Size = new System.Drawing.Size(132, 18);
            this.cbAutoClean.TabIndex = 27;
            this.cbAutoClean.Text = "Automatic clean up";
            this.cbAutoClean.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(113, 212);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbDefaultFolder
            // 
            this.tbDefaultFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDefaultFolder.Location = new System.Drawing.Point(12, 184);
            this.tbDefaultFolder.Name = "tbDefaultFolder";
            this.tbDefaultFolder.Size = new System.Drawing.Size(233, 22);
            this.tbDefaultFolder.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 23;
            this.label3.Text = "Download to";
            // 
            // cbAutoCreatePDF
            // 
            this.cbAutoCreatePDF.AutoSize = true;
            this.cbAutoCreatePDF.Location = new System.Drawing.Point(12, 105);
            this.cbAutoCreatePDF.Name = "cbAutoCreatePDF";
            this.cbAutoCreatePDF.Size = new System.Drawing.Size(210, 18);
            this.cbAutoCreatePDF.TabIndex = 22;
            this.cbAutoCreatePDF.Text = "Create PDF file for every chapters";
            this.cbAutoCreatePDF.UseVisualStyleBackColor = true;
            this.cbAutoCreatePDF.CheckedChanged += new System.EventHandler(this.cbAutoCreatePDF_CheckedChanged);
            // 
            // cbAutoCreateZip
            // 
            this.cbAutoCreateZip.AutoSize = true;
            this.cbAutoCreateZip.Location = new System.Drawing.Point(12, 81);
            this.cbAutoCreateZip.Name = "cbAutoCreateZip";
            this.cbAutoCreateZip.Size = new System.Drawing.Size(207, 18);
            this.cbAutoCreateZip.TabIndex = 21;
            this.cbAutoCreateZip.Text = "Create ZIP file for every chapters";
            this.cbAutoCreateZip.UseVisualStyleBackColor = true;
            this.cbAutoCreateZip.CheckedChanged += new System.EventHandler(this.cbAutoCreateZip_CheckedChanged);
            // 
            // cbAutoCreateShortcut
            // 
            this.cbAutoCreateShortcut.AutoSize = true;
            this.cbAutoCreateShortcut.Location = new System.Drawing.Point(12, 57);
            this.cbAutoCreateShortcut.Name = "cbAutoCreateShortcut";
            this.cbAutoCreateShortcut.Size = new System.Drawing.Size(184, 18);
            this.cbAutoCreateShortcut.TabIndex = 20;
            this.cbAutoCreateShortcut.Text = "Create a shortcut to chapter";
            this.cbAutoCreateShortcut.UseVisualStyleBackColor = true;
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.AutoSize = true;
            this.cbAutoUpdate.Checked = true;
            this.cbAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoUpdate.Location = new System.Drawing.Point(12, 33);
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.Size = new System.Drawing.Size(130, 18);
            this.cbAutoUpdate.TabIndex = 19;
            this.cbAutoUpdate.Text = "Automatic updates";
            this.cbAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "task(s) at the same time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Download";
            // 
            // nudTotalWorkers
            // 
            this.nudTotalWorkers.Location = new System.Drawing.Point(76, 7);
            this.nudTotalWorkers.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudTotalWorkers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTotalWorkers.Name = "nudTotalWorkers";
            this.nudTotalWorkers.Size = new System.Drawing.Size(35, 22);
            this.nudTotalWorkers.TabIndex = 16;
            this.nudTotalWorkers.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 254);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.cbAutoClean);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbDefaultFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbAutoCreatePDF);
            this.Controls.Add(this.cbAutoCreateZip);
            this.Controls.Add(this.cbAutoCreateShortcut);
            this.Controls.Add(this.cbAutoUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudTotalWorkers);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTotalWorkers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbAutoClean;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbDefaultFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbAutoCreatePDF;
        private System.Windows.Forms.CheckBox cbAutoCreateZip;
        private System.Windows.Forms.CheckBox cbAutoCreateShortcut;
        private System.Windows.Forms.CheckBox cbAutoUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudTotalWorkers;
        private System.Windows.Forms.Button btBrowse;
    }
}