using ObjectCacheExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache.Utilities
{
    public static class Extensions
    {
        public static T AddOrGetExistingOrDefault<T>(this ObjectCache cache, string key, Func<T> fallbackFunction, CacheItemPolicy policy) where T : class
        {
            var ret = cache.Get(key) as T;
            if (ret != null) return ret;
            ret = fallbackFunction();
            if (ret == null) return default(T);
            return cache.AddOrGetExisting(key, () => ret, policy);
        }
    }
}
