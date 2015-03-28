namespace MangaDownloader.GUIs
{
    partial class Grabber
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbMangaURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btGrab = new System.Windows.Forms.Button();
            this.dgvChapterList = new System.Windows.Forms.DataGridView();
            this.cNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsChapterList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddToQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewOnline = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChapterList)).BeginInit();
            this.cmsChapterList.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMangaURL
            // 
            this.tbMangaURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMangaURL.Location = new System.Drawing.Point(89, 12);
            this.tbMangaURL.Name = "tbMangaURL";
            this.tbMangaURL.Size = new System.Drawing.Size(261, 22);
            this.tbMangaURL.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Manga URL:";
            // 
            // btGrab
            // 
            this.btGrab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btGrab.Location = new System.Drawing.Point(356, 11);
            this.btGrab.Name = "btGrab";
            this.btGrab.Size = new System.Drawing.Size(65, 23);
            this.btGrab.TabIndex = 2;
            this.btGrab.Text = "Grab";
            this.btGrab.UseVisualStyleBackColor = true;
            this.btGrab.Click += new System.EventHandler(this.btGrab_Click);
            // 
            // dgvChapterList
            // 
            this.dgvChapterList.AllowUserToAddRows = false;
            this.dgvChapterList.AllowUserToDeleteRows = false;
            this.dgvChapterList.AllowUserToResizeColumns = false;
            this.dgvChapterList.AllowUserToResizeRows = false;
            this.dgvChapterList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChapterList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvChapterList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvChapterList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChapterList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNo,
            this.cName,
            this.cURL});
            this.dgvChapterList.ContextMenuStrip = this.cmsChapterList;
            this.dgvChapterList.Location = new System.Drawing.Point(12, 40);
            this.dgvChapterList.Name = "dgvChapterList";
            this.dgvChapterList.ReadOnly = true;
            this.dgvChapterList.RowHeadersVisible = false;
            this.dgvChapterList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChapterList.Size = new System.Drawing.Size(409, 199);
            this.dgvChapterList.TabIndex = 3;
            this.dgvChapterList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvChapterList_CellMouseDown);
            // 
            // cNo
            // 
            this.cNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.cNo.HeaderText = "No";
            this.cNo.Name = "cNo";
            this.cNo.ReadOnly = true;
            this.cNo.Width = 47;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cName.HeaderText = "Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cName.Width = 63;
            // 
            // cURL
            // 
            this.cURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cURL.HeaderText = "URL";
            this.cURL.Name = "cURL";
            this.cURL.ReadOnly = true;
            this.cURL.Width = 53;
            // 
            // cmsChapterList
            // 
            this.cmsChapterList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDownload,
            this.toolStripSeparator1,
            this.tsmiAddToQueue,
            this.tsmiCopyURL,
            this.tsmiViewOnline});
            this.cmsChapterList.Name = "cmsChapterList";
            this.cmsChapterList.Size = new System.Drawing.Size(153, 120);
            this.cmsChapterList.Opening += new System.ComponentModel.CancelEventHandler(this.cmsChapterList_Opening);
            // 
            // tsmiAddToQueue
            // 
            this.tsmiAddToQueue.Image = global::MangaDownloader.Properties.Resources.add;
            this.tsmiAddToQueue.Name = "tsmiAddToQueue";
            this.tsmiAddToQueue.Size = new System.Drawing.Size(152, 22);
            this.tsmiAddToQueue.Text = "Add to Queue";
            this.tsmiAddToQueue.Click += new System.EventHandler(this.tsmiAddToQueue_Click);
            // 
            // tsmiDownload
            // 
            this.tsmiDownload.Image = global::MangaDownloader.Properties.Resources.download;
            this.tsmiDownload.Name = "tsmiDownload";
            this.tsmiDownload.Size = new System.Drawing.Size(152, 22);
            this.tsmiDownload.Text = "Download";
            this.tsmiDownload.Click += new System.EventHandler(this.tsmiDownload_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiCopyURL
            // 
            this.tsmiCopyURL.Image = global::MangaDownloader.Properties.Resources.copy;
            this.tsmiCopyURL.Name = "tsmiCopyURL";
            this.tsmiCopyURL.Size = new System.Drawing.Size(152, 22);
            this.tsmiCopyURL.Text = "Copy URL";
            this.tsmiCopyURL.Click += new System.EventHandler(this.tsmiCopyURL_Click);
            // 
            // tsmiViewOnline
            // 
            this.tsmiViewOnline.Image = global::MangaDownloader.Properties.Resources.browser;
            this.tsmiViewOnline.Name = "tsmiViewOnline";
            this.tsmiViewOnline.Size = new System.Drawing.Size(152, 22);
            this.tsmiViewOnline.Text = "View online";
            this.tsmiViewOnline.Click += new System.EventHandler(this.tsmiViewOnline_Click);
            // 
            // Grabber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 251);
            this.Controls.Add(this.dgvChapterList);
            this.Controls.Add(this.btGrab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMangaURL);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Grabber";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grabber";
            this.Load += new System.EventHandler(this.Grabber_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChapterList)).EndInit();
            this.cmsChapterList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMangaURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btGrab;
        private System.Windows.Forms.DataGridView dgvChapterList;
        private System.Windows.Forms.ContextMenuStrip cmsChapterList;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddToQueue;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewOnline;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiDownload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}