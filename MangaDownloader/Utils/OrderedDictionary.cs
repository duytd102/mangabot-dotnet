using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MangaDownloader.Utils
{
    public class OrderedDictionary<TKey, TValue> : System.Collections.Specialized.OrderedDictionary
    {
        public OrderedDictionary()
            : base()
        {

        }

        public void Add(TKey key, TValue value)
        {
            base.Add(key, value);
        }

        public bool Contains(TKey key)
        {
            return base.Contains(key);
        }

        public TValue GetValue(TKey key)
        {
            return (TValue)base[key];
        }
    }
}
