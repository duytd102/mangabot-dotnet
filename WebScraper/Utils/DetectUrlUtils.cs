using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;

namespace WebScraper.Utils
{
    public class DetectUrlUtils
    {
        const string blogtruyen = "blogtruyen.com";
        const string mangafox = "mangafox.me";
        const string vechai = "vechai.info";
        const string mangavn = "mangavn.net";
        const string manga24h = "manga24h.com";
        const string truyentranhtuan = "truyentranhtuan.com";

        public static MangaSite GetSite(String url)
        {
            try
            {
                Uri u = new Uri(url);
                string host = u.Host;

                if (host.IndexOf(blogtruyen) >= 0)
                    return MangaSite.BLOGTRUYEN;
                else if (host.IndexOf(mangafox) >= 0)
                    return MangaSite.MANGAFOX;
                else if (host.IndexOf(vechai) >= 0)
                    return MangaSite.VECHAI;
                else if (host.IndexOf(mangavn) >= 0)
                    return MangaSite.MANGAVN;
                else if (host.IndexOf(manga24h) >= 0)
                    return MangaSite.MANGA24H;
                else if (host.IndexOf(truyentranhtuan) >= 0)
                    return MangaSite.TRUYENTRANHTUAN;
            }
            catch { }

            // TODO: Analyze more urls if has more sites
            return MangaSite.UNKNOWN;
        }

        public static bool IsSupportedSite(String url)
        {
            return GetSite(url) != MangaSite.UNKNOWN;
        }
    }
}
