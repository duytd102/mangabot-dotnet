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
                    appMode = Mode.PROD;
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
            return "https://dl.dropboxusercontent.com/u/148375006/apps/manga-downloader/manga-downloader-latest-version.csv";
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
    }
}
