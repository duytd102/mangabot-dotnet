using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;

namespace MangaDownloader.Configs.Implements
{
    class BlogTruyenConfigs : Configs
    {
        public BlogTruyenConfigs()
        {
            SiteIcon = null;
            SiteLogo = GetSiteLogo(MangaSite.BLOGTRUYEN);
            SiteName = "BlogTruyen";
            TagOfSite = " - blogtruyen.com";
        }
    }
}
