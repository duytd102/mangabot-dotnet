using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class VersionData
    {
        public String Version;
        public DateTime ReleaseDate;
        public String URL;
        public String ChangeLogs;

        public List<String> GetChangeLogs()
        {
            List<String> list = new List<string>();
            string[] arr = this.ChangeLogs.Split(new String[] { "<br>" }, StringSplitOptions.None);
            list.AddRange(arr);
            return list;
        }
    }
}
