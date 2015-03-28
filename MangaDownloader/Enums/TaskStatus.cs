using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader.Enums
{
    public enum TaskStatus
    {
        QUEUED,
        DOWNLOADING,
        COMPLETE,
        FAILED,
        SKIPPED,
        STOPPED
    }
}
