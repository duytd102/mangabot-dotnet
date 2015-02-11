using MangaDownloader.Processors.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;

namespace MangaDownloader.Processors
{
    class ProcessorFactory
    {
        public static IProcessor GetInstance(MangaSite site)
        {
            switch (site)
            {
                case MangaSite.BLOGTRUYEN:
                    return new MultiplePagesProcessor(site);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
