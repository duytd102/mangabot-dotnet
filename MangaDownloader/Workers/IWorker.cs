using MangaDownloader.Workers.Data;
using System;

namespace MangaDownloader.Workers
{
    interface IWorker
    {
        event Action<object> Downloading;
        event Action<object, double> ProgressChanged;
        event Action<object> Complete;
        event Action<object> Cancelled;
        event Action<object, Exception> Failed;

        void Start(Task task);

        void Stop();

        bool IsBusy();

        Task GetTask();
    }
}
