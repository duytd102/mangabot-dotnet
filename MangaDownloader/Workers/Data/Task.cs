using Common.Enums;
using MangaDownloader.Enums;
using System;

namespace MangaDownloader.Workers.Data
{
    public class Task
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
