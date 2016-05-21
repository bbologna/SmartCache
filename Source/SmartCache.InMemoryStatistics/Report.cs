using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache.InMemoryStatistics
{
    public class Report<T>
    {
        private DateTime generatedDate;
        private List<RankItem<T>> rankItems;
        private Func<T, T, bool> equalsComparer;

        public Report(List<T> list, int maxSize, Func<T, T, bool> equalsComparer)
        {
            generatedDate = DateTime.Now;
            rankItems = new List<RankItem<T>>();
            this.equalsComparer = equalsComparer;

            foreach (var item in list)
            {
                if (rankItems.Any(x => equalsComparer(x.Id, item)))
                {
                    var count = rankItems.Single(x => equalsComparer(x.Id, item));
                    count.Hits += 1;
                }
                else
                {
                    rankItems.Add(new RankItem<T>() { Id = item, Hits = 1 });
                }
            }
            rankItems = rankItems.OrderByDescending(x => x.Hits).Take(maxSize).ToList();
        }

        public List<RankItem<T>> Items
        {
            get { return rankItems; }
        }
    }
}
