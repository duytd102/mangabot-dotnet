using Common;
using Common.Enums;
using MangaDownloader.Enums;
using MangaDownloader.Settings;
using MangaDownloader.Utils;
using MangaDownloader.Workers;
using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WebScraper.Data;
using WebScraper.Processors;

namespace MangaDownloader.GUIs
{
    public partial class MainForm : BaseForm
    {
        const String COLUMN_MANGA_NO = "colMangaNo";
        const String COLUMN_MANGA_ID = "colMangaID";
        const String COLUMN_MANGA_NAME = "colMangaName";
        const String COLUMN_MANGA_URL = "colMangaUrl";
        const String COLUMN_MANGA_SITE = "colMangaSite";

        const String COLUMN_CHAPTER_NO = "colChapterNo";
        const String COLUMN_CHAPTER_ID = "colChapterID";
        const String COLUMN_CHAPTER_NAME = "colChapterName";
        const String COLUMN_CHAPTER_URL = "colChapterUrl";
        const String COLUMN_CHAPTER_SITE = "colChapterSite";
        const String COLUMN_CHAPTER_LINK_TYPE = "colChapterLinkType";

        const String COLUMN_TASK_ID = "colTaskNo";
        const String COLUMN_TASK_NAME = "colTaskName";
        const String COLUMN_TASK_STATUS = "colTaskStatus";
        const String COLUMN_TASK_PROGRESS = "colTaskProgress";
        const String COLUMN_TASK_SAVE_TO = "colTaskSaveTo";
        const String COLUMN_TASK_LINK_TYPE = "colTaskType";
        const String COLUMN_TASK_SITE = "colTaskSite";
        const String COLUMN_TASK_URL = "colTaskURL";
        const String COLUMN_TASK_DESCRIPTION = "colTaskDescription";

        const String AUTO_UPDATE_APP_NAME = "Auto Update.exe";

        BackgroundWorker mangaWorker;
        BackgroundWorker chapterWorker;
        BackgroundWorker pageWorker;
        MangaSite currentSite = MangaSite.BLOGTRUYEN;
        QueueWorkerManager workerManager = QueueWorkerManager.GetInstance();
        List<Manga> mangaList = new List<Manga>();
        List<Task> taskList = new List<Task>();
        WorkerHandlers workerHandlers = new WorkerHandlers();
        bool IsClosingForm = false;
        int concurrentWorkersLimit = 3;
        bool isStoppingQueue = false;
        String newAutoUpdatePath;
        BackgroundWorker autoUpdateMangaListWorker = new BackgroundWorker();
        IProcessor autoUpdateProcessor;
        MangaSite autoUpdateMangaSite = MangaSite.UNKNOWN;
        List<Manga> autoUpdateMangaList = new List<Manga>();

        public MainForm()
            : base()
        {
            InitializeComponent();
        }

