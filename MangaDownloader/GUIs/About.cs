using Common;
using MangaDownloader.Settings;
using MangaDownloader.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public partial class About : BaseForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            lbVersion.Text = String.Format("Version: {0} ({1:yyyy-MM-dd})", CommonProperties.MDVersion, CommonProperties.ReleaseDate);
            tbDescription.Text = GetDesc();
        }

        private String GetDesc()
        {
            string changeLogsPath = Application.StartupPath + "\\changelogs.txt";
            string[] lines = new string[] { };
            if (File.Exists(changeLogsPath))
            {
                lines = System.IO.File.ReadAllLines(changeLogsPath);
            }

            var settings = SettingsManager.GetInstance().GetSettings();
            StringBuilder bd = new StringBuilder();
            bd.AppendLine("A tool for downloading manga from the internet.");
            bd.AppendLine("It's TOTALLY FREE 4EVER (NO ADS).");
            bd.AppendLine("Feel free to feedback to me any errors or more features.");
            bd.AppendLine();
            bd.AppendLine("=== Contact ===");
            bd.AppendLine("Author: " + settings.Author);
            bd.AppendLine("Email: " + settings.ContactEmail);
            bd.AppendLine("Website: " + settings.Website1);
            bd.AppendLine("          or: " + settings.Website2);
            bd.AppendLine();
            bd.AppendLine("=== Changelogs ===");
            foreach (var l in lines)
                bd.AppendLine(l);
            return bd.ToString();
        }

        private void btCheckUpdates_Click(object sender, EventArgs e)
        {
            btCheckUpdates.Enabled = false;
            AutoUpdate au = new AutoUpdate();
            if (au.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VersionData vd = au.VersionData;
                DialogResult result = MessageBox.Show("Are you sure you want to download new version " + vd.Version + "?", "New version", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try { Process.Start(vd.URL); }
                    catch { }
                }
            }
            btCheckUpdates.Enabled = true;
        }
    }
}
