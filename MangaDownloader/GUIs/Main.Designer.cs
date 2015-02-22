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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scList = new System.Windows.Forms.SplitContainer();
            this.dgvMangaList = new System.Windows.Forms.DataGridView();
            this.colMangaNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMangaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMangaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMangaURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMangaSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsMangaMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMangaAddToQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMangaCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMangaViewOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMangaCommands = new System.Windows.Forms.ToolStrip();
            this.tslbSiteName = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnRefreshMangaList = new System.Windows.Forms.ToolStripButton();
            this.tstbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.dgvChapterList = new System.Windows.Forms.DataGridView();
            this.colChapterNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChapterLinkType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsChapterMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiChapterAddToQueue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChapterCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChapterViewOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsChapterNavigation = new System.Windows.Forms.ToolStrip();
            this.tsbtnManga = new System.Windows.Forms.ToolStripButton();
            this.tslbSlash = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnChapter = new System.Windows.Forms.ToolStripButton();
            this.tslbLoading = new System.Windows.Forms.ToolStripLabel();
            this.tcTasks = new System.Windows.Forms.TabControl();
            this.tpTasks = new System.Windows.Forms.TabPage();
            this.dgvTaskList = new System.Windows.Forms.DataGridView();
            this.colTaskID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskProgress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskSaveTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaskDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsTaskMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTaskOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTaskSaveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTaskReDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTaskDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTaskStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTaskReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTaskSkip = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTaskRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTaskViewOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTaskCommands = new System.Windows.Forms.ToolStrip();
            this.tsbtnStartAll = new System.Windows.Forms.ToolStripButton();
            this.tsbtnStopAll = new System.Windows.Forms.ToolStripButton();
            this.tsbtTaskMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbtTaskMoveDown = new System.Windows.Forms.ToolStripButton();
            this.tpEventLogs = new System.Windows.Forms.TabPage();
            this.dgvEventLogs = new System.Windows.Forms.DataGridView();
            this.msTop = new System.Windows.Forms.MenuStrip();
            this.tsmiVietnameseSites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBlogTruyen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVeChai = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEnglishSites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMangaFox = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiContactMe = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scList)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).BeginInit();
            this.cmsTaskMenu.SuspendLayout();
            this.tsTaskCommands.SuspendLayout();
            this.tpEventLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventLogs)).BeginInit();
            this.msTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 25);
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
            this.scMain.Size = new System.Drawing.Size(753, 455);
            this.scMain.SplitterDistance = 247;
            this.scMain.SplitterWidth = 6;
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
            this.scList.Size = new System.Drawing.Size(753, 247);
            this.scList.SplitterDistance = 269;
            this.scList.SplitterWidth = 6;
            this.scList.TabIndex = 0;
            // 
            // dgvMangaList
            // 
            this.dgvMangaList.AllowUserToAddRows = false;
            this.dgvMangaList.AllowUserToDeleteRows = false;
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
            this.dgvMangaList.ReadOnly = true;
            this.dgvMangaList.RowHeadersVisible = false;
            this.dgvMangaList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMangaList.Size = new System.Drawing.Size(269, 222);
            this.dgvMangaList.TabIndex = 1;
            this.dgvMangaList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMangaList_CellDoubleClick);
            this.dgvMangaList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMangaList_CellMouseDown);
            // 
            // colMangaNo
            // 
            this.colMangaNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colMangaNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.colMangaNo.HeaderText = "No";
            this.colMangaNo.Name = "colMangaNo";
            this.colMangaNo.ReadOnly = true;
            this.colMangaNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colMangaNo.Width = 47;
            // 
            // colMangaID
            // 
            this.colMangaID.HeaderText = "ID";
            this.colMangaID.Name = "colMangaID";
            this.colMangaID.ReadOnly = true;
            this.colMangaID.Visible = false;
            // 
            // colMangaName
            // 
            this.colMangaName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMangaName.HeaderText = "Name";
            this.colMangaName.Name = "colMangaName";
            this.colMangaName.ReadOnly = true;
            // 
            // colMangaURL
            // 
            this.colMangaURL.HeaderText = "URL";
            this.colMangaURL.Name = "colMangaURL";
            this.colMangaURL.ReadOnly = true;
            this.colMangaURL.Visible = false;
            // 
            // colMangaSite
            // 
            this.colMangaSite.HeaderText = "Site";
            this.colMangaSite.Name = "colMangaSite";
            this.colMangaSite.ReadOnly = true;
            this.colMangaSite.Visible = false;
            // 
            // cmsMangaMenu
            // 
            this.cmsMangaMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMangaAddToQueue,
            this.tsmiMangaCopyURL,
            this.tsmiMangaViewOnline});
            this.cmsMangaMenu.Name = "cmsMangaMenu";
            this.cmsMangaMenu.Size = new System.Drawing.Size(149, 70);
            this.cmsMangaMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMangaMenu_Opening);
            // 
            // tsmiMangaAddToQueue
            // 
            this.tsmiMangaAddToQueue.Image = global::MangaDownloader.Properties.Resources.add;
            this.tsmiMangaAddToQueue.Name = "tsmiMangaAddToQueue";
            this.tsmiMangaAddToQueue.Size = new System.Drawing.Size(148, 22);
            this.tsmiMangaAddToQueue.Text = "Add to Queue";
            this.tsmiMangaAddToQueue.Click += new System.EventHandler(this.tsmiMangaAddToQueue_Click);
            // 
            // tsmiMangaCopyURL
            // 
            this.tsmiMangaCopyURL.Image = global::MangaDownloader.Properties.Resources.copy;
            this.tsmiMangaCopyURL.Name = "tsmiMangaCopyURL";
            this.tsmiMangaCopyURL.Size = new System.Drawing.Size(148, 22);
            this.tsmiMangaCopyURL.Text = "Copy URL";
            this.tsmiMangaCopyURL.Click += new System.EventHandler(this.tsmiMangaCopyURL_Click);
            // 
            // tsmiMangaViewOnline
            // 
            this.tsmiMangaViewOnline.Image = global::MangaDownloader.Properties.Resources.browser;
            this.tsmiMangaViewOnline.Name = "tsmiMangaViewOnline";
            this.tsmiMangaViewOnline.Size = new System.Drawing.Size(148, 22);
            this.tsmiMangaViewOnline.Text = "View Online";
            this.tsmiMangaViewOnline.Click += new System.EventHandler(this.tsmiMangaViewOnline_Click);
            // 
            // tsMangaCommands
            // 
            this.tsMangaCommands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMangaCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbSiteName,
            this.tsbtnRefreshMangaList,
            this.tstbSearch});
            this.tsMangaCommands.Location = new System.Drawing.Point(0, 0);
            this.tsMangaCommands.Name = "tsMangaCommands";
            this.tsMangaCommands.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMangaCommands.Size = new System.Drawing.Size(269, 25);
            this.tsMangaCommands.TabIndex = 0;
            this.tsMangaCommands.Text = "toolStrip1";
            // 
            // tslbSiteName
            // 
            this.tslbSiteName.Image = ((System.Drawing.Image)(resources.GetObject("tslbSiteName.Image")));
            this.tslbSiteName.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tslbSiteName.Name = "tslbSiteName";
            this.tslbSiteName.Size = new System.Drawing.Size(87, 22);
            this.tslbSiteName.Text = "Blog Truyen";
            // 
            // tsbtnRefreshMangaList
            // 
            this.tsbtnRefreshMangaList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefreshMangaList.Image = global::MangaDownloader.Properties.Resources.refresh;
            this.tsbtnRefreshMangaList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefreshMangaList.Name = "tsbtnRefreshMangaList";
            this.tsbtnRefreshMangaList.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefreshMangaList.Text = "Refresh";
            this.tsbtnRefreshMangaList.Click += new System.EventHandler(this.tsbtnRefreshMangaList_Click);
            // 
            // tstbSearch
            // 
            this.tstbSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tstbSearch.Name = "tstbSearch";
            this.tstbSearch.Size = new System.Drawing.Size(148, 25);
            this.tstbSearch.ToolTipText = "Input and press Enter to search manga";
            this.tstbSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tstbSearch_KeyUp);
            // 
            // dgvChapterList
            // 
            this.dgvChapterList.AllowUserToAddRows = false;
            this.dgvChapterList.AllowUserToDeleteRows = false;
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
            this.colChapterSite,
            this.colChapterLinkType});
            this.dgvChapterList.ContextMenuStrip = this.cmsChapterMenu;
            this.dgvChapterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChapterList.Location = new System.Drawing.Point(0, 25);
            this.dgvChapterList.Name = "dgvChapterList";
            this.dgvChapterList.ReadOnly = true;
            this.dgvChapterList.RowHeadersVisible = false;
            this.dgvChapterList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChapterList.Size = new System.Drawing.Size(478, 222);
            this.dgvChapterList.TabIndex = 1;
            this.dgvChapterList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChapterList_CellDoubleClick);
            this.dgvChapterList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvChapterList_CellMouseDown);
            // 
            // colChapterNo
            // 
            this.colChapterNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colChapterNo.HeaderText = "No";
            this.colChapterNo.Name = "colChapterNo";
            this.colChapterNo.ReadOnly = true;
            this.colChapterNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChapterNo.Width = 47;
            // 
            // colChapterID
            // 
            this.colChapterID.HeaderText = "ID";
            this.colChapterID.Name = "colChapterID";
            this.colChapterID.ReadOnly = true;
            this.colChapterID.Visible = false;
            // 
            // colChapterName
            // 
            this.colChapterName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colChapterName.HeaderText = "Name";
            this.colChapterName.Name = "colChapterName";
            this.colChapterName.ReadOnly = true;
            this.colChapterName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colChapterURL
            // 
            this.colChapterURL.HeaderText = "URL";
            this.colChapterURL.Name = "colChapterURL";
            this.colChapterURL.ReadOnly = true;
            this.colChapterURL.Visible = false;
            // 
            // colChapterSite
            // 
            this.colChapterSite.HeaderText = "Site";
            this.colChapterSite.Name = "colChapterSite";
            this.colChapterSite.ReadOnly = true;
            this.colChapterSite.Visible = false;
            // 
            // colChapterLinkType
            // 
            this.colChapterLinkType.HeaderText = "Link Type";
            this.colChapterLinkType.Name = "colChapterLinkType";
            this.colChapterLinkType.ReadOnly = true;
            this.colChapterLinkType.Visible = false;
            // 
            // cmsChapterMenu
            // 
            this.cmsChapterMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChapterAddToQueue,
            this.tsmiChapterCopyURL,
            this.tsmiChapterViewOnline});
            this.cmsChapterMenu.Name = "cmsChapterMenu";
            this.cmsChapterMenu.Size = new System.Drawing.Size(149, 70);
            this.cmsChapterMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsChapterMenu_Opening);
            // 
            // tsmiChapterAddToQueue
            // 
            this.tsmiChapterAddToQueue.Image = global::MangaDownloader.Properties.Resources.add;
            this.tsmiChapterAddToQueue.Name = "tsmiChapterAddToQueue";
            this.tsmiChapterAddToQueue.Size = new System.Drawing.Size(148, 22);
            this.tsmiChapterAddToQueue.Text = "Add to Queue";
            this.tsmiChapterAddToQueue.Click += new System.EventHandler(this.tsmiChapterAddToQueue_Click);
            // 
            // tsmiChapterCopyURL
            // 
            this.tsmiChapterCopyURL.Image = global::MangaDownloader.Properties.Resources.copy;
            this.tsmiChapterCopyURL.Name = "tsmiChapterCopyURL";
            this.tsmiChapterCopyURL.Size = new System.Drawing.Size(148, 22);
            this.tsmiChapterCopyURL.Text = "Copy URL";
            this.tsmiChapterCopyURL.Click += new System.EventHandler(this.tsmiChapterCopyURL_Click);
            // 
            // tsmiChapterViewOnline
            // 
            this.tsmiChapterViewOnline.Image = global::MangaDownloader.Properties.Resources.browser;
            this.tsmiChapterViewOnline.Name = "tsmiChapterViewOnline";
            this.tsmiChapterViewOnline.Size = new System.Drawing.Size(148, 22);
            this.tsmiChapterViewOnline.Text = "View Online";
            this.tsmiChapterViewOnline.Click += new System.EventHandler(this.tsmiChapterViewOnline_Click);
            // 
            // tsChapterNavigation
            // 
            this.tsChapterNavigation.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsChapterNavigation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnManga,
            this.tslbSlash,
            this.tsbtnChapter,
            this.tslbLoading});
            this.tsChapterNavigation.Location = new System.Drawing.Point(0, 0);
            this.tsChapterNavigation.Name = "tsChapterNavigation";
            this.tsChapterNavigation.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsChapterNavigation.Size = new System.Drawing.Size(478, 25);
            this.tsChapterNavigation.TabIndex = 0;
            this.tsChapterNavigation.Text = "toolStrip1";
            // 
            // tsbtnManga
            // 
            this.tsbtnManga.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnManga.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnManga.Image")));
            this.tsbtnManga.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnManga.Name = "tsbtnManga";
            this.tsbtnManga.Size = new System.Drawing.Size(82, 22);
            this.tsbtnManga.Text = "Manga: -SIN-";
            this.tsbtnManga.Click += new System.EventHandler(this.tsbtnManga_Click);
            // 
            // tslbSlash
            // 
            this.tslbSlash.Name = "tslbSlash";
            this.tslbSlash.Size = new System.Drawing.Size(12, 22);
            this.tslbSlash.Text = "/";
            // 
            // tsbtnChapter
            // 
            this.tsbtnChapter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnChapter.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnChapter.Image")));
            this.tsbtnChapter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnChapter.Name = "tsbtnChapter";
            this.tsbtnChapter.Size = new System.Drawing.Size(139, 22);
            this.tsbtnChapter.Text = "Chapter: -SIN- One Shot";
            this.tsbtnChapter.Click += new System.EventHandler(this.tsbtnChapter_Click);
            // 
            // tslbLoading
            // 
            this.tslbLoading.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tslbLoading.Image = global::MangaDownloader.Properties.Resources.loading;
            this.tslbLoading.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.tslbLoading.Name = "tslbLoading";
            this.tslbLoading.Size = new System.Drawing.Size(16, 22);
            this.tslbLoading.Text = "toolStripLabel1";
            // 
            // tcTasks
            // 
            this.tcTasks.Controls.Add(this.tpTasks);
            this.tcTasks.Controls.Add(this.tpEventLogs);
            this.tcTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTasks.Location = new System.Drawing.Point(0, 0);
            this.tcTasks.Name = "tcTasks";
            this.tcTasks.SelectedIndex = 0;
            this.tcTasks.Size = new System.Drawing.Size(753, 202);
            this.tcTasks.TabIndex = 0;
            // 
            // tpTasks
            // 
            this.tpTasks.Controls.Add(this.dgvTaskList);
            this.tpTasks.Controls.Add(this.tsTaskCommands);
            this.tpTasks.Location = new System.Drawing.Point(4, 23);
            this.tpTasks.Margin = new System.Windows.Forms.Padding(4);
            this.tpTasks.Name = "tpTasks";
            this.tpTasks.Padding = new System.Windows.Forms.Padding(4);
            this.tpTasks.Size = new System.Drawing.Size(745, 175);
            this.tpTasks.TabIndex = 0;
            this.tpTasks.Text = "Tasks";
            this.tpTasks.UseVisualStyleBackColor = true;
            // 
            // dgvTaskList
            // 
            this.dgvTaskList.AllowUserToAddRows = false;
            this.dgvTaskList.AllowUserToDeleteRows = false;
            this.dgvTaskList.AllowUserToResizeRows = false;
            this.dgvTaskList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTaskList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTaskID,
            this.colTaskName,
            this.colTaskStatus,
            this.colTaskProgress,
            this.colTaskSaveTo,
            this.colTaskType,
            this.colTaskSite,
            this.colTaskURL,
            this.colTaskDescription});
            this.dgvTaskList.ContextMenuStrip = this.cmsTaskMenu;
            this.dgvTaskList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaskList.Location = new System.Drawing.Point(4, 4);
            this.dgvTaskList.Name = "dgvTaskList";
            this.dgvTaskList.ReadOnly = true;
            this.dgvTaskList.RowHeadersVisible = false;
            this.dgvTaskList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaskList.Size = new System.Drawing.Size(737, 142);
            this.dgvTaskList.TabIndex = 2;
            this.dgvTaskList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTaskList_CellMouseDown);
            this.dgvTaskList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvTaskList_KeyUp);
            // 
            // colTaskID
            // 
            this.colTaskID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTaskID.HeaderText = "ID";
            this.colTaskID.Name = "colTaskID";
            this.colTaskID.ReadOnly = true;
            this.colTaskID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskID.Visible = false;
            // 
            // colTaskName
            // 
            this.colTaskName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTaskName.HeaderText = "Name";
            this.colTaskName.Name = "colTaskName";
            this.colTaskName.ReadOnly = true;
            this.colTaskName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskName.Width = 44;
            // 
            // colTaskStatus
            // 
            this.colTaskStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTaskStatus.HeaderText = "Status";
            this.colTaskStatus.Name = "colTaskStatus";
            this.colTaskStatus.ReadOnly = true;
            this.colTaskStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskStatus.Visible = false;
            // 
            // colTaskProgress
            // 
            this.colTaskProgress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTaskProgress.HeaderText = "Status";
            this.colTaskProgress.Name = "colTaskProgress";
            this.colTaskProgress.ReadOnly = true;
            this.colTaskProgress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskProgress.Width = 48;
            // 
            // colTaskSaveTo
            // 
            this.colTaskSaveTo.HeaderText = "Save to";
            this.colTaskSaveTo.Name = "colTaskSaveTo";
            this.colTaskSaveTo.ReadOnly = true;
            this.colTaskSaveTo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskSaveTo.Width = 300;
            // 
            // colTaskType
            // 
            this.colTaskType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTaskType.HeaderText = "Type";
            this.colTaskType.Name = "colTaskType";
            this.colTaskType.ReadOnly = true;
            this.colTaskType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskType.Width = 41;
            // 
            // colTaskSite
            // 
            this.colTaskSite.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTaskSite.HeaderText = "Site";
            this.colTaskSite.Name = "colTaskSite";
            this.colTaskSite.ReadOnly = true;
            this.colTaskSite.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskSite.Width = 34;
            // 
            // colTaskURL
            // 
            this.colTaskURL.HeaderText = "URL";
            this.colTaskURL.Name = "colTaskURL";
            this.colTaskURL.ReadOnly = true;
            this.colTaskURL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskURL.Visible = false;
            // 
            // colTaskDescription
            // 
            this.colTaskDescription.HeaderText = "Description";
            this.colTaskDescription.Name = "colTaskDescription";
            this.colTaskDescription.ReadOnly = true;
            this.colTaskDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTaskDescription.Visible = false;
            // 
            // cmsTaskMenu
            // 
            this.cmsTaskMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTaskOpenFolder,
            this.toolStripSeparator1,
            this.tsmiTaskSaveTo,
            this.toolStripSeparator4,
            this.tsmiTaskReDownload,
            this.toolStripSeparator5,
            this.tsmiTaskDownload,
            this.tsmiTaskStop,
            this.toolStripSeparator3,
            this.tsmiTaskReset,
            this.tsmiTaskSkip,
            this.tsmiTaskRemove,
            this.toolStripSeparator2,
            this.tsmiTaskViewOnline});
            this.cmsTaskMenu.Name = "cmsTaskMenu";
            this.cmsTaskMenu.Size = new System.Drawing.Size(141, 232);
            this.cmsTaskMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsTaskMenu_Opening);
            // 
            // tsmiTaskOpenFolder
            // 
            this.tsmiTaskOpenFolder.Image = global::MangaDownloader.Properties.Resources.folderopen;
            this.tsmiTaskOpenFolder.Name = "tsmiTaskOpenFolder";
            this.tsmiTaskOpenFolder.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskOpenFolder.Text = "Open Folder";
            this.tsmiTaskOpenFolder.Click += new System.EventHandler(this.tsmiTaskOpenFolder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // tsmiTaskSaveTo
            // 
            this.tsmiTaskSaveTo.Name = "tsmiTaskSaveTo";
            this.tsmiTaskSaveTo.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskSaveTo.Text = "Save to...";
            this.tsmiTaskSaveTo.Click += new System.EventHandler(this.tsmiTaskSaveTo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(137, 6);
            // 
            // tsmiTaskReDownload
            // 
            this.tsmiTaskReDownload.Name = "tsmiTaskReDownload";
            this.tsmiTaskReDownload.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskReDownload.Text = "Redownload";
            this.tsmiTaskReDownload.Click += new System.EventHandler(this.tsmiTaskReDownload_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(137, 6);
            // 
            // tsmiTaskDownload
            // 
            this.tsmiTaskDownload.Image = global::MangaDownloader.Properties.Resources.download;
            this.tsmiTaskDownload.Name = "tsmiTaskDownload";
            this.tsmiTaskDownload.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskDownload.Text = "Download";
            this.tsmiTaskDownload.Click += new System.EventHandler(this.tsmiTaskDownload_Click);
            // 
            // tsmiTaskStop
            // 
            this.tsmiTaskStop.Image = global::MangaDownloader.Properties.Resources.player_stop;
            this.tsmiTaskStop.Name = "tsmiTaskStop";
            this.tsmiTaskStop.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskStop.Text = "Stop";
            this.tsmiTaskStop.Click += new System.EventHandler(this.tsmiTaskStop_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(137, 6);
            // 
            // tsmiTaskReset
            // 
            this.tsmiTaskReset.Name = "tsmiTaskReset";
            this.tsmiTaskReset.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskReset.Text = "Reset";
            this.tsmiTaskReset.Click += new System.EventHandler(this.tsmiTaskReset_Click);
            // 
            // tsmiTaskSkip
            // 
            this.tsmiTaskSkip.Name = "tsmiTaskSkip";
            this.tsmiTaskSkip.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskSkip.Text = "Skip";
            this.tsmiTaskSkip.Click += new System.EventHandler(this.tsmiTaskSkip_Click);
            // 
            // tsmiTaskRemove
            // 
            this.tsmiTaskRemove.Name = "tsmiTaskRemove";
            this.tsmiTaskRemove.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskRemove.Text = "Remove";
            this.tsmiTaskRemove.Click += new System.EventHandler(this.tsmiTaskRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(137, 6);
            // 
            // tsmiTaskViewOnline
            // 
            this.tsmiTaskViewOnline.Image = global::MangaDownloader.Properties.Resources.browser;
            this.tsmiTaskViewOnline.Name = "tsmiTaskViewOnline";
            this.tsmiTaskViewOnline.Size = new System.Drawing.Size(140, 22);
            this.tsmiTaskViewOnline.Text = "View Online";
            this.tsmiTaskViewOnline.Click += new System.EventHandler(this.tsmiTaskViewOnline_Click);
            // 
            // tsTaskCommands
            // 
            this.tsTaskCommands.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsTaskCommands.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTaskCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnStartAll,
            this.tsbtnStopAll,
            this.tsbtTaskMoveUp,
            this.tsbtTaskMoveDown});
            this.tsTaskCommands.Location = new System.Drawing.Point(4, 146);
            this.tsTaskCommands.Name = "tsTaskCommands";
            this.tsTaskCommands.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsTaskCommands.Size = new System.Drawing.Size(737, 25);
            this.tsTaskCommands.TabIndex = 1;
            this.tsTaskCommands.Text = "toolStrip1";
            // 
            // tsbtnStartAll
            // 
            this.tsbtnStartAll.Image = global::MangaDownloader.Properties.Resources.multidownload;
            this.tsbtnStartAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStartAll.Name = "tsbtnStartAll";
            this.tsbtnStartAll.Size = new System.Drawing.Size(68, 22);
            this.tsbtnStartAll.Text = "Start All";
            this.tsbtnStartAll.Click += new System.EventHandler(this.tsbtnStartAll_Click);
            // 
            // tsbtnStopAll
            // 
            this.tsbtnStopAll.Image = global::MangaDownloader.Properties.Resources.Stop_All_icon;
            this.tsbtnStopAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStopAll.Name = "tsbtnStopAll";
            this.tsbtnStopAll.Size = new System.Drawing.Size(68, 22);
            this.tsbtnStopAll.Text = "Stop All";
            this.tsbtnStopAll.Click += new System.EventHandler(this.tsbtnStopAll_Click);
            // 
            // tsbtTaskMoveUp
            // 
            this.tsbtTaskMoveUp.Image = global::MangaDownloader.Properties.Resources.arrow_up;
            this.tsbtTaskMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtTaskMoveUp.Name = "tsbtTaskMoveUp";
            this.tsbtTaskMoveUp.Size = new System.Drawing.Size(75, 22);
            this.tsbtTaskMoveUp.Text = "Move Up";
            this.tsbtTaskMoveUp.Click += new System.EventHandler(this.tsbtTaskMoveUp_Click);
            // 
            // tsbtTaskMoveDown
            // 
            this.tsbtTaskMoveDown.Image = global::MangaDownloader.Properties.Resources.arrow_down;
            this.tsbtTaskMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtTaskMoveDown.Name = "tsbtTaskMoveDown";
            this.tsbtTaskMoveDown.Size = new System.Drawing.Size(91, 22);
            this.tsbtTaskMoveDown.Text = "Move Down";
            this.tsbtTaskMoveDown.Click += new System.EventHandler(this.tsbtTaskMoveDown_Click);
            // 
            // tpEventLogs
            // 
            this.tpEventLogs.Controls.Add(this.dgvEventLogs);
            this.tpEventLogs.Location = new System.Drawing.Point(4, 23);
            this.tpEventLogs.Margin = new System.Windows.Forms.Padding(4);
            this.tpEventLogs.Name = "tpEventLogs";
            this.tpEventLogs.Padding = new System.Windows.Forms.Padding(4);
            this.tpEventLogs.Size = new System.Drawing.Size(745, 175);
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
            this.dgvEventLogs.Location = new System.Drawing.Point(4, 4);
            this.dgvEventLogs.Name = "dgvEventLogs";
            this.dgvEventLogs.Size = new System.Drawing.Size(737, 167);
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
            this.msTop.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.msTop.Size = new System.Drawing.Size(753, 25);
            this.msTop.TabIndex = 1;
            this.msTop.Text = "menuStrip1";
            // 
            // tsmiVietnameseSites
            // 
            this.tsmiVietnameseSites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBlogTruyen,
            this.tsmiVeChai});
            this.tsmiVietnameseSites.Name = "tsmiVietnameseSites";
            this.tsmiVietnameseSites.Size = new System.Drawing.Size(80, 19);
            this.tsmiVietnameseSites.Text = "Vietnamese";
            // 
            // tsmiBlogTruyen
            // 
            this.tsmiBlogTruyen.Image = ((System.Drawing.Image)(resources.GetObject("tsmiBlogTruyen.Image")));
            this.tsmiBlogTruyen.Name = "tsmiBlogTruyen";
            this.tsmiBlogTruyen.Size = new System.Drawing.Size(135, 22);
            this.tsmiBlogTruyen.Text = "BlogTruyen";
            this.tsmiBlogTruyen.Click += new System.EventHandler(this.tsmiBlogTruyen_Click);
            // 
            // tsmiVeChai
            // 
            this.tsmiVeChai.Image = global::MangaDownloader.Properties.Resources.vechai_logo;
            this.tsmiVeChai.Name = "tsmiVeChai";
            this.tsmiVeChai.Size = new System.Drawing.Size(135, 22);
            this.tsmiVeChai.Text = "VeChai";
            this.tsmiVeChai.Click += new System.EventHandler(this.tsmiVeChai_Click);
            // 
            // tsmiEnglishSites
            // 
            this.tsmiEnglishSites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMangaFox});
            this.tsmiEnglishSites.Name = "tsmiEnglishSites";
            this.tsmiEnglishSites.Size = new System.Drawing.Size(57, 19);
            this.tsmiEnglishSites.Text = "English";
            // 
            // tsmiMangaFox
            // 
            this.tsmiMangaFox.Image = global::MangaDownloader.Properties.Resources.mangafox_logo;
            this.tsmiMangaFox.Name = "tsmiMangaFox";
            this.tsmiMangaFox.Size = new System.Drawing.Size(132, 22);
            this.tsmiMangaFox.Text = "Manga Fox";
            this.tsmiMangaFox.Click += new System.EventHandler(this.tsmiMangaFox_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContactMe,
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(44, 19);
            this.tsmiHelp.Text = "Help";
            // 
            // tsmiContactMe
            // 
            this.tsmiContactMe.Name = "tsmiContactMe";
            this.tsmiContactMe.Size = new System.Drawing.Size(136, 22);
            this.tsmiContactMe.Text = "Contact Me";
            this.tsmiContactMe.Click += new System.EventHandler(this.tsmiContactMe_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(136, 22);
            this.tsmiAbout.Text = "About";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 480);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.msTop);
            this.Name = "MainForm";
            this.Text = "Manga Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scList.Panel1.ResumeLayout(false);
            this.scList.Panel1.PerformLayout();
            this.scList.Panel2.ResumeLayout(false);
            this.scList.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scList)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskList)).EndInit();
            this.cmsTaskMenu.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripButton tsbtnStopAll;
        private System.Windows.Forms.DataGridView dgvTaskList;
        private System.Windows.Forms.ToolStrip tsMangaCommands;
        private System.Windows.Forms.ToolStripLabel tslbSiteName;
        private System.Windows.Forms.ToolStripButton tsbtnRefreshMangaList;
        private System.Windows.Forms.ToolStrip tsChapterNavigation;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colChapterLinkType;
        private System.Windows.Forms.ToolStripButton tsbtnManga;
        private System.Windows.Forms.ToolStripLabel tslbSlash;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskOpenFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskDownload;
        private System.Windows.Forms.ToolStripLabel tslbLoading;
        private System.Windows.Forms.ToolStripButton tsbtnChapter;
        private System.Windows.Forms.ToolStripMenuItem tsmiVeChai;
        private System.Windows.Forms.ToolStripTextBox tstbSearch;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskStop;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskViewOnline;
        private System.Windows.Forms.ToolStripButton tsbtTaskMoveUp;
        private System.Windows.Forms.ToolStripButton tsbtTaskMoveDown;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskSkip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskSaveTo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskReset;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskProgress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskSaveTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaskDescription;
        private System.Windows.Forms.ToolStripMenuItem tsmiTaskReDownload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

    }
}