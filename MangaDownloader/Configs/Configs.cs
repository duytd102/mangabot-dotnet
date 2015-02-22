using MangaDownloader.Properties;
using MangaDownloader.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebScraper.Enums;

namespace MangaDownloader.Configs
{
    abstract class Configs
    {
        public Boolean AutoCreateShortcut = false;
        public Boolean AutoCreatePdf = false;
        public Boolean AutoCreateZip = false;
        public Boolean AutoStartup = false;
        public Boolean AutoUpdate = true;
        public Boolean AutoShutdown = false;
        public Boolean AutoDeleteImagesAfterFinished = false;
        public int TotalConcurrentWorkers = 3;

        /** Will be overriden according to sites */
        public Icon SiteIcon = null;
        public Bitmap SiteLogo = Resources.blogtruyen_logo;
        public String SiteName = "BlogTruyen";
        public String TagOfSite = " - blogtruyen.com";

        private String rootFolderPath = Application.StartupPath;

        public String RootFolderPath
        {
            get { return rootFolderPath; }
            set { rootFolderPath = value; }
        }

        public String QueuePath = String.Format("{0}\\download.csv", Application.StartupPath);
        public String SettingsPath = String.Format("{0}\\settings.csv", Application.StartupPath);
        public String GetMangaPath(MangaSite site) { return String.Format("{0}\\{1}.csv", Application.StartupPath, site); }
        public String GetDownloadFolderPath(MangaSite site) { return String.Format("{0}\\{1}", rootFolderPath, site); }

        public Bitmap GetSiteLogo(MangaSite site)
        {
            // REVIEW implement if has more sites
            switch (site)
            {
                case MangaSite.BLOGTRUYEN: return Resources.blogtruyen_logo;
                case MangaSite.MANGAFOX: return Resources.mangafox_logo;
                case MangaSite.VECHAI: return Resources.vechai_logo;
                default: throw new NotImplementedException();
            }
        }
    }
}
