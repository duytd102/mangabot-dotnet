using MangaDownloader.Enums;
using Scraper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scraper.Data
{
    public class Manga
    {
        string id = "";
        string name = "";
        string url = "";
        string localPath = "";
        string thumbnail = "";
        string description = "";
        MangaSite site = MangaSite.BLOGTRUYEN;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public string LocalPath
        {
            get { return localPath; }
            set { localPath = value; }
        }
        public string Thumbnail
        {
            get { return thumbnail; }
            set { thumbnail = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public MangaSite Site
        {
            get { return site; }
            set { site = value; }
        }
    }
}
