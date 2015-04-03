using MangaDownloader.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebScraper.Enums;

namespace MangaDownloader.Settings
{
    class SettingsManager
    {
        private static SettingsManager instance;

        private SettingsManager()
        {
            string rootPath = Properties.CommonSettings.Default.RootDownloadFolderPath;
            if (String.IsNullOrEmpty(rootPath))
            {
                Properties.CommonSettings.Default.RootDownloadFolderPath = Application.StartupPath;
                Properties.CommonSettings.Default.Save();
            }

        }

        public static SettingsManager GetInstance()
        {
            if (instance == null)
                instance = new SettingsManager();
            return instance;
        }

        public Properties.Settings GetSettings()
        {
            return Properties.Settings.Default;
        }

        public CommonSettings GetCommonSettings()
        {
            return Properties.CommonSettings.Default;
        }

        public String GetDownloadFolderPath(MangaSite site)
        {
            return GetCommonSettings().RootDownloadFolderPath + "\\" + site.ToString();
        }
    }
}
