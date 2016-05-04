using Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private static string UPDATE_AFTER = "UpdateAfter";

        private static ConfigurationData config = new ConfigurationData();
        private static Dictionary<MangaSite, DateTime> siteUpdatedDates = new Dictionary<MangaSite, DateTime>();

        public static ConfigurationData Read()
        {
            try
            {
                config = new ConfigurationData();
                siteUpdatedDates.Clear();

                string folder = AppDomain.CurrentDomain.BaseDirectory + "data";
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
                                config.ClientID = parts[1];
                            else if (parts[0] == TOTAL_WORKERS)
                                config.TotalConcurrentWorkers = int.Parse(parts[1]);
                            else if (parts[0] == SHORTCUT)
                                config.AutoCreateShortcut = ConvertToBoolean(parts[1], true);
                            else if (parts[0] == ZIP)
                                config.AutoCreateZip = ConvertToBoolean(parts[1], false);
                            else if (parts[0] == PDF)
                                config.AutoCreatePdf = ConvertToBoolean(parts[1], false);
                            else if (parts[0] == CLEANUP)
                                config.AutoCleanup = ConvertToBoolean(parts[1], false);
                            else if (parts[0] == SHUTDOWN)
                                config.AutoShutdown = ConvertToBoolean(parts[1], false);
                            else if (parts[0] == DOWNLOAD_FOLDER)
                                config.DownloadFolder = parts[1];
                            else if (parts[0] == SHOW_TASKBAR_INFO)
                                config.ShowTaskbarInfoOnMinimize = ConvertToBoolean(parts[1], true);
                            else if (parts[0] == IGNORE_VERSION)
                                config.IgnoreVersion = parts[1];
                            else if (parts[0] == MINIMIZE_TASKBAR)
                                config.MinimizeTaskbar = ConvertToBoolean(parts[1], true);
                            else if (parts[0] == UPDATE_AFTER)
                                config.UpdateAfter = int.Parse(parts[1]);

                            foreach (MangaSite ms in Enum.GetValues(typeof(MangaSite)))
                            {
                                if (ms.ToString().Equals(parts[0], StringComparison.CurrentCultureIgnoreCase))
                                {
                                    DateTime dt = DateTime.ParseExact(parts[1], "yyyyMMdd", CultureInfo.CurrentCulture);
                                    siteUpdatedDates.Add(ms, dt);
                                }
                            }
                        }
                    }
                    sr.Close();
                }
            }
            catch { }

            return new ConfigurationData(config);
        }

        public static void Write(ConfigurationData sd)
        {
            try
            {
                config = new ConfigurationData(sd);

                string folder = AppDomain.CurrentDomain.BaseDirectory + "data";
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
                        .AppendFormat("{0}={1}{2}", MINIMIZE_TASKBAR, sd.MinimizeTaskbar ? 1 : 0, Environment.NewLine)
                        .AppendFormat("{0}={1}{2}", UPDATE_AFTER, sd.UpdateAfter, Environment.NewLine);
                    
                    foreach (MangaSite ms in Enum.GetValues(typeof(MangaSite)))
                    {
                        if (ms == MangaSite.UNKNOWN) continue;

                        bool exists = false;

                        foreach (MangaSite s in siteUpdatedDates.Keys)
                        {
                            exists = true;
                            break;
                        }

                        if (exists)
                        {
                            builder.AppendFormat("{0}={1}{2}", ms.ToString(), siteUpdatedDates[ms].ToString("yyyyMMdd"), Environment.NewLine);
                        }
                        else
                        {
                            builder.AppendFormat("{0}={1}{2}", ms.ToString(), DateTime.Now.ToString("yyyyMMdd"), Environment.NewLine);
                        }
                    }

                    sw.Write(builder.ToString());
                    sw.Close();
                }
            }
            catch { }
        }

        public static Dictionary<MangaSite, DateTime> ReadSiteDates()
        {
            Dictionary<MangaSite, DateTime> dic = new Dictionary<MangaSite, DateTime>();
            foreach(MangaSite s in siteUpdatedDates.Keys)
            {
                dic.Add(s, siteUpdatedDates[s]);
            }
            return dic;
        }

        public static void WriteSiteUpdatedDate(MangaSite site, DateTime dt)
        {
            try
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory + "data";
                Directory.CreateDirectory(folder);

                string configPath = folder + "\\configuration";

                if (siteUpdatedDates.ContainsKey(site))
                {
                    siteUpdatedDates[site] = dt;
                }
                else
                {
                    siteUpdatedDates.Add(site, dt);
                }
                
                Write(config);
            }
            catch { }
        }

        private static bool ConvertToBoolean(String str, bool defaultValue)
        {
            try
            {
                int i;
                return int.TryParse(str, out i) ? Convert.ToBoolean(i) : Convert.ToBoolean(str);
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
        public int UpdateAfter;

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
            UpdateAfter = 7;
        }

        public ConfigurationData(ConfigurationData cd)
        {
            ClientID = cd.ClientID;
            TotalConcurrentWorkers = cd.TotalConcurrentWorkers;
            AutoCreateShortcut = cd.AutoCreateShortcut;
            AutoCreateZip = cd.AutoCreateZip;
            AutoCreatePdf = cd.AutoCreatePdf;
            AutoCleanup = cd.AutoCleanup;
            AutoShutdown = cd.AutoShutdown;
            DownloadFolder = cd.DownloadFolder;
            ShowTaskbarInfoOnMinimize = cd.ShowTaskbarInfoOnMinimize;
            IgnoreVersion = cd.IgnoreVersion;
            MinimizeTaskbar = cd.MinimizeTaskbar;
            UpdateAfter = cd.UpdateAfter;
        }
    }
}
