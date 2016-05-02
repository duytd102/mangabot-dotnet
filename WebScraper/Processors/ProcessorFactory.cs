using Common.Enums;
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
