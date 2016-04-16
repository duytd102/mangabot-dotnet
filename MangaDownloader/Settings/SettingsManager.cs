using Common;
using System;
using WebScraper.Enums;

namespace MangaDownloader.Settings
{
    class SettingsManager
    {
        private static SettingsManager instance;
        private static ConfigurationData configData;

        private SettingsManager() { }

        public static SettingsManager GetInstance()
        {
            if (instance == null)
                instance = new SettingsManager();
            return instance;
        }

        public static void Import()
        {
            configData = ConfigurationIO.Read();
        }

        public static void SaveChanges()
        {
            ConfigurationIO.Write(configData);
        }

        public Properties.Settings GetSettings()
        {
            return Properties.Settings.Default;
        }

        public ConfigurationData GetAppSettings()
        {
            return configData;
        }

        public String GetDownloadFolderPath(MangaSite site)
        {
            return GetAppSettings().DownloadFolder + "\\" + site.ToString();
        }
    }
}
