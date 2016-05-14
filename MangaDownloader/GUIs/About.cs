using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MangaDownloader.GUIs
{
    public partial class About : BaseForm
    {
        const String AUTO_UPDATE_APP_NAME = "Auto Update.exe";

        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            lbVersion.Text = String.Format("Version: {0} ({1:yyyy-MM-dd})", CommonSettings.AppVersion(), CommonSettings.ReleaseDate());
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

            StringBuilder bd = new StringBuilder();
            bd.AppendLine("An amazing tool aims to download manga around the world.");
            bd.AppendLine("It's 100% FREE FOREVER (NO ADS).");
            bd.AppendLine("Feel free to feedback to me any errors or more features.");
            bd.AppendLine();
            bd.AppendLine("=== Contact ===");
            bd.AppendLine("Author: Duy Tran");
            bd.AppendLine("Email: dangduy2910@gmail.com");
            bd.AppendLine("Website: http://mangabot.github.io/");
            bd.AppendLine("               https://mangabot.wordpress.com/");
            bd.AppendLine("               https://tdduy89.wordpress.com/");
            bd.AppendLine("               https://tdduy89.blogspot.com/");
            bd.AppendLine();
            bd.AppendLine("=== Changelogs ===");
            foreach (var l in lines)
                bd.AppendLine(l);
            return bd.ToString();
        }

        private void btCheckUpdates_Click(object sender, EventArgs e)
        {
            AutoUpdate au = new AutoUpdate();
            if (au.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VersionData vd = au.VersionData;
                DialogResult result = MessageBox.Show("Are you sure you want to download new version " + vd.Version + "?", "New version", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        Process.Start(AUTO_UPDATE_APP_NAME);
                        Environment.Exit(0);
                    }
                    catch { }
                }
            }
        }
    }
}
