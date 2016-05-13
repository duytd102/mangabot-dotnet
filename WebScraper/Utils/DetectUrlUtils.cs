﻿using Common.Enums;
using System;

namespace WebScraper.Utils
{
    public class DetectUrlUtils
    {
        const string blogtruyen = "blogtruyen.com";
        const string mangafox = "mangafox.me";
        const string vechai = "vechai.info";
        const string manga24h = "manga24h.com";
        const string truyentranhtuan = "truyentranhtuan.com";
        const string truyentranhnhanh = "truyentranhnhanh.com";
        const string truyentranh8 = "truyentranh8.net";
        const string izmanga = "izmanga.com";
        const string kissmanga = "kissmanga.com";
        const string otakufc = "otakufc.com";
        const string hvtt = "truyen.academyvn.com";
        const string mangapark = "mangapark.me";
        const string lhmanga = "lhmanga.com";

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

                else if (host.IndexOf(manga24h) >= 0)
                    return MangaSite.MANGA24H;

                else if (host.IndexOf(truyentranhtuan) >= 0)
                    return MangaSite.TRUYENTRANHTUAN;

                else if (host.IndexOf(truyentranh8) >= 0)
                    return MangaSite.TRUYENTRANH8;

                else if (host.IndexOf(izmanga) >= 0)
                    return MangaSite.IZMANGA;

                else if (host.IndexOf(kissmanga) >= 0)
                    return MangaSite.KISSMANGA;

                else if (host.IndexOf(otakufc) >= 0)
                    return MangaSite.OTAKUFC;

                else if (host.IndexOf(hvtt) >= 0)
                    return MangaSite.HOCVIENTRUYENTRANH;

                else if (host.IndexOf(mangapark) >= 0)
                    return MangaSite.MANGAPARK;

                else if (host.IndexOf(lhmanga) >= 0)
                    return MangaSite.LHMANGA;
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
