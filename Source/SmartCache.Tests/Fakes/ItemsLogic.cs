using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache.Tests.Fakes
{
    public class Items
    {
        private int count;
        public Item GetItem(QueryCriteria criteria)
        {
            count++;
            return new Item() { Id = 1 };
        }

        public int TimesCalled
        {
            get
            {
                return count;
            }
        }
    }
}
