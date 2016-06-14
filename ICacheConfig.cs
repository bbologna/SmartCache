using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache
{
    public interface ICacheConfig
    {
        int AbsoluteExpiration { get; set; }
        int LockListMaxSize { get; set; }
        int MaxReports { get; set; }
        int MaxSummarySize { get; set; }
        int SlidingExpiration { get; set; }
    }
}
