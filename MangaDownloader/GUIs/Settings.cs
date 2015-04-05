using MangaDownloader.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            cbAutoCreateShortcut.Checked = commonSettings.AutoCreateShortcut;
            cbAutoCreateZip.Checked = commonSettings.AutoCreateZip;
            cbAutoCreatePDF.Checked = commonSettings.AutoCreatePdf;
            cbAutoClean.Checked = commonSettings.AutoClean;
            cbTurnOffComputerWhenDone.Checked = commonSettings.TurnOffWhenDone;
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
            if (Directory.Exists(tbDefaultFolder.Text))
            {
                var commonSettings = SettingsManager.GetInstance().GetCommonSettings();
                commonSettings.TotalConcurrentWorkers = (int)nudTotalWorkers.Value;
                commonSettings.AutoCreateShortcut = cbAutoCreateShortcut.Checked;
                commonSettings.AutoCreateZip = cbAutoCreateZip.Checked;
                commonSettings.AutoCreatePdf = cbAutoCreatePDF.Checked;
                commonSettings.AutoClean = cbAutoClean.Checked;
                commonSettings.TurnOffWhenDone = cbTurnOffComputerWhenDone.Checked;
                commonSettings.RootDownloadFolderPath = tbDefaultFolder.Text;
                commonSettings.Save();
                this.Close();
            }
            else
            {
                MessageBox.Show("Download path doesn't exist", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