        public MainForm(String autoUpdateFilePath)
            : base()
        {
            InitializeComponent();
            newAutoUpdatePath = autoUpdateFilePath;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            dgvMangaList.AutoGenerateColumns = false;

            tsbtnManga.Visible = false;
            tslbSlash.Visible = false;
            tsbtnChapter.Visible = false;
            tslbLoading.Visible = false;
            tsbtnStartAll.Enabled = true;
            tsbtnStopAll.Enabled = false;
            tsmiNewVersion.Visible = false;

            SettingsManager.Import();

            EnableOrDisableTurnOffComputerOption();

            currentSite = CommonSettings.LatestSite();

            SettingsManager sm = SettingsManager.GetInstance();

            this.Text = String.Format("{0} v{1}", CommonSettings.AppName(), CommonSettings.AppVersion());

            concurrentWorkersLimit = sm.GetAppSettings().TotalConcurrentWorkers;

            workerManager.AllWorkersStopped += workerManager_AllWorkersStopped;

            workerHandlers.Downloading = OnWorkerDownloading;
            workerHandlers.ProgressChanged = OnWorkerProgressChanged;
            workerHandlers.Complete = OnWorkerComplete;
            workerHandlers.Cancelled = OnWorkerCancelled;
            workerHandlers.Failed = OnWorkerFailed;

            taskList = workerManager.GetTaskList();

            //loadingForm = new Loading("Loading manga list... please wait");
            //loadingForm.Show();

            initWorkers();

            Thread loadThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    setCurrentSite(currentSite);
                    importTaskList();
                    //loadingForm.Invoke(new MethodInvoker(() => { loadingForm.Close(); }));
                }
                catch { }
            }));
            loadThread.IsBackground = true;
            loadThread.Start();

            Thread gaThread = new Thread(new ThreadStart(() =>
            {
                GoogleAnalyticsUtils.SendView(CommonSettings.AppName(), CommonSettings.AppVersion(), CommonSettings.MainScreen());
            }));
            gaThread.IsBackground = true;
            gaThread.Start();

            Thread versionThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    DateTime cv = CommonSettings.CheckVersionDate();
                    if (cv.Date < DateTime.Now.Date)
                    {
                        VersionData vd;
                        if (VersionUtils.CheckForUpdates(out vd))
                        {
                            UpdateVersionHighlight(vd.Version, vd.URL);

                            CommonSettings.SetDateAfterCheckVersion(DateTime.Now.Date);
                            CommonSettings.SetNextVersion(vd.Version);
                            CommonSettings.SetNextVersionURL(vd.URL);

                            //ConfigurationData sd = SettingsManager.GetInstance().GetAppSettings();
                            //string ignoreVersion = sd.IgnoreVersion;
                            //if (!ignoreVersion.Equals(vd.Version))
                            //{
                            //    RunAutoUpdate();
                            //}
                        }
                    }
                    else
                    {
                        if (CommonSettings.AppVersion().Equals(CommonSettings.NextVersion()) == false)
                        {
                            UpdateVersionHighlight(CommonSettings.NextVersion(), CommonSettings.NextVersionURL());
                        }
                    }
                }
                catch { }
            }));
            versionThread.IsBackground = true;
            versionThread.Start();

            AutoUpdateMangaList();

            System.Timers.Timer t = new System.Timers.Timer(5000);
            t.Elapsed += t_Elapsed;
            t.AutoReset = false;
            t.Start();
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (File.Exists(newAutoUpdatePath))
            {
                File.SetAttributes(newAutoUpdatePath, FileAttributes.Normal);
                String fn = String.Format("{0}\\{1}", Application.StartupPath, AUTO_UPDATE_APP_NAME);
                File.Copy(newAutoUpdatePath, fn, true);
                File.Delete(newAutoUpdatePath);
            }
        }

        private void UpdateVersionHighlight(string version, string url)
        {
            msTop.Invoke(new MethodInvoker(() =>
            {
                tsmiNewVersion.Text = "New version " + version;
                tsmiNewVersion.Tag = url;
                tsmiNewVersion.Visible = true;
            }));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigurationData sd = SettingsManager.GetInstance().GetAppSettings();
            if (sd.MinimizeTaskbar)
            {
                e.Cancel = true;
                if (this.WindowState != FormWindowState.Minimized)
                {
                    this.Hide();
                    this.Tag = this.WindowState;
                    this.WindowState = FormWindowState.Minimized;
                    notifyIcon.Visible = true;

                    if (SettingsManager.GetInstance().GetAppSettings().ShowTaskbarInfoOnMinimize)
                    {
                        ShowPopup("Manga Downloader is still running",
                            "Manga Downloader is still running, so your manga will still be able to download." +
                            "\n\nClick here to disable this message in the future.", ToolTipIcon.Info);
                    }
                }
            }
        }

        void workerManager_AllWorkersStopped()
        {
            tsTaskCommands.Invoke(new MethodInvoker(() =>
            {
                tsbtnStartAll.Enabled = true;
                tsbtnStopAll.Enabled = false;
            }));

            if (IsClosingForm)
            {
                Application.Exit();
            }
            else if (!isStoppingQueue && SettingsManager.GetInstance().GetAppSettings().AutoShutdown)
            {
                WindowUtils.TurnOffComputer();
            }

            isStoppingQueue = false;
        }

        private void ImportMangaList()
        {
            mangaList = MangaUtils.Import(currentSite);
            UpdateMangaListGridView(mangaList);
        }

        private void tsmiBlogTruyen_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.BLOGTRUYEN);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiVeChai_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.TRUYENTRANHNET);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiMangaVN_Click(object sender, EventArgs e)
        {

        }

        private void tsmiMangaFox_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.MANGAFOX);
            tsMangaCommands_Resize(sender, e);
        }

        private void setCurrentSite(MangaSite site)
        {
            // TODO how to cancel manga worker???
            if (mangaWorker.IsBusy) mangaWorker.CancelAsync();

            currentSite = site;
            CommonSettings.SetLatestSite(site);

            tslbSiteLogo.Image = MangaUtils.GetLogo(currentSite);
            tslbSiteLogo.Text = EnumUtils.Capitalize(site);

            ImportMangaList();
        }

        private void tsbtnStartAll_Click(object sender, EventArgs e)
        {
            tsbtnStartAll.Enabled = false;
            tsbtnStopAll.Enabled = true;
            isStoppingQueue = false;
            ResetTaskStatusBeforeDownload();
            workerManager.StartQueue(concurrentWorkersLimit, workerHandlers);
        }

        private void ResetTaskStatusBeforeDownload()
        {
            foreach (DataGridViewRow row in dgvTaskList.Rows)
            {
                TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                if (SomeRules.CanDownloadTask(status))
                {
                    row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.QUEUED);
                    row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.QUEUED, 0);

                    Task task = taskList.Find(p => p.Url.Equals(url));
                    if (task != null)
                    {
                        task.Status = TaskStatus.QUEUED;
                        task.Percent = 0;
                    }
                }
            }
            TaskUtils.Export(taskList);
        }

        private void tsbtnStopAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to stop all tasks?", "Stop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                isStoppingQueue = true;
                workerManager.StopQueue();
            }
        }

        private void tsbtnManga_Click(object sender, EventArgs e)
        {
            if (chapterWorker.IsBusy) return;

            tslbSlash.Visible = false;
            tsbtnChapter.Visible = false;
            tslbLoading.Visible = true;

            ToolStripButton btnManga = (ToolStripButton)sender;
            String mangaUrl = btnManga.Tag.ToString();
            chapterWorker.RunWorkerAsync(mangaUrl);
        }

        private void tsbtnChapter_Click(object sender, EventArgs e)
        {
            if (pageWorker.IsBusy) return;

            tslbLoading.Visible = true;

            ToolStripButton btnChapter = (ToolStripButton)sender;
            String chapterUrl = btnChapter.Tag.ToString();
            pageWorker.RunWorkerAsync(chapterUrl);
        }

        private void dgvMangaList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (chapterWorker.IsBusy) return;
            if (dgvMangaList.SelectedRows.Count == 0) return;

            String mangaName = dgvMangaList.SelectedRows[0].Cells[COLUMN_MANGA_NAME].Value.ToString();
            String mangaUrl = dgvMangaList.SelectedRows[0].Cells[COLUMN_MANGA_URL].Value.ToString();

            tslbSlash.Visible = false;
            tsbtnChapter.Visible = false;
            tsbtnManga.Visible = true;
            tsbtnManga.Text = mangaName;
            tsbtnManga.Tag = mangaUrl;
            tslbLoading.Visible = true;

            chapterWorker.RunWorkerAsync(mangaUrl);
        }

        private void dgvChapterList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pageWorker.IsBusy) return;
            if (dgvChapterList.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvChapterList.SelectedRows[0];
            LinkType linkType = EnumUtils.Parse<LinkType>(row.Cells[COLUMN_CHAPTER_LINK_TYPE].Value.ToString());
            String chapterName = row.Cells[COLUMN_CHAPTER_NAME].Value.ToString();
            String chapterUrl = row.Cells[COLUMN_CHAPTER_URL].Value.ToString();

            if (linkType == LinkType.CHAPTER)
            {
                tslbSlash.Visible = true;
                tsbtnChapter.Visible = true;
                tsbtnChapter.Text = chapterName;
                tsbtnChapter.Tag = chapterUrl;
                tslbLoading.Visible = true;

                pageWorker.RunWorkerAsync(chapterUrl);
            }
        }

        private void initWorkers()
        {
            mangaWorker = new BackgroundWorker();
            mangaWorker.WorkerSupportsCancellation = true;
            mangaWorker.WorkerReportsProgress = true;
            mangaWorker.DoWork += mangaWorker_DoWork;
            mangaWorker.RunWorkerCompleted += mangaWorker_RunWorkerCompleted;

            chapterWorker = new BackgroundWorker();
            chapterWorker.WorkerSupportsCancellation = true;
            chapterWorker.DoWork += chapterWorker_DoWork;
            chapterWorker.RunWorkerCompleted += chapterWorker_RunWorkerCompleted;

            pageWorker = new BackgroundWorker();
            pageWorker.WorkerSupportsCancellation = true;
            pageWorker.DoWork += pageWorker_DoWork;
            pageWorker.RunWorkerCompleted += pageWorker_RunWorkerCompleted;
        }

        private void importTaskList()
        {
            taskList.Clear();
            taskList.AddRange(TaskUtils.Import());
            foreach (var task in taskList)
            {
                if (task.Status == TaskStatus.DOWNLOADING)
                {
                    task.Status = TaskStatus.STOPPED;
                }
            }
            TaskUtils.Export(taskList);
            foreach (Task t in taskList)
                AddToQueue(t);
        }

        void mangaWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            mangaList.Clear();

            IProcessor processor = ProcessorFactory.CreateProcessor(currentSite);
            processor.ScrapOneMangaPageComplete += processor_ScrapOneMangaPageComplete;
            e.Result = processor.GetMangaList();
        }

        void processor_ScrapOneMangaPageComplete(IProcessor invoker, int totalManga, int totalPages, int pageIndex, List<Manga> partialList)
        {
            if (mangaWorker.CancellationPending == false)
            {
                mangaList.AddRange(partialList);
                MangaUtils.Export(currentSite, mangaList);
                AppendMangaListGridView(totalManga, partialList);
            }
            else
            {
                invoker.Cancel();
            }
        }

        void mangaWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tslbMangaLoading.Visible = false;
            tsbtSiteUpdate.Visible = true;
            ConfigurationIO.WriteSiteUpdatedDate(currentSite, DateTime.Now.Date);
        }

        private void UpdateMangaListGridView(List<Manga> mangaList)
        {
            DataTable dt = CreateDataSource();

            int currentRowIndex = 0;
            foreach (Manga manga in mangaList)
            {
                currentRowIndex++;
                dt.Rows.Add(StringUtils.GenerateOrdinal(mangaList.Count, currentRowIndex),
                    manga.ID, manga.Name, manga.Url, manga.Site.ToString());
            }

            dgvMangaList.Invoke(new MethodInvoker(() => { dgvMangaList.DataSource = dt; }));
        }

        private void AppendMangaListGridView(int totalManga, List<Manga> partialList)
        {
            dgvMangaList.Invoke(new MethodInvoker(() =>
            {
                DataTable dt = (DataTable)dgvMangaList.DataSource;

                int currentRowIndex = 0;
                if (dt.Rows.Count > 0)
                    currentRowIndex = int.Parse(dt.Rows[dt.Rows.Count - 1][0].ToString());

                foreach (Manga manga in partialList)
                {
                    currentRowIndex++;
                    dt.Rows.Add(StringUtils.GenerateOrdinal(totalManga, currentRowIndex),
                        manga.ID, manga.Name, manga.Url, manga.Site.ToString());
                }
            }));
        }

        private DataTable CreateDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("no"));
            dt.Columns.Add(new DataColumn("id"));
            dt.Columns.Add(new DataColumn("name"));
            dt.Columns.Add(new DataColumn("url"));
            dt.Columns.Add(new DataColumn("site"));
            return dt;
        }

        void chapterWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            String mangaUrl = e.Argument.ToString();
            IProcessor processor = ProcessorFactory.CreateProcessor(currentSite);
            e.Result = processor.GetChapterList(mangaUrl);
        }

        void chapterWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int currentRowIndex = 1;
            List<Chapter> chapterList = (List<Chapter>)e.Result;

            dgvChapterList.Rows.Clear();

            foreach (Chapter chapter in chapterList)
            {
                dgvChapterList.Rows.Add(StringUtils.GenerateOrdinal(chapterList.Count, currentRowIndex),
                    chapter.ID, chapter.Name, chapter.Url, chapter.Site.ToString(), LinkType.CHAPTER);

                currentRowIndex++;
            }

            dgvChapterList.PerformLayout();

            tslbLoading.Visible = false;
        }

        void pageWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            String chapterUrl = e.Argument.ToString();
            IProcessor processor = ProcessorFactory.CreateProcessor(currentSite);
            e.Result = processor.GetPageList(chapterUrl);
        }

        void pageWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int currentRowIndex = 1;
            List<Page> pageList = (List<Page>)e.Result;

            dgvChapterList.Rows.Clear();

            foreach (Page page in pageList)
            {
                dgvChapterList.Rows.Add(StringUtils.GenerateOrdinal(pageList.Count, currentRowIndex),
                    page.ID, page.Name, page.Url, page.Site.ToString(), LinkType.PAGE);

                currentRowIndex++;
            }

            dgvChapterList.PerformLayout();

            tslbLoading.Visible = false;
        }

        private void cmsMangaMenu_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = (dgvMangaList.SelectedRows.Count == 0);
        }

        private void tsmiMangaViewOnline_Click(object sender, EventArgs e)
        {
            try { Process.Start(dgvMangaList.SelectedRows[0].Cells[COLUMN_MANGA_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiMangaCopyURL_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(dgvMangaList.SelectedRows[0].Cells[COLUMN_MANGA_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiMangaAddToQueue_Click(object sender, EventArgs e)
        {
            ValidateDownloadPath();

            List<int> indexes = new List<int>();

            foreach (DataGridViewRow row in dgvMangaList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow row = dgvMangaList.Rows[index];
                String name = row.Cells[COLUMN_MANGA_NAME].Value.ToString();
                LinkType type = LinkType.MANGA;
                MangaSite site = currentSite;
                String url = row.Cells[COLUMN_MANGA_URL].Value.ToString();
                AddToQueue(name, type, site, url);
            }
        }

        private void tsmiMangaDowload_Click(object sender, EventArgs e)
        {
            ValidateDownloadPath();

            List<int> indexes = new List<int>();

            foreach (DataGridViewRow row in dgvMangaList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow row = dgvMangaList.Rows[index];
                String name = row.Cells[COLUMN_MANGA_NAME].Value.ToString();
                LinkType type = LinkType.MANGA;
                MangaSite site = currentSite;
                String url = row.Cells[COLUMN_MANGA_URL].Value.ToString();
                Task task = AddToQueue(name, type, site, url);
                DownloadTask(task);
            }
        }

        private void ValidateDownloadPath()
        {
            var settings = SettingsManager.GetInstance().GetAppSettings();
            if (!Directory.Exists(settings.DownloadFolder))
            {
                settings.DownloadFolder = Application.StartupPath;
                SettingsManager.SaveChanges();
            }
        }

        private void cmsChapterMenu_Opening(object sender, CancelEventArgs e)
        {
            if (dgvChapterList.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                DataGridViewRow row = dgvChapterList.SelectedRows[0];
                LinkType linkType = EnumUtils.Parse<LinkType>(row.Cells[COLUMN_CHAPTER_LINK_TYPE].Value.ToString());
                if (linkType == LinkType.PAGE)
                {
                    tsmiChapterCopyURL.Visible = false;
                    tsmiChapterViewOnline.Visible = false;
                }
                else
                {
                    tsmiChapterCopyURL.Visible = true;
                    tsmiChapterViewOnline.Visible = true;
                }
            }
        }

        private void tsmiChapterViewOnline_Click(object sender, EventArgs e)
        {
            try { Process.Start(dgvChapterList.SelectedRows[0].Cells[COLUMN_CHAPTER_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiChapterCopyURL_Click(object sender, EventArgs e)
        {
            try { Clipboard.SetText(dgvChapterList.SelectedRows[0].Cells[COLUMN_CHAPTER_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiChapterAddToQueue_Click(object sender, EventArgs e)
        {
            ValidateDownloadPath();

            List<int> indexes = new List<int>();

            foreach (DataGridViewRow row in dgvChapterList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow row = dgvChapterList.Rows[index];
                String name = row.Cells[COLUMN_CHAPTER_NAME].Value.ToString();
                LinkType type = EnumUtils.Parse<LinkType>(row.Cells[COLUMN_CHAPTER_LINK_TYPE].Value.ToString());
                MangaSite site = currentSite;
                String url = row.Cells[COLUMN_CHAPTER_URL].Value.ToString();
                AddToQueue(name, type, site, url);
            }
        }

        private void tsmiChapterDownload_Click(object sender, EventArgs e)
        {
            ValidateDownloadPath();

            List<int> indexes = new List<int>();

            foreach (DataGridViewRow row in dgvChapterList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow row = dgvChapterList.Rows[index];
                String name = row.Cells[COLUMN_CHAPTER_NAME].Value.ToString();
                LinkType type = EnumUtils.Parse<LinkType>(row.Cells[COLUMN_CHAPTER_LINK_TYPE].Value.ToString());
                MangaSite site = currentSite;
                String url = row.Cells[COLUMN_CHAPTER_URL].Value.ToString();
                Task task = AddToQueue(name, type, site, url);
                DownloadTask(task);
            }
        }

        private void cmsTaskMenu_Opening(object sender, CancelEventArgs e)
        {
            if (dgvTaskList.SelectedRows.Count == 0)
            {
                e.Cancel = true;
            }
            else
            {
                DataGridViewRow row = dgvTaskList.SelectedRows[0];
                TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                LinkType type = EnumUtils.Parse<LinkType>(row.Cells[COLUMN_TASK_LINK_TYPE].Value.ToString());

                tsmiTaskSaveTo.Enabled = SomeRules.CanAssignSaveToTask(status);
                tsmiTaskReDownload.Enabled = SomeRules.CanReDownloadTask(status);
                tsmiTaskDownload.Enabled = SomeRules.CanDownloadTask(status);
                tsmiTaskStop.Enabled = SomeRules.CanStopTask(status);
                tsmiTaskReset.Enabled = SomeRules.CanResetTask(status);
                tsmiTaskSkip.Enabled = SomeRules.CanSkipTask(status);
                tsmiTaskRemove.Enabled = SomeRules.CanRemoveTask(status);
                tsmiTaskViewOnline.Visible = (type != LinkType.PAGE);
            }
        }

        private void tsmiTaskReDownload_Click(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();

            foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow row = dgvTaskList.Rows[index];
                TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                if (SomeRules.CanReDownloadTask(status))
                {
                    row.Cells[COLUMN_TASK_STATUS].Value = TaskStatus.QUEUED;
                    row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.QUEUED, 0);

                    Task task = taskList.Find(p => p.Url.Equals(url));
                    if (task != null)
                    {
                        task.Status = TaskStatus.QUEUED;
                        task.Percent = 0;
                    }
                }
            }

            TaskUtils.Export(taskList);

            tsmiTaskDownload_Click(sender, e);
        }

        private void tsmiTaskDownload_Click(object sender, EventArgs e)
        {
            List<int> indexes = new List<int>();
            List<Task> tl = new List<Task>();

            foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach (int index in indexes)
            {
                DataGridViewRow row = dgvTaskList.Rows[index];
                TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                if (SomeRules.CanDownloadTask(status))
                {
                    row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.QUEUED);
                    row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.QUEUED, 0);

                    Task task = taskList.First(p => p.Url.Equals(url));
                    if (task != null)
                    {
                        task.Status = TaskStatus.QUEUED;
                        task.Percent = 0;
                        tl.Add(task);
                    }
                }
            }
            TaskUtils.Export(taskList);

            foreach (Task t in tl)
            {
                workerManager.Download(t, workerHandlers);
            }
        }

        public void DownloadTask(Task task)
        {
            if (SomeRules.CanDownloadTask(task.Status))
                workerManager.Download(task, workerHandlers);
        }

        private void tsmiTaskStop_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to stop the task(s)?", "Stop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
                {
                    TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                    string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                    if (SomeRules.CanStopTask(status))
                    {
                        Task task = taskList.First(p => p.Url.Equals(url));
                        workerManager.Stop(task);
                    }
                }
            }
        }

        public Task AddToQueue(String name, LinkType type, MangaSite site, String url)
        {
            if (taskList.Exists(p => p.Url.Equals(url)))
                return taskList.First(p => p.Url.Equals(url));

            string folderPath = SettingsManager.GetInstance().GetDownloadFolderPath(site);
            string saveTo = String.Format("{0}\\{1}", folderPath, FileUtils.GetSafeName(name));
            Directory.CreateDirectory(saveTo);
            TaskStatus status = TaskStatus.QUEUED;
            int newRowIndex = dgvTaskList.Rows.Add(0, name, EnumUtils.Capitalize(status),
                GetProgressText(status, 0), EnumUtils.Capitalize(type), EnumUtils.Capitalize(site),
                saveTo, url, "");

            Task t = new Task();
            t.Sender = dgvTaskList.Rows[newRowIndex];
            t.Name = name;
            t.Path = saveTo;
            t.Description = "";
            t.Percent = 0;
            t.Site = site;
            t.Status = status;
            t.Type = type;
            t.Url = url;
            taskList.Add(t);
            TaskUtils.Export(taskList);

            return t;
        }

        private void AddToQueue(Task task)
        {
            dgvTaskList.Invoke(new MethodInvoker(() =>
            {
                int newRowIndex = dgvTaskList.Rows.Add(0, task.Name, EnumUtils.Capitalize(task.Status),
                    GetProgressText(task.Status, task.Percent), EnumUtils.Capitalize(task.Type),
                    EnumUtils.Capitalize(task.Site), task.Path, task.Url, task.Description);

                task.Sender = dgvTaskList.Rows[newRowIndex];
            }));
        }

        private string GetProgressText(TaskStatus taskStatus, double percent)
        {
            switch (taskStatus)
            {
                case TaskStatus.DOWNLOADING:
                    return String.Format("{0:0.##}%", percent);

                default:
                    return EnumUtils.Capitalize(taskStatus);
            }
        }

        private void OnWorkerDownloading(object dataRowSender)
        {
            try
            {
                dgvTaskList.Invoke(new MethodInvoker(() =>
                {
                    DataGridViewRow row = dataRowSender as DataGridViewRow;
                    if (row.Index > -1)
                    {
                        row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.DOWNLOADING);
                        row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.DOWNLOADING, 0);

                        String url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                        Task t = taskList.Find(p => p.Url.Equals(url));
                        if (t != null)
                        {
                            t.Percent = 0;
                            t.Status = TaskStatus.DOWNLOADING;
                        }
                    }
                }));
            }
            catch (InvalidOperationException) { }
        }

        private void OnWorkerProgressChanged(object dataRowSender, double percent)
        {
            try
            {
                dgvTaskList.Invoke(new MethodInvoker(() =>
                {
                    DataGridViewRow row = dataRowSender as DataGridViewRow;
                    if (row.Index > -1)
                    {
                        row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.DOWNLOADING, percent);

                        String url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                        Task t = taskList.Find(p => p.Url.Equals(url));
                        if (t != null)
                        {
                            t.Percent = percent;
                            t.Status = TaskStatus.DOWNLOADING;
                            TaskUtils.Export(taskList);
                        }
                    }
                }));
            }
            catch (InvalidOperationException) { }
        }

        private void OnWorkerComplete(object dataRowSender)
        {
            try
            {
                dgvTaskList.Invoke(new MethodInvoker(() =>
                {
                    DataGridViewRow row = dataRowSender as DataGridViewRow;
                    if (row.Index > -1)
                    {
                        TaskStatus completeStatus = TaskStatus.COMPLETE;
                        row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(completeStatus);
                        row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(completeStatus, 0);

                        String url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                        Task t = taskList.Find(p => p.Url.Equals(url));
                        if (t != null)
                        {
                            t.Percent = 100;
                            t.Status = completeStatus;
                            TaskUtils.Export(taskList);
                        }
                    }
                }));
            }
            catch (InvalidOperationException) { }
        }

        private void OnWorkerCancelled(object dataRowSender)
        {
            try
            {
                dgvTaskList.Invoke(new MethodInvoker(() =>
                {
                    DataGridViewRow row = dataRowSender as DataGridViewRow;
                    if (row.Index > -1)
                    {
                        row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.STOPPED);
                        row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.STOPPED, 0);

                        String url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                        Task t = taskList.Find(p => p.Url.Equals(url));
                        if (t != null)
                        {
                            t.Status = TaskStatus.STOPPED;
                            TaskUtils.Export(taskList);
                        }
                    }
                }));
            }
            catch (InvalidOperationException) { }
        }

        private void OnWorkerFailed(object dataRowSender, Exception e)
        {
            try
            {
                dgvTaskList.Invoke(new MethodInvoker(() =>
                {
                    DataGridViewRow row = dataRowSender as DataGridViewRow;
                    if (row.Index > -1)
                    {
                        row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.FAILED);
                        row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.FAILED, 0);
                        row.Cells[COLUMN_TASK_DESCRIPTION].Value = e.StackTrace;

                        String url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                        Task t = taskList.Find(p => p.Url.Equals(url));
                        if (t != null)
                        {
                            t.Status = TaskStatus.FAILED;
                            t.Description = e.Message;
                            TaskUtils.Export(taskList);
                        }
                    }
                }));
            }
            catch (InvalidOperationException) { }
        }

        private void dgvMangaList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (e.Button != MouseButtons.Right || rowIndex == -1)
                return;

            if (Control.ModifierKeys != Keys.Control && !dgvMangaList.Rows[rowIndex].Selected)
            {
                dgvMangaList.ClearSelection();
                dgvMangaList.Rows[rowIndex].Selected = true;
            }
        }

        private void dgvChapterList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (e.Button != MouseButtons.Right || rowIndex == -1)
                return;

            if (Control.ModifierKeys != Keys.Control && !dgvChapterList.Rows[rowIndex].Selected)
            {
                dgvChapterList.ClearSelection();
                dgvChapterList.Rows[rowIndex].Selected = true;
            }
        }

        private void dgvTaskList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (e.Button != MouseButtons.Right || rowIndex == -1)
                return;

            if (Control.ModifierKeys != Keys.Control && !dgvTaskList.Rows[rowIndex].Selected)
            {
                dgvTaskList.ClearSelection();
                dgvTaskList.Rows[rowIndex].Selected = true;
            }
        }

        private void tstbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            String keyword = tstbSearch.Text.ToUpper();
            List<Manga> resultList = new List<Manga>();
            foreach (Manga manga in mangaList)
                if (manga.Name.ToUpper().Contains(keyword) || manga.Url.ToUpper().Contains(keyword))
                    resultList.Add(manga);
            UpdateMangaListGridView(resultList);
        }

        private void tsmiTaskOpenFolder_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvTaskList.SelectedRows[0];
            String folderPath = row.Cells[COLUMN_TASK_SAVE_TO].Value.ToString();
            if (String.IsNullOrEmpty(folderPath)) return;
            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show(String.Format("Cannot find destination folder {0}", folderPath),
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process prc = new Process();
                prc.StartInfo.FileName = folderPath;
                prc.Start();
            }
            catch { }
        }

        private void tsmiTaskRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove the task(s)?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
                {
                    TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                    string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                    if (SomeRules.CanRemoveTask(status))
                    {
                        dgvTaskList.Rows.Remove(row);
                        Task task = taskList.First(p => p.Url.Equals(url));
                        taskList.Remove(task);
                        workerManager.Stop(task);
                    }
                }
                TaskUtils.Export(taskList);
            }
        }

        private void tsmiTaskViewOnline_Click(object sender, EventArgs e)
        {
            try { Process.Start(dgvTaskList.SelectedRows[0].Cells[COLUMN_TASK_URL].Value.ToString()); }
            catch { }
        }

        private void tsmiTaskSkip_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
            {
                TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                if (SomeRules.CanSkipTask(status))
                {
                    row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.SKIPPED);
                    row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.SKIPPED, 0);

                    Task task = taskList.Find(p => p.Url.Equals(url));
                    task.Status = TaskStatus.SKIPPED;
                    task.Percent = 0;
                }
            }
            TaskUtils.Export(taskList);
        }

        private void tsmiTaskReset_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
            {
                TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                if (SomeRules.CanResetTask(status))
                {
                    row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.QUEUED);
                    row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.QUEUED, 0);

                    Task task = taskList.Find(p => p.Url.Equals(url));
                    task.Status = TaskStatus.QUEUED;
                    task.Percent = 0;
                }
            }
            TaskUtils.Export(taskList);
        }

        private void tsbtTaskMoveUp_Click(object sender, EventArgs e)
        {
            List<int> selectedIndex = new List<int>();

            foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
                selectedIndex.Add(row.Index);

            selectedIndex.Sort();

            foreach (int index in selectedIndex)
            {
                if (index > 0)
                {
                    ListUtils.Swap(taskList, index, index - 1);
                    SwapTaskRow(index, index - 1);
                }
            }

            TaskUtils.Export(taskList);

            dgvTaskList.ClearSelection();

            foreach (int index in selectedIndex)
                dgvTaskList.Rows[index >= 1 ? index - 1 : index].Selected = true;
        }

        private void tsbtTaskMoveDown_Click(object sender, EventArgs e)
        {
            List<int> selectedIndex = new List<int>();
            int maxRowIndex = dgvTaskList.RowCount - 1;

            foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
                selectedIndex.Add(row.Index);

            selectedIndex.Sort();
            selectedIndex.Reverse();

            foreach (int index in selectedIndex)
            {
                if (index < dgvTaskList.RowCount - 1)
                {
                    ListUtils.Swap(taskList, index, index + 1);
                    SwapTaskRow(index, index + 1);
                }
            }

            TaskUtils.Export(taskList);

            dgvTaskList.ClearSelection();

            foreach (int index in selectedIndex)
                dgvTaskList.Rows[index < maxRowIndex ? index + 1 : index].Selected = true;
        }

        private void SwapTaskRow(int fromIndex, int toIndex)
        {
            if (fromIndex == toIndex) return;
            if (fromIndex < 0 || toIndex < 0) return;
            if (fromIndex >= dgvTaskList.RowCount || toIndex >= dgvTaskList.RowCount) return;

            DataGridViewRow fromRow = dgvTaskList.Rows[fromIndex];
            DataGridViewRow toRow = dgvTaskList.Rows[toIndex];

            dgvTaskList.Rows.Remove(fromRow);
            dgvTaskList.Rows.Remove(toRow);

            if (fromIndex < toIndex)
            {
                // Move down
                dgvTaskList.Rows.Insert(fromIndex, toRow);
                dgvTaskList.Rows.Insert(toIndex, fromRow);
            }
            else
            {
                // Move up
                dgvTaskList.Rows.Insert(toIndex, fromRow);
                dgvTaskList.Rows.Insert(fromIndex, toRow);
            }
        }

        private void tsmiTaskSaveTo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedPath = dialog.SelectedPath;
                foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
                {
                    string name = row.Cells[COLUMN_TASK_NAME].Value.ToString();
                    string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                    MangaSite site = EnumUtils.Parse<MangaSite>(row.Cells[COLUMN_TASK_SITE].Value.ToString());
                    string path = String.Format("{0}\\{1}\\{2}", selectedPath, site, FileUtils.GetSafeName(name));

                    row.Cells[COLUMN_TASK_SAVE_TO].Value = path;

                    Task task = taskList.Find(p => p.Url.Equals(url));
                    if (task != null)
                    {
                        task.Path = path;
                    }
                }
                TaskUtils.Export(taskList);
            }
        }

        private void dgvTaskList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                tsmiTaskRemove_Click(sender, e);
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            new Settings().ShowDialog();
            concurrentWorkersLimit = SettingsManager.GetInstance().GetAppSettings().TotalConcurrentWorkers;
            EnableOrDisableTurnOffComputerOption();
        }

        private void tsmiNewVersion_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(CommonSettings.NextVersionURL());
            }
            catch { }
        }

        private void tsmiCheckForUpdates_Click(object sender, EventArgs e)
        {
            VersionData vd;
            if (VersionUtils.CheckForUpdates(out vd))
            {
                CommonSettings.SetDateAfterCheckVersion(DateTime.Now.Date);
                CommonSettings.SetNextVersion(vd.Version);
                CommonSettings.SetNextVersionURL(vd.URL);

                DialogResult result = MessageBox.Show("A new version " + vd.Version + " is available! Would you like to update now?", CommonSettings.AppName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Process.Start(CommonSettings.NextVersionURL());
                    }
                    catch { }
                }
                else
                {
                    //ConfigurationData sd = SettingsManager.GetInstance().GetAppSettings();
                    //sd.IgnoreVersion = vd.Version;
                    //SettingsManager.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show(CommonSettings.AppName() + " is up to date (" + CommonSettings.AppVersion() + ")", CommonSettings.AppName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [Obsolete("Will be use once find a free host allow to get direct link")]
        private void RunAutoUpdate()
        {
            AutoUpdate au = new AutoUpdate();
            if (au.ShowDialog() == DialogResult.OK)
            {
                VersionData vd = au.VersionData;
                DialogResult result = MessageBox.Show("Are you sure you want to download new version " + vd.Version + "?", "New version", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Download d = new Download(vd.URL);
                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            Process.Start(AUTO_UPDATE_APP_NAME, d.GetDownloadedPath());
                            Environment.Exit(0);
                        }
                        catch { }
                    }
                }
                else
                {
                    ConfigurationData sd = SettingsManager.GetInstance().GetAppSettings();
                    sd.IgnoreVersion = vd.Version;
                    SettingsManager.SaveChanges();
                }
            }
        }

        private void tsmiGrabber_Click(object sender, EventArgs e)
        {
            Grabber g = new Grabber(this);
            g.ShowDialog();
        }

        private void tsbtSiteUpdate_Click(object sender, EventArgs e)
        {
            if (mangaWorker.IsBusy) return;

            ((DataTable)dgvMangaList.DataSource).Rows.Clear();

            tslbMangaLoading.Visible = true;
            tsbtSiteUpdate.Visible = false;
            mangaWorker.RunWorkerAsync();
        }

        private void tsmiTruyenTranhTuan_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.TRUYENTRANHTUAN);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiTruyenTranh8_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.TRUYENTRANH8);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiIZManga_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.IZTRUYENTRANH);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsMangaCommands_Resize(object sender, EventArgs e)
        {
            tstbSearch.Width = tsMangaCommands.Width - tslbSiteLogo.Width - tsbtSiteUpdate.Width - 30;
        }

        private void tscbDoWhenDone_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsManager.GetInstance().GetAppSettings().AutoShutdown = tscbDoWhenDone.SelectedIndex == 1;
            SettingsManager.SaveChanges();
        }

        private void tsmiNotifyShow_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;

            this.Show();

            try
            {
                FormWindowState fws = (FormWindowState)this.Tag;
                this.WindowState = fws;
            }
            catch
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void tsmiNotifyExit_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;

            if (workerManager.IsBusy)
            {
                workerManager.StopQueue();
                IsClosingForm = true;
            }
            else
            {
                Application.ExitThread();
                Application.Exit();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            tsmiNotifyShow_Click(sender, e);
        }

        private void tsmiAdvancedSearch_Click(object sender, EventArgs e)
        {
            AdvancedSearch s = new AdvancedSearch(this);
            s.ShowDialog();
        }

        private void ShowPopup(string title, string text, ToolTipIcon icon)
        {
            try { notifyIcon.ShowBalloonTip(1000, title, text, icon); }
            catch { }
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            SettingsManager.GetInstance().GetAppSettings().ShowTaskbarInfoOnMinimize = false;
            SettingsManager.SaveChanges();
        }

        private void EnableOrDisableTurnOffComputerOption()
        {
            tscbDoWhenDone.SelectedIndex = SettingsManager.GetInstance().GetAppSettings().AutoShutdown ? 1 : 0;
        }

        private void tsmiConverter_Click(object sender, EventArgs e)
        {
            try { Process.Start("Converter.exe"); }
            catch { MessageBox.Show("Cannot found Converter.exe, please make sure it's on the same directory with this.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsmiKissManga_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.KISSMANGA);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiHVTT_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.HOCVIENTRUYENTRANH);
            tsMangaCommands_Resize(sender, e);
        }

        private void mangaParkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.MANGAPARK);
            tsMangaCommands_Resize(sender, e);
        }

        private void AutoUpdateMangaList()
        {
            autoUpdateMangaListWorker.WorkerSupportsCancellation = true;
            autoUpdateMangaListWorker.DoWork += AutoUpdateMangaListWorker_DoWork;
            autoUpdateMangaListWorker.RunWorkerCompleted += AutoUpdateMangaListWorker_RunWorkerCompleted;

            MangaSite site = checkAndRunAutoUpdateMangaList();
            if (site != MangaSite.UNKNOWN)
            {
                autoUpdateMangaSite = site;
                autoUpdateMangaListWorker.RunWorkerAsync(site);
            }
        }

        private MangaSite checkAndRunAutoUpdateMangaList()
        {
            ConfigurationData sd = SettingsManager.GetInstance().GetAppSettings();
            Dictionary<MangaSite, DateTime> sites = ConfigurationIO.ReadSiteDates();
            foreach (MangaSite s in sites.Keys)
            {
                DateTime dt = sites[s];
                DateTime afterDays = dt.AddDays(sd.UpdateAfter);
                if (afterDays.Date <= DateTime.Now.Date)
                {
                    return s;
                }
            }
            return MangaSite.UNKNOWN;
        }

        private void AutoUpdateMangaListWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            autoUpdateMangaList.Clear();

            MangaSite site = (MangaSite)e.Argument;
            autoUpdateProcessor = ProcessorFactory.CreateProcessor(site);
            autoUpdateProcessor.ScrapOneMangaPageComplete += AutoUpdateProcessor_ScrapOneMangaPageComplete;
            autoUpdateProcessor.GetMangaList();
        }

        private void AutoUpdateMangaListWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (autoUpdateProcessor.IsCancel() == false)
            {
                string autoUpdateFilePath = Path.GetTempFileName();
                MangaUtils.SaveListToFile(autoUpdateFilePath, autoUpdateMangaSite, autoUpdateMangaList);
                try
                {
                    File.Copy(autoUpdateFilePath, Application.StartupPath + "\\data\\" + autoUpdateMangaSite.ToString().ToLower(), true);
                }
                catch { }
            }

            ConfigurationIO.WriteSiteUpdatedDate(autoUpdateMangaSite, DateTime.Now.Date);
            MangaSite site = checkAndRunAutoUpdateMangaList();
            if (site != MangaSite.UNKNOWN)
            {
                autoUpdateMangaSite = site;
                autoUpdateMangaListWorker.RunWorkerAsync(site);
            }
        }

        private void AutoUpdateProcessor_ScrapOneMangaPageComplete(IProcessor invoker, int totalManga, int totalPages, int pageIndex, List<Manga> partialList)
        {
            autoUpdateMangaList.AddRange(partialList);

            if (mangaWorker.IsBusy && currentSite == autoUpdateMangaSite)
            {
                autoUpdateProcessor.Cancel();
                autoUpdateMangaListWorker.CancelAsync();
            }
        }

        private void tsmiLHManga_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.LHMANGA);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiTruyenTranhMoi_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.TRUYENTRANHMOI);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiMangaK_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.MANGAK);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiUpTruyen_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.UPTRUYEN);
            tsMangaCommands_Resize(sender, e);
        }

        private void tsmiTruyenTranhOnline_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.TRUYENTRANHONLINE);
            tsMangaCommands_Resize(sender, e);
        }
    }
}
