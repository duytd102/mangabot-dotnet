using MangaDownloader.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader.Utils
{
    class SomeRules
    {
        public static bool CanDownloadTask(TaskStatus status)
        {
            return status == TaskStatus.QUEUED || status == TaskStatus.STOPPED;
        }

        public static bool CanReDownloadTask(TaskStatus status)
        {
            return status == TaskStatus.COMPLETE || status == TaskStatus.FAILED;
        }

        public static bool CanStopTask(TaskStatus status)
        {
            return status == TaskStatus.DOWNLOADING;
        }

        public static bool CanResetTask(TaskStatus status)
        {
            return status == TaskStatus.FAILED || status == TaskStatus.SKIPPED;
        }

        public static bool CanSkipTask(TaskStatus status)
        {
            return status == TaskStatus.FAILED || status == TaskStatus.SKIPPED;
        }

        public static bool CanAssignSaveToTask(TaskStatus status)
        {
            return status == TaskStatus.QUEUED || status == TaskStatus.SKIPPED;
        }

        public static bool CanRemoveTask(TaskStatus status)
        {
            return true;
        }
    }
}
