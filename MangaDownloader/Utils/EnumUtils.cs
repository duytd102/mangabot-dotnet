using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader.Utils
{
    class EnumUtils
    {
        public static T Parse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
