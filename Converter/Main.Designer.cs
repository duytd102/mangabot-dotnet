namespace Converter
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbtAddFiles = new System.Windows.Forms.ToolStripButton();
            this.tsbtAddFolders = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtToPDF = new System.Windows.Forms.ToolStripButton();
            this.tsbtToZip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbtMoveDown = new System.Windows.Forms.ToolStripButton();
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
            this.tsbtAddFiles,
            this.tsbtAddFolders,
            this.toolStripSeparator2,
            this.tsbtToPDF,
            this.tsbtToZip,
            this.toolStripSeparator1,
            this.tsbtMoveUp,
            this.tsbtMoveDown});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(549, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbtAddFiles
            // 
            this.tsbtAddFiles.Image = global::Converter.Properties.Resources.add_photos;
            this.tsbtAddFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtAddFiles.Name = "tsbtAddFiles";
            this.tsbtAddFiles.Size = new System.Drawing.Size(75, 22);
            this.tsbtAddFiles.Text = "Add Files";
            this.tsbtAddFiles.Click += new System.EventHandler(this.tsbtAddFiles_Click);
            // 
            // tsbtAddFolders
            // 
            this.tsbtAddFolders.Image = global::Converter.Properties.Resources.add_folders;
            this.tsbtAddFolders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtAddFolders.Name = "tsbtAddFolders";
            this.tsbtAddFolders.Size = new System.Drawing.Size(90, 22);
            this.tsbtAddFolders.Text = "Add Folders";
            this.tsbtAddFolders.Click += new System.EventHandler(this.tsbtAddFolders_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtToPDF
            // 
            this.tsbtToPDF.Image = global::Converter.Properties.Resources.to_pdf;
            this.tsbtToPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtToPDF.Name = "tsbtToPDF";
            this.tsbtToPDF.Size = new System.Drawing.Size(65, 22);
            this.tsbtToPDF.Text = "To PDF";
            this.tsbtToPDF.Click += new System.EventHandler(this.tsbtToPDF_Click);
            // 
            // tsbtToZip
            // 
            this.tsbtToZip.Image = global::Converter.Properties.Resources.zip;
            this.tsbtToZip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtToZip.Name = "tsbtToZip";
            this.tsbtToZip.Size = new System.Drawing.Size(61, 22);
            this.tsbtToZip.Text = "To Zip";
            this.tsbtToZip.Click += new System.EventHandler(this.tsbtToZip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtMoveUp
            // 
            this.tsbtMoveUp.Image = global::Converter.Properties.Resources.arrow_up;
            this.tsbtMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtMoveUp.Name = "tsbtMoveUp";
            this.tsbtMoveUp.Size = new System.Drawing.Size(42, 22);
            this.tsbtMoveUp.Text = "Up";
            this.tsbtMoveUp.Click += new System.EventHandler(this.tsbtMoveUp_Click);
            // 
            // tsbtMoveDown
            // 
            this.tsbtMoveDown.Image = global::Converter.Properties.Resources.arrow_down;
            this.tsbtMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtMoveDown.Name = "tsbtMoveDown";
            this.tsbtMoveDown.Size = new System.Drawing.Size(58, 22);
            this.tsbtMoveDown.Text = "Down";
            this.tsbtMoveDown.Click += new System.EventHandler(this.tsbtMoveDown_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslbStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 333);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(549, 22);
            this.statusStrip.TabIndex = 5;
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
            this.panel1.Size = new System.Drawing.Size(549, 29);
            this.panel1.TabIndex = 6;
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Location = new System.Drawing.Point(471, 3);
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
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Save to:";
            // 
            // tbSaveTo
            // 
            this.tbSaveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSaveTo.Location = new System.Drawing.Point(62, 5);
            this.tbSaveTo.Name = "tbSaveTo";
            this.tbSaveTo.Size = new System.Drawing.Size(403, 20);
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
            this.dgvPhotos.Size = new System.Drawing.Size(549, 279);
            this.dgvPhotos.TabIndex = 7;
            this.dgvPhotos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPhotos_KeyUp);
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cName.HeaderText = "File Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            this.cName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cName.Width = 60;
            // 
            // cPath
            // 
            this.cPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cPath.HeaderText = "Path";
            this.cPath.Name = "cPath";
            this.cPath.ReadOnly = true;
            this.cPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 355);
            this.Controls.Add(this.dgvPhotos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Converter";
            this.Load += new System.EventHandler(this.Main_Load);
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
        private System.Windows.Forms.ToolStripButton tsbtMoveUp;
        private System.Windows.Forms.ToolStripButton tsbtMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtToPDF;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslbStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSaveTo;
        private System.Windows.Forms.DataGridView dgvPhotos;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPath;
        private System.Windows.Forms.ToolStripButton tsbtToZip;
        private System.Windows.Forms.ToolStripButton tsbtAddFiles;
        private System.Windows.Forms.ToolStripButton tsbtAddFolders;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

