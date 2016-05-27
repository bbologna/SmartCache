using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache
{
    /// <summary>
    /// Fire event to emit calculations on ongoing rankings.
    /// </summary>
    public interface IEnlapsedEvent
    {
        void SetEnlapsedEvent(Action handler);

        void Fire();
    }
}
