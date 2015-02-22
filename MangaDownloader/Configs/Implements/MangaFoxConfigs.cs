using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;

namespace MangaDownloader.Configs.Implements
{
    class MangaFoxConfigs : Configs
    {
        public MangaFoxConfigs()
        {
            SiteIcon = null;
            SiteLogo = GetSiteLogo(MangaSite.MANGAFOX);
            SiteName = "MangaFox";
            TagOfSite = " - mangafox.me";
        }
    }
}
