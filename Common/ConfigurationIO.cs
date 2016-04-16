using System;
using System.IO;
using System.Text;

namespace Common
{
    public class ConfigurationIO
    {
        private static string CLIENT_ID = "ClientID";
        private static string TOTAL_WORKERS = "TotalWorkers";
        private static string SHORTCUT = "Shortcut";
        private static string ZIP = "ZIP";
        private static string PDF = "PDF";
        private static string CLEANUP = "Cleanup";
        private static string SHUTDOWN = "Shutdown";
        private static string DOWNLOAD_FOLDER = "DownloadFolder";
        private static string SHOW_TASKBAR_INFO = "ShowInfo";
        private static string IGNORE_VERSION = "IgnoreVersion";
        private static string MINIMIZE_TASKBAR = "MinimizeTaskbar";

        public static void Write(ConfigurationData sd)
        {
            try
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory + "\\data";
                Directory.CreateDirectory(folder);

                string configPath = folder + "\\configuration";
                using (StreamWriter sw = new StreamWriter(configPath, false, Encoding.UTF8))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendFormat("{0}={1}{2}", CLIENT_ID, sd.ClientID, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", TOTAL_WORKERS, sd.TotalConcurrentWorkers, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", SHORTCUT, sd.AutoCreateShortcut ? 1 : 0, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", ZIP, sd.AutoCreateZip ? 1 : 0, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", PDF, sd.AutoCreatePdf ? 1 : 0, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", CLEANUP, sd.AutoCleanup ? 1 : 0, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", SHUTDOWN, sd.AutoShutdown ? 1 : 0, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", DOWNLOAD_FOLDER, sd.DownloadFolder, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", SHOW_TASKBAR_INFO, sd.ShowTaskbarInfoOnMinimize ? 1 : 0, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", IGNORE_VERSION, sd.IgnoreVersion, Environment.NewLine)
                        .AppendFormat("{0}={1}", MINIMIZE_TASKBAR, sd.MinimizeTaskbar ? 1 : 0);
                    sw.WriteLine(builder.ToString());
                    sw.Close();
                }
            }
            catch { }
        }

        public static ConfigurationData Read()
        {
            ConfigurationData sd = new ConfigurationData();
            try
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory + "\\data";
                Directory.CreateDirectory(folder);

                string configPath = folder + "\\configuration";
                using (StreamReader sr = new StreamReader(configPath, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            if (parts[0] == CLIENT_ID)
                                sd.ClientID = parts[1];
                            else if (parts[0] == TOTAL_WORKERS)
                                sd.TotalConcurrentWorkers = int.Parse(parts[1]);
                            else if (parts[0] == SHORTCUT)
                                sd.AutoCreateShortcut = ConvertToBoolean(parts[1], true);
                            else if (parts[0] == ZIP)
                                sd.AutoCreateZip = ConvertToBoolean(parts[1], false);
                            else if (parts[0] == PDF)
                                sd.AutoCreatePdf = ConvertToBoolean(parts[1], false);
                            else if (parts[0] == CLEANUP)
                                sd.AutoCleanup = ConvertToBoolean(parts[1], false);
                            else if (parts[0] == SHUTDOWN)
                                sd.AutoShutdown = ConvertToBoolean(parts[1], false);
                            else if (parts[0] == DOWNLOAD_FOLDER)
                                sd.DownloadFolder = parts[1];
                            else if (parts[0] == SHOW_TASKBAR_INFO)
                                sd.ShowTaskbarInfoOnMinimize = ConvertToBoolean(parts[1], true);
                            else if (parts[0] == IGNORE_VERSION)
                                sd.IgnoreVersion = parts[1];
                            else if (parts[0] == MINIMIZE_TASKBAR)
                                sd.MinimizeTaskbar = ConvertToBoolean(parts[1], true);
                        }
                    }
                    sr.Close();
                }
            }
            catch { }
            return sd;
        }

        private static bool ConvertToBoolean(String str, bool defaultValue)
        {
            try
            {
                int i;
                return Int32.TryParse(str, out i) ? Convert.ToBoolean(i) : Convert.ToBoolean(str);
            }
            catch
            {
                return defaultValue;
            }
        }
    }

    public class ConfigurationData
    {
        public string ClientID;
        public int TotalConcurrentWorkers;
        public bool AutoCreateShortcut;
        public bool AutoCreateZip;
        public bool AutoCreatePdf;
        public bool AutoCleanup;
        public bool AutoShutdown;
        public string DownloadFolder;
        public bool ShowTaskbarInfoOnMinimize;
        public string IgnoreVersion;
        public bool MinimizeTaskbar;

        public ConfigurationData()
        {
            ClientID = "";
            TotalConcurrentWorkers = 3;
            AutoCreateShortcut = true;
            AutoCreateZip = false;
            AutoCreatePdf = false;
            AutoCleanup = false;
            AutoShutdown = false;
            DownloadFolder = AppDomain.CurrentDomain.BaseDirectory;
            ShowTaskbarInfoOnMinimize = true;
            IgnoreVersion = "";
            MinimizeTaskbar = true;
        }
    }
}
