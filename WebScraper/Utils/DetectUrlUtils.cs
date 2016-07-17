using Common.Enums;
using System;

namespace WebScraper.Utils
{
    public class DetectUrlUtils
    {
        const string blogtruyen = "blogtruyen.com";
        const string truyentranhnet = "truyentranh.net";
        const string manga24h = "manga24h.com";
        const string truyentranhtuan = "truyentranhtuan.com";
        const string truyentranhnhanh = "truyentranhnhanh.com";
        const string truyentranh8 = "truyentranh8.net";
        const string iztruyentranh = "iztruyentranh.com";
        const string otakufc = "otakufc.com";
        const string hvtt = "truyen.academyvn.com";
        const string lhmanga = "lhmanga.com";
        const string truyentranhmoi = "truyentranhmoi.com";
        const string mangak = "mangak.info";
        const string uptruyen = "uptruyen.com";
        
        const string mangafox = "mangafox.me";
        const string mangapark = "mangapark.me";
        const string kissmanga = "kissmanga.com";

        public static MangaSite GetSite(String url)
        {
            try
            {
                Uri u = new Uri(url);
                string host = u.Host;

                if (host.IndexOf(blogtruyen) >= 0)
                    return MangaSite.BLOGTRUYEN;

                else if (host.IndexOf(truyentranhnet) >= 0)
                    return MangaSite.TRUYENTRANHNET;

                else if (host.IndexOf(truyentranhtuan) >= 0)
                    return MangaSite.TRUYENTRANHTUAN;

                else if (host.IndexOf(truyentranh8) >= 0)
                    return MangaSite.TRUYENTRANH8;

                else if (host.IndexOf(iztruyentranh) >= 0)
                    return MangaSite.IZTRUYENTRANH;

                else if (host.IndexOf(hvtt) >= 0)
                    return MangaSite.HOCVIENTRUYENTRANH;

                else if (host.IndexOf(lhmanga) >= 0)
                    return MangaSite.LHMANGA;

                else if (host.IndexOf(truyentranhmoi) >= 0)
                    return MangaSite.TRUYENTRANHMOI;

                else if (host.IndexOf(mangak) >= 0)
                    return MangaSite.MANGAK;

                else if (host.IndexOf(uptruyen) >= 0)
                    return MangaSite.UPTRUYEN;




                else if (host.IndexOf(mangafox) >= 0)
                    return MangaSite.MANGAFOX;

                else if (host.IndexOf(mangapark) >= 0)
                    return MangaSite.MANGAPARK;

                else if (host.IndexOf(kissmanga) >= 0)
                    return MangaSite.KISSMANGA;
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
