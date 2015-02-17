using MangaDownloader.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebScraper.Enums;

namespace MangaDownloader.Workers.Data
{
    class Task
    {
        public object Sender;
        public String Name;
        public String Url;
        public MangaSite Site;
        public LinkType Type;
        public Double Percent;
        public String Path;
        public String Description;
        public TaskStatus Status;
    }
}
