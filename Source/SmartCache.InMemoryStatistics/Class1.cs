using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache.InMemoryStatistics
{
    public class LockList<T>
    {
        private List<T> items;
        private object lockObject;

        public LockList()
        {
            items = new List<T>();
            lockObject = new object();
        }

        public int Count { get; private set; }

        public bool Contains(T i)
        {
            lock (lockObject)
            {
                return items.Contains(i);
            }
        }

        public List<T> Clone()
        {
            lock (lockObject)
            {
                return items.ToList();
            }
        }

        public void Add(T i)
        {
            lock (lockObject)
            {
                this.items.Add(i);
                Count += 1;
            }
        }

        public void Remove(T i)
        {
            lock (lockObject)
            {
                this.items.Remove(i);
                Count -= 1;
            }
        }

    }
}
