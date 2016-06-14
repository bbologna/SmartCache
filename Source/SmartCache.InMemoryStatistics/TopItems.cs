using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace SmartCache.InMemoryStatistics
{
    public class TopItems<T>
    {
        private LockList<T> hits;
        private Timer timer;
        private ReportsSummary<T> summary;
        private int milisecondsInterval;
        private int lockListMaxSize;
        private Func<T, T, bool> equalsFunction;


        public TopItems(int milisecondsInterval, int lockListMaxSize, int maxReports, int maxSummarySize)
            : this(milisecondsInterval, lockListMaxSize, maxReports, maxSummarySize, (T x, T y) => { return x.Equals(y); })
        {
        }

        public TopItems(int milisecondsInterval, int lockListMaxSize, int maxReports, int maxSummarySize, Func<T, T, bool> equalsFunction)
        {
            this.milisecondsInterval = milisecondsInterval;
            this.lockListMaxSize = lockListMaxSize;
            summary = new ReportsSummary<T>(maxReports, maxSummarySize, equalsFunction);
            hits = new LockList<T>();
            this.equalsFunction = equalsFunction;
            CreateTimer();
        }

        private void CreateTimer()
        {
            timer = new Timer(milisecondsInterval);
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(Reset);
            timer.Enabled = true;
        }

        private void Reset(object obj, ElapsedEventArgs args)
        {
            //Get copy of items
            var copyList = hits.Clone();

            //Make Report
            summary.AddReport(new Report<T>(hits.Clone(), lockListMaxSize, equalsFunction));

            //Reset List
            this.hits = new LockList<T>();
        }

        public List<RankItem<T>> GetRank()
        {
            return this.summary.CloneReport();
        }

        public int ReportsCount()
        {
            return this.summary.GetCountOfReports();
        }

        public void Hit(T id)
        {
            hits.Add(id);
        }
    }
}
