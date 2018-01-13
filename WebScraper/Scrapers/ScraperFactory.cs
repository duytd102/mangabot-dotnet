using Common.Enums;
using System;

namespace WebScraper.Scrapers
{
    class ScraperFactory
    {
        public static IScraper CreateScraper(MangaSite site)
        {
            // TODO implement manga list
            switch (site)
            {
                case MangaSite.BLOGTRUYEN:
                    return new DefaultScraper(MangaSite.BLOGTRUYEN, "WebScraper.Scrapers.Scripts.BlogTruyenScript", "http://blogtruyen.com");

                case MangaSite.TRUYENTRANHNET:
                    return new DefaultScraper(MangaSite.TRUYENTRANHNET, "WebScraper.Scrapers.Scripts.TruyenTranhNetScript", "http://truyentranh.net");

                case MangaSite.TRUYENTRANHTUAN:
                    return new DefaultScraper(MangaSite.TRUYENTRANHTUAN, "WebScraper.Scrapers.Scripts.TruyenTranhTuanScript", "http://truyentranhtuan.com/");

                case MangaSite.TRUYENTRANH8:
                    return new DefaultScraper(MangaSite.TRUYENTRANH8, "WebScraper.Scrapers.Scripts.TruyenTranh8Script", "http://truyentranh8.net");

                case MangaSite.IZTRUYENTRANH:
                    return new DefaultScraper(MangaSite.IZTRUYENTRANH, "WebScraper.Scrapers.Scripts.IzTruyenTranhScript", "http://iztruyentranh.com/");

                case MangaSite.HOCVIENTRUYENTRANH:
                    return new DefaultScraper(MangaSite.HOCVIENTRUYENTRANH, "WebScraper.Scrapers.Scripts.HocVienTruyenTranhScript", "http://truyen.academyvn.com/");

                case MangaSite.LHMANGA:
                    return new DefaultScraper(MangaSite.LHMANGA, "WebScraper.Scrapers.Scripts.LHMangaScript", "http://truyentranhlh.com/");

                case MangaSite.TRUYENTRANHMOI:
                    return new DefaultScraper(MangaSite.TRUYENTRANHMOI, "WebScraper.Scrapers.Scripts.TruyenTranhMoiScript", "http://2.truyentranhmoi.com");

                case MangaSite.MANGAK:
                    return new DefaultScraper(MangaSite.MANGAK, "WebScraper.Scrapers.Scripts.MangaKScript", "http://mangak.info");

                case MangaSite.UPTRUYEN:
                    return new DefaultScraper(MangaSite.UPTRUYEN, "WebScraper.Scrapers.Scripts.UpTruyenScript", "http://uptruyen.com");



                case MangaSite.MANGAFOX:
                    return new DefaultScraper(MangaSite.MANGAFOX, "WebScraper.Scrapers.Scripts.MangaFoxScript", "http://mangafox.la/");

                case MangaSite.MANGAPARK:
                    return new DefaultScraper(MangaSite.MANGAPARK, "WebScraper.Scrapers.Scripts.MangaParkScript", "http://mangapark.me/");

                case MangaSite.KISSMANGA:
                    return new DefaultScraper(MangaSite.KISSMANGA, "WebScraper.Scrapers.Scripts.KissMangaScript", "http://kissmanga.com/");


                default:
                    throw new NotImplementedException();
            }
        }
    }
}