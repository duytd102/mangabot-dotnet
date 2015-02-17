using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader.Workers
{
    interface IWorker
    {
        void Start();
        void Stop();

        bool IsBusy();
        bool IsQueued();
        void SetTask(Task task);
        Task GetTask();
    }
}
