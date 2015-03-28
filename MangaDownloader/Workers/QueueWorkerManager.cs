using MangaDownloader.Utils;
using MangaDownloader.Workers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;

namespace MangaDownloader.Workers
{
    class QueueWorkerManager
    {
        public event Action AllWorkersStopped;
        public bool IsBusy = false;

        const int MANUAL_WORKER_LIMIT = 10;
        static QueueWorkerManager instance;
        Timer taskTimer;
        bool IsStopping = false;
        int workerLimit = 3;
        List<Task> taskList = new List<Task>();
        List<IWorker> workerList = new List<IWorker>();
        List<IWorker> manualWorkerList = new List<IWorker>();

        private QueueWorkerManager()
        {
            taskTimer = new Timer(1000);
            taskTimer.AutoReset = false;
            taskTimer.Elapsed += taskTimer_Elapsed;
        }

        public static QueueWorkerManager GetInstance()
        {
            if (instance == null)
                instance = new QueueWorkerManager();
            return instance;
        }

        public void StartQueue(int workerLimit, List<Task> taskList, WorkerHandlers handlers)
        {
            if (!IsBusy)
            {
                this.workerLimit = workerLimit;

                this.taskList.Clear();
                foreach (var task in taskList)
                    this.taskList.Add(task);

                if (workerList.Count < workerLimit)
                    for (int i = workerList.Count + 1; i <= workerLimit; i++)
                        workerList.Add(WorkerFactory.CreateWorker(handlers));

                IsBusy = true;
                IsStopping = false;
                taskTimer.Start();
            }
        }

        public void StopQueue()
        {
            if (IsBusy)
            {
                foreach (var w in workerList)
                    w.Stop();

                IsStopping = true;
            }
        }

        public void Download(Task task, WorkerHandlers handlers)
        {
            int totalRunningManualWorkers = CountRunningManualWorkers();
            if (totalRunningManualWorkers < MANUAL_WORKER_LIMIT && SomeRules.CanDownloadTask(task.Status))
            {
                IWorker worker = GetFreeManualWorker();
                if (worker == null) 
                    worker = WorkerFactory.CreateWorker(handlers);
                worker.Start(task);
                manualWorkerList.Add(worker);
            }
        }

        public void Stop(Task task)
        {
            foreach (var w in workerList)
                if (w.GetTask() != null && w.GetTask().Equals(task))
                    w.Stop();

            foreach (var mw in manualWorkerList)
                if (mw.GetTask() != null && mw.GetTask().Equals(task))
                    mw.Stop();
        }

        void taskTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int totalRunningWorkers = CountRunningWorkers();
            bool noTask = taskList.Count == 0 && totalRunningWorkers == 0;
            bool canStop = IsStopping && totalRunningWorkers == 0;

            if (noTask || canStop)
            {
                IsBusy = false;
                IsStopping = false;
                CallEventSafely(AllWorkersStopped);
                return;
            }
            else if (!IsStopping)
            {
                while (taskList.Count > 0 && totalRunningWorkers < workerLimit)
                {
                    int firstIndex = 0;
                    Task firstTask = taskList[firstIndex];
                    taskList.RemoveAt(firstIndex);

                    if (SomeRules.CanDownloadTask(firstTask.Status))
                    {
                        IWorker worker = GetFreeWorker();
                        if (worker != null)
                            worker.Start(firstTask);
                    }

                    totalRunningWorkers = CountRunningWorkers();
                }
            }

            taskTimer.Start();
        }

        private int CountRunningWorkers()
        {
            int count = 0;
            foreach (var w in workerList)
                if (w.IsBusy())
                    count++;
            return count;
        }

        private int CountRunningManualWorkers()
        {
            int count = 0;
            foreach (var w in manualWorkerList)
                if (w.IsBusy())
                    count++;
            return count;
        }

        private IWorker GetFreeWorker()
        {
            foreach (var w in workerList)
                if (!w.IsBusy())
                    return w;
            return null;
        }

        private IWorker GetFreeManualWorker()
        {
            foreach (var w in manualWorkerList)
                if (!w.IsBusy())
                    return w;
            return null;
        }

        private void CallEventSafely(Action eventAction)
        {
            if (eventAction != null)
                eventAction.Invoke();
        }
    }
}
