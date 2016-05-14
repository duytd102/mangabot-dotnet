using Common;
using Common.Enums;
using System;

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

        public string GetDownloadFolderPath(MangaSite site)
        {
            string path = GetAppSettings().DownloadFolder;
            return path + (path.EndsWith("\\") ? "" : "\\") + site.ToString();
        }
    }
}
