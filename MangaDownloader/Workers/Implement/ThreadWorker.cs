using MangaDownloader.Processors;
using MangaDownloader.Utils;
using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Utils;

namespace MangaDownloader.Workers.Implement
{
    class ThreadWorker : IWorker
    {
        public event Action<object> Downloading;
        public event Action<object, double> ProgressChanged;
        public event Action<object> Complete;
        public event Action<object> Cancelled;
        public event Action<object, Exception> Failed;

        private Task task;
        private IProcessor processor;
        private BackgroundWorker executor;

        public ThreadWorker(IProcessor processor, Task task, WorkerHandlers handlers)
        {
            this.processor = processor;
            this.task = task;
            RegisterCallbacks(handlers);

            executor = new BackgroundWorker();
            executor.WorkerReportsProgress = true;
            executor.WorkerSupportsCancellation = true;
            executor.DoWork += executor_DoWork;
            executor.ProgressChanged += executor_ProgressChanged;
            executor.RunWorkerCompleted += executor_RunWorkerCompleted;
        }

        public bool IsBusy() { return executor.IsBusy; }

        public void Start()
        {
            if (!IsBusy())
            {
                Downloading(task.Sender);
                executor.RunWorkerAsync();
            }
        }

        public void Stop()
        {
            if (IsBusy())
            {
                executor.CancelAsync();
            }
        }

        public void SetTask(Task task)
        {
            this.task = task;
        }

        void executor_DoWork(object sender, DoWorkEventArgs e)
        {
            String parentFolderPath = task.Path;
            Directory.CreateDirectory(parentFolderPath);
            switch (task.Type)
            {
                case Enums.LinkType.MANGA:
                    DownloadManga(e, task);
                    break;
                case Enums.LinkType.CHAPTER:
                    DownloadChapter(e, task);
                    break;
                case Enums.LinkType.PAGE:
                    DownloadPage(task.Name, task.Url, task.Site, parentFolderPath);
                    executor.ReportProgress(1, new DownloadData(task.Sender, 100));
                    break;
            }
        }

        void executor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DownloadData downloadData = e.UserState as DownloadData;
            ProgressChanged(downloadData.DataRowSender, downloadData.Percent);
        }

        void executor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Cancelled(task.Sender);
            }
            else if (e.Error != null)
            {
                Failed(task.Sender, e.Error);
            }
            else
            {
                Complete(task.Sender);
            }
        }

        private void DownloadManga(DoWorkEventArgs e, Task task)
        {
            string mangaName = task.Name;
            string mangaUrl = task.Url;
            string mangaFolderPath = task.Path;
            MangaSite site = task.Site;
            object dataRowSender = task.Sender;
            List<Chapter> chapterList = processor.GetChapterList(mangaUrl);
            double percentPerChapter = (double)100 / chapterList.Count;
            double totalPercent = 0;
            foreach (Chapter chapter in chapterList)
            {
                if (executor.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                string chapterName = FileUtils.GetSafeName(chapter.Name);
                string chapterFolderPath = String.Format("{0}\\{1}", mangaFolderPath, chapterName);
                Directory.CreateDirectory(chapterFolderPath);
                List<Page> pageList = processor.GetPageList(chapter.Url);
                double percentPerPage = ((double)1 / pageList.Count) * percentPerChapter;
                foreach (Page page in pageList)
                {
                    if (executor.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    DownloadPage(page.Name, page.Url, site, chapterFolderPath);
                    totalPercent += percentPerPage;
                    executor.ReportProgress(1, new DownloadData(dataRowSender, totalPercent));
                }
                Shortcut(chapter.Url, chapterFolderPath, chapterName);
                ZipFolder(chapterFolderPath, chapterName);
                CreatePDF(chapterFolderPath, chapterName);
                DeleteImages(chapterFolderPath);
            }
            Shortcut(mangaUrl, mangaFolderPath, mangaName);
        }

        private void DownloadChapter(DoWorkEventArgs e, Task task)
        {
            string chapterName = task.Name;
            string chapterUrl = task.Url;
            string chapterFolderPath = task.Path;
            MangaSite site = task.Site;
            object dataRowSender = task.Sender;
            List<Page> pageList = processor.GetPageList(chapterUrl);
            double totalPercent = 0;
            double percentPerPage = (double)100 / pageList.Count;
            foreach (Page page in pageList)
            {
                if (executor.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                DownloadPage(page.Name, page.Url, site, chapterFolderPath);
                totalPercent += percentPerPage;
                executor.ReportProgress(1, new DownloadData(dataRowSender, totalPercent));
            }
            Shortcut(chapterUrl, chapterFolderPath, chapterName);
            ZipFolder(chapterFolderPath, chapterName);
            CreatePDF(chapterFolderPath, chapterName);
            DeleteImages(chapterFolderPath);
        }

        private void DownloadPage(String pageName, String pageUrl, MangaSite site, String chapterFolderPath)
        {
            String fixedUrl = pageUrl;
            //switch (site)
            //{
            //    case MangaSite.MANGAFOX:
            //        fixedUrl = MangaFoxGrabber.ExtractPageImage(pageUrl);
            //        break;
            //}

            String pagePath = String.Format("{0}\\{1}{2}{3}", chapterFolderPath, pageName, site, FileUtils.GetExtension(fixedUrl));
            HttpUtils.DownloadFile(fixedUrl, pagePath);
        }

        private void Shortcut(string url, string folderPath, string fileName)
        {
            bool autoCreateShortCut = true;
            if (autoCreateShortCut)
                FileUtils.CreateShortcut(url, folderPath, fileName);
        }

        private void ZipFolder(string folderPath, string fileName)
        {
            bool autoCreateZip = true;
            if (autoCreateZip)
                FileUtils.ZipFolder(folderPath, fileName);
        }

        private void CreatePDF(string folderPath, string fileName)
        {
            bool autoCreatePdf = true;
            if (autoCreatePdf)
            {
                String saveToFolder = Path.GetDirectoryName(folderPath);
                String fullPath = String.Format("{0}\\{1}.pdf", saveToFolder, fileName);
                FileUtils.ImagesToPDF(Directory.GetFiles(folderPath), fullPath);
            }
        }

        private void DeleteImages(String folderPath)
        {
            bool autoDeleteImages = false;
            if (autoDeleteImages)
                Directory.Delete(folderPath, true);
        }

        private void RegisterCallbacks(WorkerHandlers handlers)
        {
            if (handlers != null)
            {
                this.Downloading = ((object sender) =>
                {
                    if (handlers.Downloading != null)
                        handlers.Downloading(sender);
                });
                this.ProgressChanged = ((object sender, double percent) =>
                {
                    if (handlers.ProgressChanged != null)
                        handlers.ProgressChanged(sender, percent);
                });
                this.Complete = ((object sender) =>
                {
                    if (handlers.Complete != null)
                        handlers.Complete(sender);
                });
                this.Cancelled = ((object sender) =>
                {
                    if (handlers.Cancelled != null)
                        handlers.Cancelled(sender);
                });
                this.Failed = ((object sender, Exception ex) =>
                {
                    if (handlers.Failed != null)
                        handlers.Failed(sender, ex);
                });
            }
        }


        public Task GetTask()
        {
            return task;
        }


        public bool IsQueued()
        {
            return task != null & 
                (task.Status == Enums.TaskStatus.QUEUED ||
                task.Status == Enums.TaskStatus.STOPPED);
        }
    }
}
