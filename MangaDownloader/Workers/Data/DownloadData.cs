using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
