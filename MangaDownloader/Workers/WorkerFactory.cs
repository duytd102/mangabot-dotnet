using MangaDownloader.Workers.Data;
using MangaDownloader.Workers.Implement;
using System;

namespace MangaDownloader.Workers
{
    class WorkerFactory
    {
        private WorkerFactory() { }

        public static IWorker CreateWorker(WorkerHandlers handlers)
        {
            IWorker worker = new ThreadWorker();
            RegisterCallbacks(worker, handlers);
            return worker;
        }

        private static void RegisterCallbacks(IWorker worker, WorkerHandlers handlers)
        {
            if (handlers != null)
            {
                worker.Downloading += ((object sender) =>
                {
                    if (handlers.Downloading != null)
                        handlers.Downloading(sender);
                });
                worker.ProgressChanged += ((object sender, double percent) =>
                {
                    if (handlers.ProgressChanged != null)
                        handlers.ProgressChanged(sender, percent);
                });
                worker.Complete += ((object sender) =>
                {
                    if (handlers.Complete != null)
                        handlers.Complete(sender);
                });
                worker.Cancelled += ((object sender) =>
                {
                    if (handlers.Cancelled != null)
                        handlers.Cancelled(sender);
                });
                worker.Failed += ((object sender, Exception ex) =>
                {
                    if (handlers.Failed != null)
                        handlers.Failed(sender, ex);
                });
            }
        }
    }
}
