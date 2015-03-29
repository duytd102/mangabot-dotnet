using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;

namespace WebScraper.Utils
{
    public class DetectUrlUtils
    {
        const string domain_blogtruyen = "http://blogtruyen.com";
        const string domain_mangafox = "http://mangafox.me";
        const string domain_vechai = "http://vechai.info";
        const string domain_mangavn = "http://www.mangavn.net";

        public static MangaSite GetSite(String url)
        {
            if (url.IndexOf(domain_blogtruyen) >= 0)
                return MangaSite.BLOGTRUYEN;
            else if (url.IndexOf(domain_mangafox) >= 0)
                return MangaSite.MANGAFOX;
            else if (url.IndexOf(domain_vechai) >= 0)
                return MangaSite.VECHAI;
            else if (url.IndexOf(domain_mangavn) >= 0)
                return MangaSite.MANGAVN;

            // TODO: Analyze more urls if has more sites
            return MangaSite.UNKNOWN;
        }

        public static bool IsSupportedSite(String url)
        {
            return GetSite(url) != MangaSite.UNKNOWN;
        }
    }
}
