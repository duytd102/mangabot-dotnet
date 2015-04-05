namespace MangaDownloader.GUIs
{
    partial class AdvancedSearch
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
            this.cmsSearch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAddToQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.gbKeyword = new System.Windows.Forms.GroupBox();
            this.tbKeyword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbNote = new System.Windows.Forms.Label();
            this.btSearch = new System.Windows.Forms.Button();
            this.dgvMangaList = new System.Windows.Forms.DataGridView();
            this.cSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsSearch.SuspendLayout();
            this.gbKeyword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMangaList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsSearch
            // 
            this.cmsSearch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDownload,
            this.toolStripSeparator1,
            this.tsmiAddToQueue,
            this.tsmiCopyURL,
            this.tsmiViewOnline});
            this.cmsSearch.Name = "cmsSearch";
            this.cmsSearch.Size = new System.Drawing.Size(149, 98);
            this.cmsSearch.Opening += new System.ComponentModel.CancelEventHandler(this.cmsSearch_Opening);
            // 
            // tsmiDownload
            // 
            this.tsmiDownload.Image = global::MangaDownloader.Properties.Resources.download;
            this.tsmiDownload.Name = "tsmiDownload";
            this.tsmiDownload.Size = new System.Drawing.Size(148, 22);
            this.tsmiDownload.Text = "Download";
            this.tsmiDownload.Click += new System.EventHandler(this.tsmiDownload_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // tsmiAddToQueue
            // 
            this.tsmiAddToQueue.Image = global::MangaDownloader.Properties.Resources.add;
            this.tsmiAddToQueue.Name = "tsmiAddToQueue";
            this.tsmiAddToQueue.Size = new System.Drawing.Size(148, 22);
            this.tsmiAddToQueue.Text = "Add to Queue";
            this.tsmiAddToQueue.Click += new System.EventHandler(this.tsmiAddToQueue_Click);
            // 
            // tsmiCopyURL
            // 
            this.tsmiCopyURL.Image = global::MangaDownloader.Properties.Resources.copy;
            this.tsmiCopyURL.Name = "tsmiCopyURL";
            this.tsmiCopyURL.Size = new System.Drawing.Size(148, 22);
            this.tsmiCopyURL.Text = "Copy URL";
            this.tsmiCopyURL.Click += new System.EventHandler(this.tsmiCopyURL_Click);
            // 
            // tsmiViewOnline
            // 
            this.tsmiViewOnline.Image = global::MangaDownloader.Properties.Resources.browser;
            this.tsmiViewOnline.Name = "tsmiViewOnline";
            this.tsmiViewOnline.Size = new System.Drawing.Size(148, 22);
            this.tsmiViewOnline.Text = "View online";
            this.tsmiViewOnline.Click += new System.EventHandler(this.tsmiViewOnline_Click);
            // 
            // gbKeyword
            // 
            this.gbKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbKeyword.Controls.Add(this.tbKeyword);
            this.gbKeyword.Controls.Add(this.label2);
            this.gbKeyword.Controls.Add(this.lbNote);
            this.gbKeyword.Controls.Add(this.btSearch);
            this.gbKeyword.Location = new System.Drawing.Point(12, 12);
            this.gbKeyword.Name = "gbKeyword";
            this.gbKeyword.Size = new System.Drawing.Size(491, 81);
            this.gbKeyword.TabIndex = 7;
            this.gbKeyword.TabStop = false;
            this.gbKeyword.Text = "Keyword";
            // 
            // tbKeyword
            // 
            this.tbKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKeyword.Location = new System.Drawing.Point(77, 21);
            this.tbKeyword.Name = "tbKeyword";
            this.tbKeyword.Size = new System.Drawing.Size(327, 22);
            this.tbKeyword.TabIndex = 0;
            this.tbKeyword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbKeyword_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "Search by:";
            // 
            // lbNote
            // 
            this.lbNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNote.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNote.Location = new System.Drawing.Point(74, 46);
            this.lbNote.Name = "lbNote";
            this.lbNote.Size = new System.Drawing.Size(330, 31);
            this.lbNote.TabIndex = 6;
            this.lbNote.Text = "* Search by name or url of manga, the result is filtered from manga list of sites" +
    " were fetched before.";
            // 
            // btSearch
            // 
            this.btSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSearch.Image = global::MangaDownloader.Properties.Resources.search1;
            this.btSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSearch.Location = new System.Drawing.Point(410, 19);
            this.btSearch.Name = "btSearch";
            this.btSearch.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btSearch.Size = new System.Drawing.Size(75, 25);
            this.btSearch.TabIndex = 4;
            this.btSearch.Text = "Search";
            this.btSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // dgvMangaList
            // 
            this.dgvMangaList.AllowUserToAddRows = false;
            this.dgvMangaList.AllowUserToDeleteRows = false;
            this.dgvMangaList.AllowUserToResizeColumns = false;
            this.dgvMangaList.AllowUserToResizeRows = false;
            this.dgvMangaList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMangaList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMangaList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMangaList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMangaList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cSite,
            this.cName,
            this.cUrl});
            this.dgvMangaList.ContextMenuStrip = this.cmsSearch;
            this.dgvMangaList.Location = new System.Drawing.Point(12, 99);
            this.dgvMangaList.Name = "dgvMangaList";
            this.dgvMangaList.ReadOnly = true;
            this.dgvMangaList.RowHeadersVisible = false;
            this.dgvMangaList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMangaList.Size = new System.Drawing.Size(491, 228);
            this.dgvMangaList.TabIndex = 1;
            this.dgvMangaList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMangaList_CellMouseDown);
            // 
            // cSite
            // 
            this.cSite.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cSite.DataPropertyName = "site";
            this.cSite.HeaderText = "Site";
            this.cSite.Name = "cSite";
            this.cSite.ReadOnly = true;
            this.cSite.Width = 53;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cName.DataPropertyName = "name";
            this.cName.HeaderText = "Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cName.Width = 63;
            // 
            // cUrl
            // 
            this.cUrl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cUrl.DataPropertyName = "url";
            this.cUrl.HeaderText = "URL";
            this.cUrl.Name = "cUrl";
            this.cUrl.ReadOnly = true;
            this.cUrl.Width = 53;
            // 
            // AdvancedSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 339);
            this.Controls.Add(this.gbKeyword);
            this.Controls.Add(this.dgvMangaList);
            this.Name = "AdvancedSearch";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Search";
            this.Load += new System.EventHandler(this.AdvancedSearch_Load);
            this.cmsSearch.ResumeLayout(false);
            this.gbKeyword.ResumeLayout(false);
            this.gbKeyword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMangaList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMangaList;
        private System.Windows.Forms.TextBox tbKeyword;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.ContextMenuStrip cmsSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmiDownload;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddToQueue;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewOnline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lbNote;
        private System.Windows.Forms.GroupBox gbKeyword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUrl;
    }
}