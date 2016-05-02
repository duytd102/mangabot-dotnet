using Common;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public partial class Download : BaseForm
    {
        private string unZipFolder = String.Format("{0}{1}", Path.GetTempPath(), Guid.NewGuid().ToString());
        private string zipFile = "";
        private string downloadUrl = "";

        public Download()
        {
            InitializeComponent();
        }

        public Download(String url)
        {
            InitializeComponent();
            this.downloadUrl = url;
        }

        public String GetDownloadedPath() { return unZipFolder; }

        private void Download_Load(object sender, EventArgs e)
        {
            zipFile = String.Format("{0}.zip", unZipFolder);
            lblDesc.Text = "Downloading...";
            Thread versionThread = new Thread(new ThreadStart(() => { DownloadFile(downloadUrl, zipFile); }));
            versionThread.IsBackground = true;
            versionThread.Start();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void DownloadFile(string url, string filePath)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
                    webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
                    webClient.DownloadFileAsync(new Uri(url), filePath);
                }
            }
            catch { }
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            File.SetAttributes(zipFile, FileAttributes.Hidden);
            UpdateText(String.Format("Downloading... [{0} / {1}]", ByteToMB(e.BytesReceived), ByteToMB(e.TotalBytesToReceive)));
            pbProgress.Invoke(new MethodInvoker(() => { pbProgress.Value = e.ProgressPercentage; }));
        }

        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                ZipUtils.UnZip(zipFile);
                DirectoryInfo di = new DirectoryInfo(unZipFolder);
                di.Attributes = FileAttributes.Hidden;
                File.Delete(zipFile);
                pbProgress.Invoke(new MethodInvoker(() => { pbProgress.Value = 100; }));
                this.Invoke(new MethodInvoker(() =>
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }));
            }
            catch { }
        }

        private void UpdateText(string text)
        {
            try { lblDesc.Invoke(new MethodInvoker(() => { lblDesc.Text = text; })); } catch { }
        }

        private string ByteToMB(long b)
        {
            double KB = b / 1024f;
            double MB = KB / 1024f;
            if (MB > 1) return Math.Round(MB, 2) + " MB";
            return Math.Round(KB, 2) + " KB";
        }
    }
}
