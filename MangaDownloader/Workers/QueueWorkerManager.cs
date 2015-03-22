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

        static QueueWorkerManager instance;
        Timer taskTimer;
        bool IsStopping = false;
        int workerLimit = 3;
        List<Task> taskList = new List<Task>();
        List<IWorker> workerList = new List<IWorker>();

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
                // TODO callback when all workers stopped
                foreach (var w in workerList)
                    w.Stop();
                IsStopping = true;
            }
        }

        public void Download(Task task, WorkerHandlers handlers)
        {
            if (SomeRules.CanDownloadTask(task.Status))
            {
                IWorker worker = WorkerFactory.CreateWorker(handlers);
                worker.Start(task);
                workerList.Add(worker);
            }
        }

        public void Stop(Task task)
        {
            foreach (var w in workerList)
            {
                if (w.GetTask().Equals(task))
                {
                    w.Stop();
                }
            }
        }

        public bool RemoveTask(Task task)
        {
            if (task.Status == Enums.TaskStatus.DOWNLOADING)
                return false;

            lock (taskList)
            {
                taskList.Remove(task);
            }
            return true;
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

            taskTimer.Start();
        }

        private int CountRunningWorkers()
        {
            int count = 0;
            foreach (var w in workerList)
            {
                if (w.IsBusy())
                    count++;
            }
            return count;
        }

        private IWorker GetFreeWorker()
        {
            foreach (var w in workerList)
            {
                if (!w.IsBusy())
                    return w;
            }
            return null;
        }

        private void CallEventSafely(Action eventAction)
        {
            if (eventAction != null)
                eventAction.Invoke();
        }
    }
}
