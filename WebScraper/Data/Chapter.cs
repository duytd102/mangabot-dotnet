using Common.Enums;

namespace WebScraper.Data
{
    public class Chapter
    {
        string id;
        string name;
        string url;
        string localPath;
        string publishedDate;
        MangaSite site = MangaSite.BLOGTRUYEN;
        Manga manga;

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

        public string PublishedDate
        {
            get { return publishedDate; }
            set { publishedDate = value; }
        }

        public MangaSite Site
        {
            get { return site; }
            set { site = value; }
        }

        public Manga Manga
        {
            get { return manga; }
            set { manga = value; }
        }

    }
}
