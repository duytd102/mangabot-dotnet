using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader.Enums
{
    enum TaskStatus
    {
        QUEUED,
        DOWNLOADING,
        COMPLETE,
        FAILED,
        SKIPPED,
        STOPPED,
        PAUSED
    }
}
