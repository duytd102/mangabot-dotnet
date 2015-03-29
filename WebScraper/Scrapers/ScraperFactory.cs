﻿using System;
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

                default:
                    // TODO implement if has more sites
                    throw new NotImplementedException();
            }
        }
    }
}