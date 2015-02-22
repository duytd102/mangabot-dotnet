using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader.Workers
{
    interface IWorker
    {
        event Action<object> Downloading;
        event Action<object, double> ProgressChanged;
        event Action<object> Complete;
        event Action<object> Cancelled;
        event Action<object, Exception> Failed;

        void Start();
        void Stop();

        bool IsBusy();
        bool IsQueued();
        void SetTask(Task task);
        Task GetTask();
    }
}
