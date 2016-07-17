using Common.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Common
{
    public class CommonSettings
    {
        private static AppMode appMode;

        public static string AppName()
        {
            return "Manga Bot";
        }

        public static string AppVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileMajorPart + "." + fvi.FileMinorPart;
        }

        public static AppMode AppMode
        {
            get
            {
#if (!DEBUG)
                    appMode = AppMode.PROD;
#else
                appMode = (AppMode)Enum.Parse(typeof(AppMode), Properties.Settings.Default.AppMode);
#endif
                return appMode;
            }
            set
            {
                appMode = value;
            }

        }

        public static string CheckVersionUrl()
        {
            return "https://drive.google.com/uc?export=download&id=0BwclU1yWN7VGVHBTenA3NURaVkU";
        }

        public static DateTime ReleaseDate()
        {
            return Properties.Settings.Default.ReleaseDate;
        }

        public static string MainScreen()
        {
            return "Main";
        }

        public static string GaBaseUrl()
        {
            return "http://www.google-analytics.com/collect";
        }

        public static string GaTrackingId()
        {
            return AppMode == AppMode.PROD ? "UA-61053646-1" : "UA-61053646-2";
        }

        public static DateTime CheckVersionDate()
        {
            return Properties.Settings.Default.CheckVersionDate;
        }

        public static void SetDateAfterCheckVersion(DateTime dt)
        {
            Properties.Settings.Default.CheckVersionDate = dt;
            Properties.Settings.Default.Save();
        }

        public static string NextVersion()
        {
            string version = Properties.Settings.Default.NextVersion;
            return string.IsNullOrWhiteSpace(version) ? AppVersion() : version;
        }

        public static void SetNextVersion(string nextVersion)
        {
            Properties.Settings.Default.NextVersion = nextVersion;
            Properties.Settings.Default.Save();
        }

        public static string NextVersionURL()
        {
            return Properties.Settings.Default.NextVersionURL;
        }

        public static void SetNextVersionURL(string nextVersionUrl)
        {
            Properties.Settings.Default.NextVersionURL = nextVersionUrl;
            Properties.Settings.Default.Save();
        }

        public static MangaSite LatestSite()
        {
            MangaSite site = MangaSite.BLOGTRUYEN;
            return Enum.TryParse(Properties.Settings.Default.LatestSite, true, out site) ? site : MangaSite.BLOGTRUYEN;
        }

        public static void SetLatestSite(MangaSite site)
        {
            Properties.Settings.Default.LatestSite = site.ToString();
            Properties.Settings.Default.Save();
        }
    }
}
