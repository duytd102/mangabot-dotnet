using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;
using WebScraper.Processors.Implement;

namespace WebScraper.Processors
{
    public class ProcessorFactory
    {
        public static IProcessor CreateProcessor(MangaSite site)
        {
            return new MultiplePagesProcessor(site);
        }
    }
}
