namespace MangaDownloader.Workers.Data
{
    class DownloadData
    {
        public double Percent;
        public object DataRowSender;

        public DownloadData(object dataRowSender, double percent)
        {
            this.DataRowSender = dataRowSender;
            this.Percent = percent;
        }
    }
}
