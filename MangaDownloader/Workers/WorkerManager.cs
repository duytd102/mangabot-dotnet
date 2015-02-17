using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MangaDownloader.Workers
{
    class WorkerManager
    {
        int workerLimit = 3;
        static WorkerManager instance;
        BackgroundWorker executor;
        List<KeyValuePair<string, IWorker>> workerList;

        private WorkerManager()
        {
            workerList = new List<KeyValuePair<string, IWorker>>();

            executor = new BackgroundWorker();
            executor.WorkerSupportsCancellation = true;
            executor.DoWork += executor_DoWork;
            executor.RunWorkerCompleted += executor_RunWorkerCompleted;
        }

        public static WorkerManager GetInstance()
        {
            if (instance == null) 
                instance = new WorkerManager();
            return instance;
        }

        void executor_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (executor.CancellationPending)
                {
                    StopAllWorkers();
                    e.Cancel = true;
                    break;
                }

                int totalBusyWorkers = countBusyWorkers();
                if (totalBusyWorkers < workerLimit)
                {
                    KeyValuePair<string, IWorker> entry = workerList.FirstOrDefault(p => !p.Value.IsBusy() && p.Value.IsQueued());
                    if (entry.Value != null)
                    {
                        entry.Value.Start();
                    }
                }
            }
        }

        void executor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void StopAllWorkers()
        {
            foreach (KeyValuePair<string, IWorker> entry in workerList)
                entry.Value.Stop();
        }

        public void Download(string url)
        {
            IWorker worker = GetWorker(url);
            if (worker != null)
                worker.Start();
        }

        public void Stop(String url)
        {
            IWorker worker = GetWorker(url);
            if (worker != null)
                worker.Stop();
        }

        public void StartQueue()
        {
            if (!executor.IsBusy)
                executor.RunWorkerAsync();
        }

        public void StopQueue()
        {
            if (executor.IsBusy)
                executor.CancelAsync();
        }

        public void InsertOrUpdateWorker(Task task, WorkerHandlers handlers)
        {
            IWorker worker = GetWorker(task.Url);
            if (worker == null)
            {
                worker = WorkerFactory.CreateWorker(task, handlers);
                workerList.Add(new KeyValuePair<string, IWorker>(task.Url, worker));
            }
            else
            {
                worker.SetTask(task);
            }
        }

        public void UpdateWorker(string url, string folderPath)
        {
            IWorker worker = GetWorker(url);
            if (worker != null)
                worker.GetTask().Path = folderPath;
        }

        public void RemoveWorker(string url)
        {
            IWorker worker = GetWorker(url);
            if (worker != null)
            {
                worker.Stop();
                workerList.Remove(GetEntry(url));
            }
        }

        private KeyValuePair<string, IWorker> GetEntry(string url)
        {
            return workerList.FirstOrDefault(p => p.Key.Equals(url));
        }

        private IWorker GetWorker(string url)
        {
            return GetEntry(url).Value;
        }

        private int countBusyWorkers()
        {
            return workerList.Count(D => D.Value.IsBusy());
        }
    }
}
