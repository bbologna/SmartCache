using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache.InMemoryStatistics
{
    public class RankItem<T>
    {
        public T Id { get; set; }
        public int Hits { get; set; }
    }
}
