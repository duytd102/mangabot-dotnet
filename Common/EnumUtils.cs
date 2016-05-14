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

        public static string Capitalize<T>(T enumVar)
        {
            string type = enumVar.ToString().ToLower();
            return string.Format("{0}{1}", type.Substring(0, 1).ToUpper(), type.Substring(1));
        }

        public static List<T> ToList<T>()
        {
            List<T> enumNames = new List<T>();
            foreach(T e in Enum.GetValues(typeof(T)))
            {
                enumNames.Add(e);
            }
            return enumNames;
        }
    }
}
