﻿using Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdate
{
    public partial class MainForm : Form
    {
        private string unZipFolder = String.Format("{0}/{1}", Application.StartupPath, Guid.NewGuid().ToString().Replace("-", ""));
        private string zipFile = "";
        private string versionURL = "";
        private const string MD_APP_NAME = "Manga Downloader.exe";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            zipFile = String.Format("{0}.zip", unZipFolder);
            lbDescription.Text = "Checking...";
            Thread versionThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    VersionData vd;
                    if (VersionUtils.CheckForUpdates(out vd))
                    {
                        versionURL = vd.URL;
                        DownloadFile(vd.URL, zipFile);
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }));
            versionThread.IsBackground = true;
            versionThread.Start();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnklbUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start(versionURL); }
            catch { }
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

        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                ZipUtils.UnZip(zipFile);
                MoveToUpdate();
                File.Delete(zipFile);
                Directory.Delete(unZipFolder, true);
            }
            catch { }
            try
            {
                Process.Start(MD_APP_NAME);
                this.Invoke(new MethodInvoker(() => { this.Close(); }));
            }
            catch { }
        }

        private void MoveToUpdate()
        {
            string[] paths = Directory.GetFiles(unZipFolder);
            foreach (string p in paths)
            {
                File.Copy(p, Application.StartupPath + "/" + Path.GetFileName(p), true);
            }
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            UpdateText(String.Format("Downloading... [{0} / {1}]", ByteToMB(e.BytesReceived), ByteToMB(e.TotalBytesToReceive)));
        }

        private void UpdateText(string text)
        {
            lbDescription.Invoke(new MethodInvoker(() => { lbDescription.Text = text; }));
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