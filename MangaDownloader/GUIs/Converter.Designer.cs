namespace MangaDownloader.GUIs
{
    partial class Converter
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tssbtAddFiles = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiAddFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbtMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtToPDF = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSaveTo = new System.Windows.Forms.TextBox();
            this.dgvPhotos = new System.Windows.Forms.DataGridView();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhotos)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssbtAddFiles,
            this.tsbtMoveUp,
            this.tsbtMoveDown,
            this.toolStripSeparator1,
            this.tsbtToPDF});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(535, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tssbtAddFiles
            // 
            this.tssbtAddFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddFolders});
            this.tssbtAddFiles.Image = global::MangaDownloader.Properties.Resources.add_photos;
            this.tssbtAddFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssbtAddFiles.Name = "tssbtAddFiles";
            this.tssbtAddFiles.Size = new System.Drawing.Size(87, 22);
            this.tssbtAddFiles.Text = "Add Files";
            this.tssbtAddFiles.ButtonClick += new System.EventHandler(this.tssbtAddFiles_ButtonClick);
            // 
            // tsmiAddFolders
            // 
            this.tsmiAddFolders.Image = global::MangaDownloader.Properties.Resources.add_folders;
            this.tsmiAddFolders.Name = "tsmiAddFolders";
            this.tsmiAddFolders.Size = new System.Drawing.Size(137, 22);
            this.tsmiAddFolders.Text = "Add Folders";
            this.tsmiAddFolders.Click += new System.EventHandler(this.tsmiAddFolders_Click);
            // 
            // tsbtMoveUp
            // 
            this.tsbtMoveUp.Image = global::MangaDownloader.Properties.Resources.arrow_up;
            this.tsbtMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtMoveUp.Name = "tsbtMoveUp";
            this.tsbtMoveUp.Size = new System.Drawing.Size(42, 22);
            this.tsbtMoveUp.Text = "Up";
            this.tsbtMoveUp.Click += new System.EventHandler(this.tsbtMoveUp_Click);
            // 
            // tsbtMoveDown
            // 
            this.tsbtMoveDown.Image = global::MangaDownloader.Properties.Resources.arrow_down;
            this.tsbtMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtMoveDown.Name = "tsbtMoveDown";
            this.tsbtMoveDown.Size = new System.Drawing.Size(58, 22);
            this.tsbtMoveDown.Text = "Down";
            this.tsbtMoveDown.Click += new System.EventHandler(this.tsbtMoveDown_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtToPDF
            // 
            this.tsbtToPDF.Image = global::MangaDownloader.Properties.Resources.exchange;
            this.tsbtToPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtToPDF.Name = "tsbtToPDF";
            this.tsbtToPDF.Size = new System.Drawing.Size(65, 22);
            this.tsbtToPDF.Text = "To PDF";
            this.tsbtToPDF.Click += new System.EventHandler(this.tsbtToPDF_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslbStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 319);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(535, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsslbStatus
            // 
            this.tsslbStatus.Name = "tsslbStatus";
            this.tsslbStatus.Size = new System.Drawing.Size(79, 17);
            this.tsslbStatus.Text = "99 files added";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btBrowse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbSaveTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 29);
            this.panel1.TabIndex = 2;
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Location = new System.Drawing.Point(457, 3);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 2;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Save to:";
            // 
            // tbSaveTo
            // 
            this.tbSaveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSaveTo.Location = new System.Drawing.Point(62, 3);
            this.tbSaveTo.Name = "tbSaveTo";
            this.tbSaveTo.Size = new System.Drawing.Size(389, 22);
            this.tbSaveTo.TabIndex = 0;
            // 
            // dgvPhotos
            // 
            this.dgvPhotos.AllowUserToAddRows = false;
            this.dgvPhotos.AllowUserToDeleteRows = false;
            this.dgvPhotos.AllowUserToResizeColumns = false;
            this.dgvPhotos.AllowUserToResizeRows = false;
            this.dgvPhotos.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPhotos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPhotos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPhotos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhotos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cName,
            this.cPath});
            this.dgvPhotos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhotos.Location = new System.Drawing.Point(0, 54);
            this.dgvPhotos.Name = "dgvPhotos";
            this.dgvPhotos.ReadOnly = true;
            this.dgvPhotos.RowHeadersVisible = false;
            this.dgvPhotos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhotos.Size = new System.Drawing.Size(535, 265);
            this.dgvPhotos.TabIndex = 3;
            this.dgvPhotos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPhotos_KeyUp);
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cName.HeaderText = "File Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cName.Width = 65;
            // 
            // cPath
            // 
            this.cPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cPath.HeaderText = "Path";
            this.cPath.Name = "cPath";
            this.cPath.ReadOnly = true;
            this.cPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Converter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 341);
            this.Controls.Add(this.dgvPhotos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Name = "Converter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Converter";
            this.Load += new System.EventHandler(this.Converter_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhotos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSplitButton tssbtAddFiles;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddFolders;
        private System.Windows.Forms.ToolStripButton tsbtToPDF;
        private System.Windows.Forms.ToolStripButton tsbtMoveUp;
        private System.Windows.Forms.ToolStripButton tsbtMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslbStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSaveTo;
        private System.Windows.Forms.DataGridView dgvPhotos;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPath;
    }
}