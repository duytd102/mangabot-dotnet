using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ListUtils
    {
        public static void Swap<T>(List<T> list, int fromIndex, int toIndex)
        {
            if (fromIndex == toIndex) return;
            if (fromIndex < 0 || toIndex < 0) return;
            if (fromIndex >= list.Count || toIndex >= list.Count) return;

            T fromTask = list[fromIndex];
            T toTask = list[toIndex];

            list.Remove(fromTask);
            list.Remove(toTask);

            if (fromIndex < toIndex)
            {
                // Move down
                list.Insert(fromIndex, toTask);
                list.Insert(toIndex, fromTask);
            }
            else
            {
                // Move up
                list.Insert(toIndex, fromTask);
                list.Insert(fromIndex, toTask);
            }
        }
    }
}
