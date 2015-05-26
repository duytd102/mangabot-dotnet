using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class EnumUtils
    {
        public static T Parse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static String Capitalize<T>(T enumVar)
        {
            String type = enumVar.ToString().ToLower();
            return String.Format("{0}{1}", type.Substring(0, 1).ToUpper(), type.Substring(1));
        }
    }
}
