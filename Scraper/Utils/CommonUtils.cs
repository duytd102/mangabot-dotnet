using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scraper.Utils
{
    public class CommonUtils
    {
        public static string GenerateOrdinalString(int totalRows, int currentRowIndex)
        {
            string name = "";
            int lengthOfName = totalRows.ToString().Length - currentRowIndex.ToString().Length;
            while (lengthOfName > 0)
            {
                name += "0";
                lengthOfName--;
            }
            name += currentRowIndex;
            return name;
        }
    }
}
