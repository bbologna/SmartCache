using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace SmartCache
{
    /// <summary>
    /// Encapsulates System.Timer
    /// </summary>
    internal class SystemTimer : IEnlapsedEvent
    {
        private int miliseconds;
        private Action enlapsedEventHandler;
        private Timer timer;

        public SystemTimer(int milliseconds)
        {
            this.miliseconds = milliseconds;
        }

        public void SetEnlapsedEvent(Action handler)
        {
            enlapsedEventHandler = handler;
            timer = new Timer();
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(Fire);
            timer.Enabled = true;
        }

        public void Fire()
        {
            enlapsedEventHandler.Invoke();
        }

        private void Fire(object obj, ElapsedEventArgs args)
        {
            Fire();
        }
    }
}
