using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache
{
    public class SmartCacheResolver
    {
        private object lockObject;
        private static SmartCacheResolver instance;

        private List<INamedKey> caches;
        private SmartCacheResolver()
        {
            this.caches = new List<INamedKey>();
            this.lockObject = new object();
        }
        public static SmartCacheResolver Instance { get { if (instance == null) instance = new SmartCacheResolver(); return instance; } }

        public void Add<T, R>(SmartCache<T, R> cache)
            where T : ICacheItem
            where R : class
        {
            lock (lockObject)
            {
                if (!this.caches.Any(x => x.NamedKey().Equals(cache.NamedKey(), StringComparison.InvariantCultureIgnoreCase)))
                {
                    this.caches.Add(cache);
                }
                else { throw new ApplicationException("Duplicate Key"); }
            }
        }

        public SmartCache<T, R> GetCache<T, R>(string key)
            where R : class
            where T : ICacheItem
        {
            lock (lockObject)
            {
                return this.caches.Single(x => x.NamedKey().Equals(key, StringComparison.InvariantCultureIgnoreCase)) as SmartCache<T, R>;
            }
        }
    }
}
