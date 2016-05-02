using Common.Enums;

namespace WebScraper.Data
{
    public class Page
    {
        string id = "";
        string name = "";
        string url = "";
        string localPath = "";
        MangaSite site = MangaSite.BLOGTRUYEN;
        Chapter chapter;

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

        public MangaSite Site
        {
            get { return site; }
            set { site = value; }
        }

        public Chapter Chapter
        {
            get { return chapter; }
            set { chapter = value; }
        }
    }
}
