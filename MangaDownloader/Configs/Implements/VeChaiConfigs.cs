using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;

namespace MangaDownloader.Configs.Implements
{
    class VeChaiConfigs : Configs
    {
        public VeChaiConfigs()
        {
            SiteIcon = null;
            SiteLogo = GetSiteLogo(MangaSite.VECHAI);
            SiteName = "VeChai";
            TagOfSite = " - vechai.info";
        }
    }
}
