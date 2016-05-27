using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache.Tests.Fakes
{
    internal class ManualEnlapsedFire : IEnlapsedEvent
    {
        private Action enlapsedEventHandler;

        public void SetEnlapsedEvent(Action handler)
        {
            enlapsedEventHandler = handler;
        }

        public void Fire()
        {
            enlapsedEventHandler.Invoke();
        }
    }
}
