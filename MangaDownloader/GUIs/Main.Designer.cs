namespace MangaDownloader.GUIs
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scList = new System.Windows.Forms.SplitContainer();
            this.dgvMangaList = new System.Windows.Forms.DataGridView();
            this.cmsMangaMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMangaCommands = new System.Windows.Forms.ToolStrip();
            this.tslbSiteName = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnRefreshMangaList = new System.Windows.Forms.ToolStripButton();
            this.dgvChapterList = new System.Windows.Forms.DataGridView();
            this.cmsChapterMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsChapterNavigation = new System.Windows.Forms.ToolStrip();
            this.tslbPathLabel = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnPath = new System.Windows.Forms.ToolStripButton();
            this.tcTasks = new System.Windows.Forms.TabControl();
            this.tpTasks = new System.Windows.Forms.TabPage();
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.cmsTaskMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsTaskCommands = new System.Windows.Forms.ToolStrip();
            this.tsbtnPauseAll = new System.Windows.Forms.ToolStripButton();
            this.tsbtnStartAll = new System.Windows.Forms.ToolStripButton();
            this.tpEventLogs = new System.Windows.Forms.TabPage();
            this.dgvEventLogs = new System.Windows.Forms.DataGridView();
            this.msTop = new System.Windows.Forms.MenuStrip();
            this.tsmiVietnameseSites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBlogTruyen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEnglishSites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMangaFox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiContactMe = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMangaViewOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMangaCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMangaAddToQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChapterViewOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChapterCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChapterAddToQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.colMangaNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMangaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMangaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMangaURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMangaSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskProgress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskDownloadTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.scList.Panel1.SuspendLayout();
            this.scList.Panel2.SuspendLayout();
            this.scList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMangaList)).BeginInit();
            this.cmsMangaMenu.SuspendLayout();
            this.tsMangaCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChapterList)).BeginInit();
            this.cmsChapterMenu.SuspendLayout();
            this.tsChapterNavigation.SuspendLayout();
            this.tcTasks.SuspendLayout();
            this.tpTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.tsTaskCommands.SuspendLayout();
            this.tpEventLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventLogs)).BeginInit();
            this.msTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 24);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.scList);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.tcTasks);
            this.scMain.Size = new System.Drawing.Size(645, 423);
            this.scMain.SplitterDistance = 238;
            this.scMain.TabIndex = 0;
            // 
            // scList
            // 
            this.scList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scList.Location = new System.Drawing.Point(0, 0);
            this.scList.Name = "scList";
            // 
            // scList.Panel1
            // 
            this.scList.Panel1.Controls.Add(this.dgvMangaList);
            this.scList.Panel1.Controls.Add(this.tsMangaCommands);
            // 
            // scList.Panel2
            // 
            this.scList.Panel2.Controls.Add(this.dgvChapterList);
            this.scList.Panel2.Controls.Add(this.tsChapterNavigation);
            this.scList.Size = new System.Drawing.Size(645, 238);
            this.scList.SplitterDistance = 215;
            this.scList.TabIndex = 0;
            // 
            // dgvMangaList
            // 
            this.dgvMangaList.AllowUserToAddRows = false;
            this.dgvMangaList.AllowUserToDeleteRows = false;
            this.dgvMangaList.AllowUserToOrderColumns = true;
            this.dgvMangaList.AllowUserToResizeColumns = false;
            this.dgvMangaList.AllowUserToResizeRows = false;
            this.dgvMangaList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMangaList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvMangaList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMangaList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMangaNo,
            this.colMangaID,
            this.colMangaName,
            this.colMangaURL,
            this.colMangaSite});
            this.dgvMangaList.ContextMenuStrip = this.cmsMangaMenu;
            this.dgvMangaList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMangaList.Location = new System.Drawing.Point(0, 25);
            this.dgvMangaList.Name = "dgvMangaList";
            this.dgvMangaList.RowHeadersVisible = false;
            this.dgvMangaList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMangaList.Size = new System.Drawing.Size(215, 213);
            this.dgvMangaList.TabIndex = 1;
            // 
            // cmsMangaMenu
            // 
            this.cmsMangaMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMangaViewOnline,
            this.tsmiMangaCopyURL,
            this.tsmiMangaAddToQueue});
            this.cmsMangaMenu.Name = "cmsMangaMenu";
            this.cmsMangaMenu.Size = new System.Drawing.Size(149, 70);
            // 
            // tsMangaCommands
            // 
            this.tsMangaCommands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMangaCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbSiteName,
            this.tsbtnRefreshMangaList});
            this.tsMangaCommands.Location = new System.Drawing.Point(0, 0);
            this.tsMangaCommands.Name = "tsMangaCommands";
            this.tsMangaCommands.Size = new System.Drawing.Size(215, 25);
            this.tsMangaCommands.TabIndex = 0;
            this.tsMangaCommands.Text = "toolStrip1";
            // 
            // tslbSiteName
            // 
            this.tslbSiteName.Image = global::MangaDownloader.Properties.Resources.blogtruyen_logo;
            this.tslbSiteName.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tslbSiteName.Name = "tslbSiteName";
            this.tslbSiteName.Size = new System.Drawing.Size(87, 22);
            this.tslbSiteName.Text = "Blog Truyen";
            // 
            // tsbtnRefreshMangaList
            // 
            this.tsbtnRefreshMangaList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnRefreshMangaList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefreshMangaList.Image")));
            this.tsbtnRefreshMangaList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefreshMangaList.Name = "tsbtnRefreshMangaList";
            this.tsbtnRefreshMangaList.Size = new System.Drawing.Size(66, 22);
            this.tsbtnRefreshMangaList.Text = "Refresh";
            // 
            // dgvChapterList
            // 
            this.dgvChapterList.AllowUserToAddRows = false;
            this.dgvChapterList.AllowUserToDeleteRows = false;
            this.dgvChapterList.AllowUserToOrderColumns = true;
            this.dgvChapterList.AllowUserToResizeColumns = false;
            this.dgvChapterList.AllowUserToResizeRows = false;
            this.dgvChapterList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvChapterList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvChapterList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChapterList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChapterNo,
            this.colChapterID,
            this.colChapterName,
            this.colChapterURL,
            this.colChapterSite});
            this.dgvChapterList.ContextMenuStrip = this.cmsChapterMenu;
            this.dgvChapterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChapterList.Location = new System.Drawing.Point(0, 25);
            this.dgvChapterList.Name = "dgvChapterList";
            this.dgvChapterList.RowHeadersVisible = false;
            this.dgvChapterList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChapterList.Size = new System.Drawing.Size(426, 213);
            this.dgvChapterList.TabIndex = 1;
            // 
            // cmsChapterMenu
            // 
            this.cmsChapterMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChapterViewOnline,
            this.tsmiChapterCopyURL,
            this.tsmiChapterAddToQueue});
            this.cmsChapterMenu.Name = "cmsChapterMenu";
            this.cmsChapterMenu.Size = new System.Drawing.Size(149, 70);
            // 
            // tsChapterNavigation
            // 
            this.tsChapterNavigation.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsChapterNavigation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbPathLabel,
            this.tsbtnPath});
            this.tsChapterNavigation.Location = new System.Drawing.Point(0, 0);
            this.tsChapterNavigation.Name = "tsChapterNavigation";
            this.tsChapterNavigation.Size = new System.Drawing.Size(426, 25);
            this.tsChapterNavigation.TabIndex = 0;
            this.tsChapterNavigation.Text = "toolStrip1";
            // 
            // tslbPathLabel
            // 
            this.tslbPathLabel.Name = "tslbPathLabel";
            this.tslbPathLabel.Size = new System.Drawing.Size(34, 22);
            this.tslbPathLabel.Text = "Path:";
            // 
            // tsbtnPath
            // 
            this.tsbtnPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnPath.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPath.Image")));
            this.tsbtnPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPath.Name = "tsbtnPath";
            this.tsbtnPath.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPath.Text = "/";
            // 
            // tcTasks
            // 
            this.tcTasks.Controls.Add(this.tpTasks);
            this.tcTasks.Controls.Add(this.tpEventLogs);
            this.tcTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTasks.Location = new System.Drawing.Point(0, 0);
            this.tcTasks.Name = "tcTasks";
            this.tcTasks.SelectedIndex = 0;
            this.tcTasks.Size = new System.Drawing.Size(645, 181);
            this.tcTasks.TabIndex = 0;
            // 
            // tpTasks
            // 
            this.tpTasks.Controls.Add(this.dgvTasks);
            this.tpTasks.Controls.Add(this.tsTaskCommands);
            this.tpTasks.Location = new System.Drawing.Point(4, 22);
            this.tpTasks.Name = "tpTasks";
            this.tpTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tpTasks.Size = new System.Drawing.Size(637, 155);
            this.tpTasks.TabIndex = 0;
            this.tpTasks.Text = "Tasks";
            this.tpTasks.UseVisualStyleBackColor = true;
            // 
            // dgvTasks
            // 
            this.dgvTasks.AllowUserToAddRows = false;
            this.dgvTasks.AllowUserToDeleteRows = false;
            this.dgvTasks.AllowUserToResizeColumns = false;
            this.dgvTasks.AllowUserToResizeRows = false;
            this.dgvTasks.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTasks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTaskID,
            this.colTaskName,
            this.colTaskStatus,
            this.colTaskProgress,
            this.colTaskDownloadTo,
            this.colTaskType,
            this.colTaskSite,
            this.colTaskURL,
            this.colTaskDescription});
            this.dgvTasks.ContextMenuStrip = this.cmsTaskMenu;
            this.dgvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTasks.Location = new System.Drawing.Point(3, 3);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.RowHeadersVisible = false;
            this.dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTasks.Size = new System.Drawing.Size(631, 124);
            this.dgvTasks.TabIndex = 2;
            // 
            // cmsTaskMenu
            // 
            this.cmsTaskMenu.Name = "cmsTaskMenu";
            this.cmsTaskMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // tsTaskCommands
            // 
            this.tsTaskCommands.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsTaskCommands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTaskCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPauseAll,
            this.tsbtnStartAll});
            this.tsTaskCommands.Location = new System.Drawing.Point(3, 127);
            this.tsTaskCommands.Name = "tsTaskCommands";
            this.tsTaskCommands.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tsTaskCommands.Size = new System.Drawing.Size(631, 25);
            this.tsTaskCommands.TabIndex = 1;
            this.tsTaskCommands.Text = "toolStrip1";
            // 
            // tsbtnPauseAll
            // 
            this.tsbtnPauseAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPauseAll.Image")));
            this.tsbtnPauseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPauseAll.Name = "tsbtnPauseAll";
            this.tsbtnPauseAll.Size = new System.Drawing.Size(75, 22);
            this.tsbtnPauseAll.Text = "Pause All";
            // 
            // tsbtnStartAll
            // 
            this.tsbtnStartAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnStartAll.Image")));
            this.tsbtnStartAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStartAll.Name = "tsbtnStartAll";
            this.tsbtnStartAll.Size = new System.Drawing.Size(68, 22);
            this.tsbtnStartAll.Text = "Start All";
            // 
            // tpEventLogs
            // 
            this.tpEventLogs.Controls.Add(this.dgvEventLogs);
            this.tpEventLogs.Location = new System.Drawing.Point(4, 22);
            this.tpEventLogs.Name = "tpEventLogs";
            this.tpEventLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tpEventLogs.Size = new System.Drawing.Size(637, 155);
            this.tpEventLogs.TabIndex = 1;
            this.tpEventLogs.Text = "Event Logs";
            this.tpEventLogs.UseVisualStyleBackColor = true;
            // 
            // dgvEventLogs
            // 
            this.dgvEventLogs.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvEventLogs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvEventLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEventLogs.Location = new System.Drawing.Point(3, 3);
            this.dgvEventLogs.Name = "dgvEventLogs";
            this.dgvEventLogs.Size = new System.Drawing.Size(631, 149);
            this.dgvEventLogs.TabIndex = 0;
            // 
            // msTop
            // 
            this.msTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiVietnameseSites,
            this.tsmiEnglishSites,
            this.tsmiHelp});
            this.msTop.Location = new System.Drawing.Point(0, 0);
            this.msTop.Name = "msTop";
            this.msTop.Size = new System.Drawing.Size(645, 24);
            this.msTop.TabIndex = 1;
            this.msTop.Text = "menuStrip1";
            // 
            // tsmiVietnameseSites
            // 
            this.tsmiVietnameseSites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBlogTruyen});
            this.tsmiVietnameseSites.Name = "tsmiVietnameseSites";
            this.tsmiVietnameseSites.Size = new System.Drawing.Size(80, 20);
            this.tsmiVietnameseSites.Text = "Vietnamese";
            // 
            // tsmiBlogTruyen
            // 
            this.tsmiBlogTruyen.Name = "tsmiBlogTruyen";
            this.tsmiBlogTruyen.Size = new System.Drawing.Size(152, 22);
            this.tsmiBlogTruyen.Text = "BlogTruyen";
            // 
            // tsmiEnglishSites
            // 
            this.tsmiEnglishSites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMangaFox});
            this.tsmiEnglishSites.Name = "tsmiEnglishSites";
            this.tsmiEnglishSites.Size = new System.Drawing.Size(57, 20);
            this.tsmiEnglishSites.Text = "English";
            // 
            // tsmiMangaFox
            // 
            this.tsmiMangaFox.Name = "tsmiMangaFox";
            this.tsmiMangaFox.Size = new System.Drawing.Size(152, 22);
            this.tsmiMangaFox.Text = "Manga Fox";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContactMe,
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(44, 20);
            this.tsmiHelp.Text = "Help";
            // 
            // tsmiContactMe
            // 
            this.tsmiContactMe.Name = "tsmiContactMe";
            this.tsmiContactMe.Size = new System.Drawing.Size(152, 22);
            this.tsmiContactMe.Text = "Contact Me";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(152, 22);
            this.tsmiAbout.Text = "About";
            // 
            // tsmiMangaViewOnline
            // 
            this.tsmiMangaViewOnline.Name = "tsmiMangaViewOnline";
            this.tsmiMangaViewOnline.Size = new System.Drawing.Size(148, 22);
            this.tsmiMangaViewOnline.Text = "View Online";
            // 
            // tsmiMangaCopyURL
            // 
            this.tsmiMangaCopyURL.Name = "tsmiMangaCopyURL";
            this.tsmiMangaCopyURL.Size = new System.Drawing.Size(148, 22);
            this.tsmiMangaCopyURL.Text = "Copy URL";
            // 
            // tsmiMangaAddToQueue
            // 
            this.tsmiMangaAddToQueue.Name = "tsmiMangaAddToQueue";
            this.tsmiMangaAddToQueue.Size = new System.Drawing.Size(148, 22);
            this.tsmiMangaAddToQueue.Text = "Add to Queue";
            // 
            // tsmiChapterViewOnline
            // 
            this.tsmiChapterViewOnline.Name = "tsmiChapterViewOnline";
            this.tsmiChapterViewOnline.Size = new System.Drawing.Size(148, 22);
            this.tsmiChapterViewOnline.Text = "View Online";
            // 
            // tsmiChapterCopyURL
            // 
            this.tsmiChapterCopyURL.Name = "tsmiChapterCopyURL";
            this.tsmiChapterCopyURL.Size = new System.Drawing.Size(148, 22);
            this.tsmiChapterCopyURL.Text = "Copy URL";
            // 
            // tsmiChapterAddToQueue
            // 
            this.tsmiChapterAddToQueue.Name = "tsmiChapterAddToQueue";
            this.tsmiChapterAddToQueue.Size = new System.Drawing.Size(148, 22);
            this.tsmiChapterAddToQueue.Text = "Add to Queue";
            // 
            // colMangaNo
            // 
            this.colMangaNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colMangaNo.DefaultCellStyle = dataGridViewCellStyle6;
            this.colMangaNo.HeaderText = "No";
            this.colMangaNo.Name = "colMangaNo";
            this.colMangaNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colMangaNo.Width = 46;
            // 
            // colMangaID
            // 
            this.colMangaID.HeaderText = "ID";
            this.colMangaID.Name = "colMangaID";
            this.colMangaID.Visible = false;
            // 
            // colMangaName
            // 
            this.colMangaName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMangaName.HeaderText = "Name";
            this.colMangaName.Name = "colMangaName";
            // 
            // colMangaURL
            // 
            this.colMangaURL.HeaderText = "URL";
            this.colMangaURL.Name = "colMangaURL";
            this.colMangaURL.Visible = false;
            // 
            // colMangaSite
            // 
            this.colMangaSite.HeaderText = "Site";
            this.colMangaSite.Name = "colMangaSite";
            this.colMangaSite.Visible = false;
            // 
            // colChapterNo
            // 
            this.colChapterNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colChapterNo.HeaderText = "No";
            this.colChapterNo.Name = "colChapterNo";
            this.colChapterNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChapterNo.Width = 46;
            // 
            // colChapterID
            // 
            this.colChapterID.HeaderText = "ID";
            this.colChapterID.Name = "colChapterID";
            this.colChapterID.Visible = false;
            // 
            // colChapterName
            // 
            this.colChapterName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colChapterName.HeaderText = "Name";
            this.colChapterName.Name = "colChapterName";
            this.colChapterName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colChapterURL
            // 
            this.colChapterURL.HeaderText = "URL";
            this.colChapterURL.Name = "colChapterURL";
            this.colChapterURL.Visible = false;
            // 
            // colChapterSite
            // 
            this.colChapterSite.HeaderText = "Site";
            this.colChapterSite.Name = "colChapterSite";
            this.colChapterSite.Visible = false;
            // 
            // colTaskID
            // 
            this.colTaskID.HeaderText = "ID";
            this.colTaskID.Name = "colTaskID";
            // 
            // colTaskName
            // 
            this.colTaskName.HeaderText = "Name";
            this.colTaskName.Name = "colTaskName";
            // 
            // colTaskStatus
            // 
            this.colTaskStatus.HeaderText = "Status";
            this.colTaskStatus.Name = "colTaskStatus";
            // 
            // colTaskProgress
            // 
            this.colTaskProgress.HeaderText = "Progress";
            this.colTaskProgress.Name = "colTaskProgress";
            // 
            // colTaskDownloadTo
            // 
            this.colTaskDownloadTo.HeaderText = "Download to";
            this.colTaskDownloadTo.Name = "colTaskDownloadTo";
            this.colTaskDownloadTo.Visible = false;
            // 
            // colTaskType
            // 
            this.colTaskType.HeaderText = "Type";
            this.colTaskType.Name = "colTaskType";
            this.colTaskType.Visible = false;
            // 
            // colTaskSite
            // 
            this.colTaskSite.HeaderText = "Site";
            this.colTaskSite.Name = "colTaskSite";
            // 
            // colTaskURL
            // 
            this.colTaskURL.HeaderText = "URL";
            this.colTaskURL.Name = "colTaskURL";
            // 
            // colTaskDescription
            // 
            this.colTaskDescription.HeaderText = "Description";
            this.colTaskDescription.Name = "colTaskDescription";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 447);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.msTop);
            this.Name = "MainForm";
            this.Text = "Manga Downloader";
            this.Load += new System.EventHandler(this.Main_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.scList.Panel1.ResumeLayout(false);
            this.scList.Panel1.PerformLayout();
            this.scList.Panel2.ResumeLayout(false);
            this.scList.Panel2.PerformLayout();
            this.scList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMangaList)).EndInit();
            this.cmsMangaMenu.ResumeLayout(false);
            this.tsMangaCommands.ResumeLayout(false);
            this.tsMangaCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChapterList)).EndInit();
            this.cmsChapterMenu.ResumeLayout(false);
            this.tsChapterNavigation.ResumeLayout(false);
            this.tsChapterNavigation.PerformLayout();
            this.tcTasks.ResumeLayout(false);
            this.tpTasks.ResumeLayout(false);
            this.tpTasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.tsTaskCommands.ResumeLayout(false);
            this.tsTaskCommands.PerformLayout();
            this.tpEventLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventLogs)).EndInit();
            this.msTop.ResumeLayout(false);
            this.msTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.SplitContainer scList;
        private System.Windows.Forms.TabControl tcTasks;
        private System.Windows.Forms.TabPage tpTasks;
        private System.Windows.Forms.TabPage tpEventLogs;
        private System.Windows.Forms.MenuStrip msTop;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiContactMe;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiVietnameseSites;
        private System.Windows.Forms.ToolStripMenuItem tsmiBlogTruyen;
        private System.Windows.Forms.ToolStripMenuItem tsmiEnglishSites;
        private System.Windows.Forms.ToolStripMenuItem tsmiMangaFox;
        private System.Windows.Forms.ToolStrip tsTaskCommands;
        private System.Windows.Forms.ToolStripButton tsbtnStartAll;
        private System.Windows.Forms.ToolStripButton tsbtnPauseAll;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.ToolStrip tsMangaCommands;
        private System.Windows.Forms.ToolStripLabel tslbSiteName;
        private System.Windows.Forms.ToolStripButton tsbtnRefreshMangaList;
        private System.Windows.Forms.ToolStrip tsChapterNavigation;
        private System.Windows.Forms.ToolStripLabel tslbPathLabel;
        private System.Windows.Forms.ToolStripButton tsbtnPath;
        private System.Windows.Forms.DataGridView dgvChapterList;
        private System.Windows.Forms.ContextMenuStrip cmsTaskMenu;
        private System.Windows.Forms.DataGridView dgvMangaList;
        private System.Windows.Forms.DataGridView dgvEventLogs;
        private System.Windows.Forms.ContextMenuStrip cmsMangaMenu;
        private System.Windows.Forms.ContextMenuStrip cmsChapterMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiMangaViewOnline;
        private System.Windows.Forms.ToolStripMenuItem tsmiMangaCopyURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiMangaAddToQueue;
        private System.Windows.Forms.ToolStripMenuItem tsmiChapterViewOnline;
        private System.Windows.Forms.ToolStripMenuItem tsmiChapterCopyURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiChapterAddToQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMangaNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMangaID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMangaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMangaURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMangaSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChapterNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChapterID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChapterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChapterURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChapterSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskProgress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskDownloadTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskDescription;

    }
}