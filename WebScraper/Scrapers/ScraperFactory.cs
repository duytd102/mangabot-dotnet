using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;
using WebScraper.Scrapers.Implement;

namespace WebScraper.Scrapers
{
    public class ScraperFactory
    {
        public static IScraper GetInstance(MangaSite site)
        {
            switch (site)
            {
                case MangaSite.BLOGTRUYEN:
                    return new BlogTruyenScraper();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
