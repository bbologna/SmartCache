using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache.Tests.Fakes
{
    public class QueryCriteria : ICacheItem
    {
        private string key;
        private bool isCachable;

        public QueryCriteria(string key, bool isCachable)
        {
            this.key = key;
            this.isCachable = isCachable;
        }
        public string GetKey()
        {
            return key;
        }

        public bool IsCachable()
        {
            return isCachable;
        }
    }
}
