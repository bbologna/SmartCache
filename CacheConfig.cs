using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache
{
    public class CacheConfig : ICacheConfig
    {
        private string cacheType;
        private const string milisecondsInterval = "TopItems-MilisecondsInterval";
        private const string lockListMaxSize = "TopItems-LockListMaxSize";
        private const string maxReports = "TopItems-MaxReports";
        private const string maxSummarySize = "TopItems-MaxSummarySize";
        private const string absoluteExpiration = "Cache-AbsoluteExpiration";
        private const string slidingExpiration = "Cache-SlidingExpiration";

        public CacheConfig(string cacheType)
        {
            this.cacheType = cacheType;
            LoadConfig();
        }

        private void LoadConfig()
        {
            this.MilisecondsInterval = GetInt(string.Format("{0}-{1}", cacheType, milisecondsInterval));
            this.LockListMaxSize = GetInt(string.Format("{0}-{1}", cacheType, lockListMaxSize));
            this.MaxReports = GetInt(string.Format("{0}-{1}", cacheType, maxReports));
            this.MaxSummarySize = GetInt(string.Format("{0}-{1}", cacheType, maxSummarySize));
            this.AbsoluteExpiration = GetInt(string.Format("{0}-{1}", cacheType, absoluteExpiration));
            this.SlidingExpiration = GetInt(string.Format("{0}-{1}", cacheType, slidingExpiration));
        }

        public int MilisecondsInterval { get; set; }
        public int LockListMaxSize { get; set; }
        public int MaxReports { get; set; }
        public int MaxSummarySize { get; set; }
        public int AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }

        private int GetInt(string key)
        {
            return int.Parse(ConfigurationManager.AppSettings[key]);
        }
    }
}
