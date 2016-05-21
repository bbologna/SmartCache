using SmartCache.InMemoryStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using SmartCache.Utilities;

namespace SmartCache
{
    public class SmartCache<T, R> : INamedKey
            where T : ICacheItem
            where R : class
    {
        private string key;
        private ICacheConfig config;
        private Func<T, R> function;
        private ObjectCache cache;
        private TopItems<ICacheItem> topItems;

        public SmartCache(ObjectCache cache, string key, Func<T, R> function, ICacheConfig config)
        {
            this.config = config;
            this.cache = cache;
            this.key = key;
            this.function = function;
            this.topItems = new TopItems<ICacheItem>(config.MilisecondsInterval, config.LockListMaxSize, config.MaxReports, config.MaxSummarySize);
        }

        public R Invoke(T argument)
        {
            topItems.Hit(argument);
            var key = string.Join(",", this.key, argument.GetKey());

            if (topItems.GetRank().Any(x => x.Id.GetKey().ToString().Equals(argument.GetKey().ToString())) && argument.IsCachable())
            {
                return cache.AddOrGetExistingOrDefault(
                key, () =>
                {
                    return this.function.Invoke(argument);
                }, this.CreateCachePolicy());
            }
            else
            {
                return this.function.Invoke(argument);
            }
        }

        private CacheItemPolicy CreateCachePolicy()
        {
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(this.config.AbsoluteExpiration);
            return policy;
        }

        public string NamedKey()
        {
            return key;
        }
    }

    public interface INamedKey
    {
        string NamedKey();
    }

    public interface ICacheItem
    {
        string GetKey();

        bool IsCachable();
    }
}
