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
        public static IProcessor CreateProcessor(MangaSite site)
        {
            return new MultiplePagesProcessor(site);
        }
    }
}
