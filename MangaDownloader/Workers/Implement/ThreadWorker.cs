using Common;
using MangaDownloader.Settings;
using MangaDownloader.Utils;
using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using WebScraper.Data;
using WebScraper.Enums;
using WebScraper.Processors;
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

        const int MILLISECONDS_DELAY = 200;

        private Task task;
        private IProcessor processor;
        private BackgroundWorker executor;

        public ThreadWorker()
        {
            executor = new BackgroundWorker();
            executor.WorkerReportsProgress = true;
            executor.WorkerSupportsCancellation = true;
            executor.DoWork += executor_DoWork;
            executor.ProgressChanged += executor_ProgressChanged;
            executor.RunWorkerCompleted += executor_RunWorkerCompleted;
        }

        public bool IsBusy() { return executor.IsBusy; }

        public void Start(Task task)
        {
            if (!IsBusy())
            {
                this.task = task;
                this.processor = ProcessorFactory.CreateProcessor(task.Site);
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

        public Task GetTask() { return task; }

        void executor_DoWork(object sender, DoWorkEventArgs e)
        {
            String parentFolderPath = task.Path;
            Directory.CreateDirectory(parentFolderPath);
            switch (task.Type)
            {
                case Enums.LinkType.MANGA:
                    GoogleAnalyticsUtils.SendEvent(GlobalProperties.APP_NAME, CommonProperties.MDVersion, task.Site.ToString(), EventAction.DOWNLOAD_MANGA, task.Url);
                    DownloadManga(e, task);
                    break;

                case Enums.LinkType.CHAPTER:
                    GoogleAnalyticsUtils.SendEvent(GlobalProperties.APP_NAME, CommonProperties.MDVersion, task.Site.ToString(), EventAction.DOWNLOAD_CHAPTER, task.Url);
                    DownloadChapter(e, task);
                    break;

                case Enums.LinkType.PAGE:
                    GoogleAnalyticsUtils.SendEvent(GlobalProperties.APP_NAME, CommonProperties.MDVersion, task.Site.ToString(), EventAction.DOWNLOAD_PAGE, task.Url);
                    DownloadPage(task.Name, task.Url, task.Site, parentFolderPath);
                    executor.ReportProgress(1, new DownloadData(task.Sender, 100));
                    break;
            }
        }

        void executor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!executor.CancellationPending)
            {
                DownloadData downloadData = e.UserState as DownloadData;
                ProgressChanged(downloadData.DataRowSender, downloadData.Percent);
            }
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
                    return;
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
                        return;
                    }

                    DownloadPage(page.Name, page.Url, site, chapterFolderPath);
                    totalPercent += percentPerPage;
                    executor.ReportProgress(1, new DownloadData(dataRowSender, totalPercent));
                    Thread.Sleep(MILLISECONDS_DELAY);  // prevent firing OnCancelled and OnProgressChanged at the same time
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
                    return;
                }

                DownloadPage(page.Name, page.Url, site, chapterFolderPath);
                totalPercent += percentPerPage;
                executor.ReportProgress(1, new DownloadData(dataRowSender, totalPercent));
                Thread.Sleep(MILLISECONDS_DELAY);
            }
            Shortcut(chapterUrl, chapterFolderPath, chapterName);
            ZipFolder(chapterFolderPath, chapterName);
            CreatePDF(chapterFolderPath, chapterName);
            DeleteImages(chapterFolderPath);
        }

        private void DownloadPage(String pageName, String pageUrl, MangaSite site, String chapterFolderPath)
        {
            String pagePath = String.Format("{0}\\{1}-{2}{3}", 
                chapterFolderPath, pageName, site, FileUtils.GetExtension(pageUrl));
            HttpUtils.DownloadFile(pageUrl, pagePath);
        }

        private void Shortcut(string url, string folderPath, string fileName)
        {
            bool autoCreateShortCut = SettingsManager.GetInstance().GetAppSettings().AutoCreateShortcut;
            if (autoCreateShortCut)
                FileUtils.CreateShortcut(url, folderPath, fileName);
        }

        private void ZipFolder(string folderPath, string fileName)
        {
            bool autoCreateZip = SettingsManager.GetInstance().GetAppSettings().AutoCreateZip;
            if (autoCreateZip)
                FileUtils.ZipFolder(folderPath, fileName);
        }

        private void CreatePDF(string folderPath, string fileName)
        {
            bool autoCreatePdf = SettingsManager.GetInstance().GetAppSettings().AutoCreatePdf;
            if (autoCreatePdf)
            {
                String saveToFolder = Path.GetDirectoryName(folderPath);
                String fullPath = String.Format("{0}\\{1}.pdf", saveToFolder, fileName);
                FileUtils.ImagesToPDF(Directory.GetFiles(folderPath), fullPath);
            }
        }

        private void DeleteImages(String folderPath)
        {
            bool autoDeleteImages = SettingsManager.GetInstance().GetAppSettings().AutoCleanup;
            if (autoDeleteImages)
                Directory.Delete(folderPath, true);
        }
    }
}
