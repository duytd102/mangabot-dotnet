using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;

namespace WebScraper.Utils
{
    public class DetectUrlUtils
    {
        const string domain_blogtruyen = "http://blogtruyen.com/";
        const string domain_mangafox = "http://mangafox.me/";
        const string domain_vechai = "http://vechai.info/";

        public static MangaSite GetSite(String url)
        {
            if (url.IndexOf(domain_blogtruyen) >= 0)
                return MangaSite.BLOGTRUYEN;

            if (url.IndexOf(domain_mangafox) >= 0)
                return MangaSite.MANGAFOX;

            if (url.IndexOf(domain_vechai) >= 0)
                return MangaSite.VECHAI;

            // TODO: Analyze more urls if has more sites
            return MangaSite.UNKNOWN;
        }

        public static bool IsSupportedSite(String url)
        {
            return GetSite(url) != MangaSite.UNKNOWN;
        }
    }
}
