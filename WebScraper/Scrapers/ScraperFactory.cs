using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;
using WebScraper.Scrapers.Implement;

namespace WebScraper.Scrapers
{
    class ScraperFactory
    {
        public static IScraper CreateScraper(MangaSite site)
        {
            switch (site)
            {
                case MangaSite.BLOGTRUYEN:
                    return new BlogTruyenScraper();
                case MangaSite.MANGAFOX:
                    return new MangaFoxScraper();
                case MangaSite.VECHAI:
                    return new VeChaiScraper();
                case MangaSite.MANGAVN:
                    return new MangaVnScraper();
                case MangaSite.MANGA24H:
                    return new Manga24hScraper();
                case MangaSite.TRUYENTRANHTUAN:
                    return new TruyenTranhTuanScraper();
                case MangaSite.TRUYENTRANH8:
                    return new TruyenTranh8Scraper();
                case MangaSite.IZMANGA:
                    return new IzMangaScraper();
                case MangaSite.KISSMANGA:
                    return new KissMangaScraper();
                case MangaSite.OTAKUFC:
                    return new OtakufcScraper();
                case MangaSite.HOCVIENTRUYENTRANH:
                    return new HocVienTruyenTranhScraper();

                default:
                    // TODO implement if has more sites
                    throw new NotImplementedException();
            }
        }
    }
}