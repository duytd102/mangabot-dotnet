using MangaDownloader.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public partial class Settings : BaseForm
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            var commonSettings = SettingsManager.GetInstance().GetCommonSettings();
            nudTotalWorkers.Value = commonSettings.TotalConcurrentWorkers;
            cbAutoUpdate.Checked = commonSettings.AutoUpdate;
            cbAutoCreateShortcut.Checked = commonSettings.AutoCreateShortcut;
            cbAutoCreateZip.Checked = commonSettings.AutoCreateZip;
            cbAutoCreatePDF.Checked = commonSettings.AutoCreatePdf;
            cbAutoClean.Checked = commonSettings.AutoClean;
            tbDefaultFolder.Text = commonSettings.RootDownloadFolderPath;
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            d.SelectedPath = tbDefaultFolder.Text;
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbDefaultFolder.Text = d.SelectedPath;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var commonSettings = SettingsManager.GetInstance().GetCommonSettings();
            commonSettings.TotalConcurrentWorkers = (int)nudTotalWorkers.Value;
            commonSettings.AutoUpdate = cbAutoUpdate.Checked;
            commonSettings.AutoCreateShortcut = cbAutoCreateShortcut.Checked;
            commonSettings.AutoCreateZip = cbAutoCreateZip.Checked;
            commonSettings.AutoCreatePdf = cbAutoCreatePDF.Checked;
            commonSettings.AutoClean = cbAutoClean.Checked;
            commonSettings.RootDownloadFolderPath = tbDefaultFolder.Text;
            commonSettings.Save();
            this.Close();
        }

        private void cbAutoCreateZip_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisableAutoClean();
        }

        private void cbAutoCreatePDF_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisableAutoClean();
        }

        private void EnableOrDisableAutoClean()
        {
            cbAutoClean.Enabled = cbAutoCreateZip.Checked || cbAutoCreatePDF.Checked;
        }
    }
}
