using MangaDownloader.Enums;
using MangaDownloader.Processors;
using MangaDownloader.Utils;
using MangaDownloader.Workers;
using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Scrapers;
using WebScraper.Utils;

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

        BackgroundWorker mangaWorker;
        BackgroundWorker chapterWorker;
        BackgroundWorker pageWorker;
        MangaSite currentSite = MangaSite.BLOGTRUYEN;
        WorkerManager workerManager = WorkerManager.GetInstance();
        List<Manga> mangaList = new List<Manga>();
        List<Task> taskList = new List<Task>();
        WorkerHandlers workerHandlers = new WorkerHandlers();

        public MainForm()
            : base()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ImportMangaList();
            initWorkers();
            importTaskList();
            tsbtnManga.Visible = false;
            tslbSlash.Visible = false;
            tsbtnChapter.Visible = false;
            tslbLoading.Visible = false;
            tsbtnStartAll.Enabled = true;
            tsbtnStopAll.Enabled = false;

            workerManager.AllWorkersStopped += workerManager_AllWorkersStopped;

            workerHandlers.Downloading = OnWorkerDownloading;
            workerHandlers.ProgressChanged = OnWorkerProgressChanged;
            workerHandlers.Complete = OnWorkerComplete;
            workerHandlers.Cancelled = OnWorkerCancelled;
            workerHandlers.Failed = OnWorkerFailed;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            workerManager.StopQueue();
        }

        void workerManager_AllWorkersStopped()
        {
            tsTaskCommands.Invoke(new MethodInvoker(() =>
            {
                tsbtnStartAll.Enabled = true;
                tsbtnStopAll.Enabled = false;
            }));
        }

        private void ImportMangaList()
        {
            mangaList = MangaExportUtils.Import(currentSite);
            UpdateMangaListGridView(mangaList);
        }

        private void tsmiBlogTruyen_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.BLOGTRUYEN);
        }

        private void tsmiVeChai_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.VECHAI);
        }

        private void tsmiMangaFox_Click(object sender, EventArgs e)
        {
            setCurrentSite(MangaSite.MANGAFOX);
        }

        private void setCurrentSite(MangaSite site)
        {
            // TODO how to cancel manga worker???
            if (mangaWorker.IsBusy) mangaWorker.CancelAsync();
            currentSite = site;
            switch(site)
            {
                case MangaSite.BLOGTRUYEN:
                    tslbSiteName.Text = "Blog Truyen";
                    tslbSiteName.Image = Properties.Resources.blogtruyen_logo;
                    break;
                case MangaSite.VECHAI:
                    tslbSiteName.Text = "Ve Chai";
                    tslbSiteName.Image = Properties.Resources.vechai_logo;
                    break;

                case MangaSite.MANGAFOX:
                    tslbSiteName.Text = "Manga Fox";
                    tslbSiteName.Image = Properties.Resources.mangafox_logo;
                    break;

                default:
                    // REVIEW Add logo for more sites
                    throw new NotImplementedException();
            }
            ImportMangaList();
        }

        private void tsmiContactMe_Click(object sender, EventArgs e)
        {

        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnRefreshMangaList_Click(object sender, EventArgs e)
        {
            if (mangaWorker.IsBusy) return;
            tsbtnRefreshMangaList.Enabled = false;
            mangaWorker.RunWorkerAsync();
        }

        private void tsbtnStartAll_Click(object sender, EventArgs e)
        {
            tsbtnStartAll.Enabled = false;
            tsbtnStopAll.Enabled = true;
            workerManager.StartQueue();
        }

        private void tsbtnStopAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to stop all tasks?", "Stop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
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
            tsbtnManga.Text = "Manga: " + mangaName;
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
                tsbtnChapter.Text = "Chapter: " + chapterName;
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
            mangaWorker.ProgressChanged += mangaWorker_ProgressChanged;

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
            taskList = TaskUtils.Import();
            foreach(Task t in taskList)
                AddToQueue(t);
        }

        void mangaWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            mangaList.Clear();
            IProcessor processor = ProcessorFactory.CreateProcessor(currentSite);
            processor.ScrapOneMangaPageComplete += processor_ScrapOneMangaPageComplete;
            e.Result = processor.GetMangaList();
        }

        void processor_ScrapOneMangaPageComplete(int totalPages, int pageIndex, List<Manga> partialList)
        {
            int NO_NEED_PERCENT = 0;
            mangaWorker.ReportProgress(NO_NEED_PERCENT, partialList);

            mangaList.AddRange(partialList);
            MangaExportUtils.Export(currentSite, mangaList);
            AppendMangaListGridView(partialList);
        }

        void mangaWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //List<Manga> partialList = (List<Manga>)e.UserState;
            //mangaList.AddRange(partialList);
            //MangaExportUtils.Export(currentSite, mangaList);
            //AppendMangaListGridView(partialList);
        }

        void mangaWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tsbtnRefreshMangaList.Enabled = true;
        }

        private void UpdateMangaListGridView(List<Manga> mangaList)
        {
            int currentRowIndex = 1;
            dgvMangaList.Rows.Clear();

            foreach (Manga manga in mangaList)
            {
                dgvMangaList.Rows.Add(StringUtils.GenerateOrdinal(mangaList.Count, currentRowIndex),
                    manga.ID, manga.Name, manga.Url, manga.Site.ToString());

                currentRowIndex++;
            }
            dgvMangaList.PerformLayout();
        }

        private void AppendMangaListGridView(List<Manga> partialList)
        {
            dgvMangaList.Invoke(new MethodInvoker(() =>
            {
                foreach (Manga manga in partialList)
                    dgvMangaList.Rows.Add(0, manga.ID, manga.Name, manga.Url, manga.Site.ToString());
                UpdateMangaListGridViewIndex();
                dgvMangaList.PerformLayout();
            }));
        }

        private void UpdateMangaListGridViewIndex()
        {
            int currentRowIndex = 1;
            foreach (DataGridViewRow row in dgvMangaList.Rows)
            {
                row.Cells[COLUMN_MANGA_NO].Value = StringUtils.GenerateOrdinal(dgvMangaList.RowCount, currentRowIndex);
                currentRowIndex++;
            }
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
            List<int> indexes = new List<int>();

            foreach(DataGridViewRow row in dgvMangaList.SelectedRows)
                indexes.Add(row.Index);

            indexes.Sort();

            foreach(int index in indexes)
            {
                DataGridViewRow row = dgvMangaList.Rows[index];
                String name = row.Cells[COLUMN_MANGA_NAME].Value.ToString();
                LinkType type = LinkType.MANGA;
                MangaSite site = currentSite;
                String url = row.Cells[COLUMN_MANGA_URL].Value.ToString();
                AddToQueue(name, type, site, url);
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
            foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
            {
                TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                if (SomeRules.CanReDownloadTask(status))
                {
                    row.Cells[COLUMN_TASK_STATUS].Value = TaskStatus.QUEUED;
                    Task task = taskList.Find(p => p.Url.Equals(url));
                    if (task != null)
                    {
                        task.Status = TaskStatus.QUEUED;
                    }
                }
            }

            TaskUtils.Export(taskList);

            tsmiTaskDownload_Click(sender, e);
        }

        private void tsmiTaskDownload_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTaskList.SelectedRows)
            {
                TaskStatus status = EnumUtils.Parse<TaskStatus>(row.Cells[COLUMN_TASK_STATUS].Value.ToString());
                string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                if (SomeRules.CanDownloadTask(status))
                    workerManager.Download(url);
            }
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
                        workerManager.Stop(url);
                }
            }
        }

        private void AddToQueue(String name, LinkType type, MangaSite site, String url)
        {
            if (taskList.Exists(p => p.Url.Equals(url))) return;

            // TODO use config instead of fixed string
            string folderPath = "D:\\BlogTruyen";
            string saveTo = String.Format("{0}\\{1}", folderPath, FileUtils.GetSafeName(name));
            TaskStatus status = TaskStatus.QUEUED;
            int newRowIndex = dgvTaskList.Rows.Add(0, name, EnumUtils.Capitalize(status),
                GetProgressText(status, 0), saveTo, EnumUtils.Capitalize(type), 
                EnumUtils.Capitalize(site), url, "");

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

            workerManager.InsertOrUpdateWorker(t, workerHandlers);
        }

        private void AddToQueue(Task task)
        {
            int newRowIndex = dgvTaskList.Rows.Add(0, task.Name, EnumUtils.Capitalize(task.Status),
                GetProgressText(task.Status, task.Percent), task.Path, EnumUtils.Capitalize(task.Type),
                EnumUtils.Capitalize(task.Site), task.Url, task.Description);

            task.Sender = dgvTaskList.Rows[newRowIndex];
            workerManager.InsertOrUpdateWorker(task, workerHandlers);
        }

        private string GetProgressText(TaskStatus taskStatus, double percent)
        {
            switch(taskStatus)
            {
                case TaskStatus.DOWNLOADING:
                    return String.Format("{0:0.##}%", percent);
                    
                default:
                    return EnumUtils.Capitalize(taskStatus);
            }
        }

        private void OnWorkerDownloading(object dataRowSender)
        {
            dgvTaskList.Invoke(new MethodInvoker(() =>
            {
                DataGridViewRow row = dataRowSender as DataGridViewRow;
                row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.DOWNLOADING);
                row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.DOWNLOADING, 0);
            }));
        }

        private void OnWorkerProgressChanged(object dataRowSender, double percent)
        {
            dgvTaskList.Invoke(new MethodInvoker(() =>
            {
                DataGridViewRow row = dataRowSender as DataGridViewRow;
                row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.DOWNLOADING, percent);

                String url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                Task t = taskList.Find(p => p.Url.Equals(url));
                if (t != null)
                {
                    t.Percent = percent;
                    t.Status = TaskStatus.DOWNLOADING;
                    TaskUtils.Export(taskList);
                }
            }));
        }

        private void OnWorkerComplete(object dataRowSender)
        {
            dgvTaskList.Invoke(new MethodInvoker(() =>
            {
                DataGridViewRow row = dataRowSender as DataGridViewRow;
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
            }));
        }

        private void OnWorkerCancelled(object dataRowSender)
        {
            dgvTaskList.Invoke(new MethodInvoker(() =>
            {
                DataGridViewRow row = dataRowSender as DataGridViewRow;
                row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.STOPPED);
                row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.STOPPED, 0);

                String url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                Task t = taskList.Find(p => p.Url.Equals(url));
                if (t != null)
                {
                    t.Status = TaskStatus.STOPPED;
                    TaskUtils.Export(taskList);
                }
            }));
        }

        private void OnWorkerFailed(object dataRowSender, Exception e)
        {
            dgvTaskList.Invoke(new MethodInvoker(() =>
            {
                DataGridViewRow row = dataRowSender as DataGridViewRow;
                row.Cells[COLUMN_TASK_STATUS].Value = EnumUtils.Capitalize(TaskStatus.FAILED);
                row.Cells[COLUMN_TASK_PROGRESS].Value = GetProgressText(TaskStatus.FAILED, 0);
                row.Cells[COLUMN_TASK_DESCRIPTION].Value = e.StackTrace;

                String url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                Task t = taskList.Find(p => p.Url.Equals(url));
                if (t != null)
                {
                    t.Status = TaskStatus.COMPLETE;
                    t.Description = e.Message;
                    TaskUtils.Export(taskList);
                }
            }));
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

            String keyword = tstbSearch.Text;
            List<Manga> resultList = new List<Manga>();
            foreach(Manga manga in mangaList)
                if (manga.Name.ToUpper().Contains(keyword.ToUpper()))
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
                        taskList.RemoveAll(p => p.Url.Equals(url));
                        workerManager.RemoveWorker(url);
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
                    Task task = taskList.Find(p => p.Url.Equals(url));
                    task.Status = TaskStatus.SKIPPED;
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
                foreach(DataGridViewRow row in dgvTaskList.SelectedRows)
                {
                    string name = row.Cells[COLUMN_TASK_NAME].Value.ToString();
                    string url = row.Cells[COLUMN_TASK_URL].Value.ToString();
                    string path = String.Format("{0}\\{1}", selectedPath, FileUtils.GetSafeName(name));

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
    }
}
