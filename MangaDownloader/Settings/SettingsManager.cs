using MangaDownloader.Properties;
using MangaDownloader.Utils;
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
        private static SettingsData settingsData;

        private SettingsManager() { }

        public static SettingsManager GetInstance()
        {
            if (instance == null)
                instance = new SettingsManager();
            return instance;
        }

        public static void Import()
        {
            settingsData = SettingsUtils.Read();
        }

        public static void SaveChanges()
        {
            SettingsUtils.Write(settingsData);
        }

        public Properties.Settings GetSettings()
        {
            return Properties.Settings.Default;
        }

        public SettingsData GetAppSettings()
        {
            return settingsData;
        }

        public String GetDownloadFolderPath(MangaSite site)
        {
            return GetAppSettings().DownloadFolder + "\\" + site.ToString();
        }
    }
}
