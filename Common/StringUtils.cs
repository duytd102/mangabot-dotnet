using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class StringUtils
    {
        public static string GenerateOrdinal(int totalRows, int currentRowIndex)
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
