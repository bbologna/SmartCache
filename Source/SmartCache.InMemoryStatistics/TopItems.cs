using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SmartCache.InMemoryStatistics
{
    public class TopItems<T>
    {
        private LockList<T> hits;
        private ReportsSummary<T> summary;
        private int lockListMaxSize;
        private Func<T, T, bool> equalsFunction;
        private IEnlapsedEvent enlapsedEvent;


        public TopItems(IEnlapsedEvent enlapsed, int lockListMaxSize, int maxReports, int maxSummarySize)
            : this(lockListMaxSize, maxReports, maxSummarySize, (T x, T y) => { return x.Equals(y); })
        {
            this.enlapsedEvent = enlapsed;
            this.enlapsedEvent.SetEnlapsedEvent(Reset);
        }

        public TopItems(int lockListMaxSize, int maxReports, int maxSummarySize, Func<T, T, bool> equalsFunction)
        {
            this.lockListMaxSize = lockListMaxSize;
            summary = new ReportsSummary<T>(maxReports, maxSummarySize, equalsFunction);
            hits = new LockList<T>();
            this.equalsFunction = equalsFunction;
        }

        private void Reset()
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
