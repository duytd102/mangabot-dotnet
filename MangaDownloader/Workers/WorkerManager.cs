using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
namespace MangaDownloader.Workers
{
    class WorkerManager
    {
        public event Action AllWorkersStopped;

        int workerLimit = 3;
        static WorkerManager instance;
        BackgroundWorker executor;
        BackgroundWorker checkWorkerStatus;
        List<KeyValuePair<string, IWorker>> workerList;

        private WorkerManager()
        {
            workerList = new List<KeyValuePair<string, IWorker>>();

            executor = new BackgroundWorker();
            executor.WorkerSupportsCancellation = true;
            executor.DoWork += executor_DoWork;
            executor.RunWorkerCompleted += executor_RunWorkerCompleted;

            checkWorkerStatus = new BackgroundWorker();
            checkWorkerStatus.DoWork += checkWorkerStatus_DoWork;
            checkWorkerStatus.RunWorkerCompleted += checkWorkerStatus_RunWorkerCompleted;
        }

        void checkWorkerStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach(KeyValuePair<string, IWorker> entry in workerList)
            {
                if (entry.Value.IsBusy())
                {
                    e.Result = false;
                    break;
                }
            }
            e.Result = true;
        }

        void checkWorkerStatus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool allWorkersStopped = (bool)e.Result;
            if (allWorkersStopped)
                AllWorkersStopped();
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
                    checkWorkerStatus.RunWorkerAsync();
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
            if (e.Cancelled)
            {
                AllWorkersStopped();
            }
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
