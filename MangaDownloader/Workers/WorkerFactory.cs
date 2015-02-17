using MangaDownloader.Processors;
using MangaDownloader.Workers.Data;
using MangaDownloader.Workers.Implement;

namespace MangaDownloader.Workers
{
    class WorkerFactory
    {
        private WorkerFactory() { }

        public static IWorker CreateWorker(Task task, WorkerHandlers handlers)
        {
            IProcessor processor = ProcessorFactory.CreateProcessor(task.Site);
            return new ThreadWorker(processor, task, handlers);
        }
    }
}
