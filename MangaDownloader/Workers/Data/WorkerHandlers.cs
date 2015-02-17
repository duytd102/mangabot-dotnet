using System;

namespace MangaDownloader.Workers.Data
{
    class WorkerHandlers
    {
        public Action<object> Downloading;
        public Action<object, double> ProgressChanged;
        public Action<object> Complete;
        public Action<object> Cancelled;
        public Action<object, Exception> Failed;
    }
}
