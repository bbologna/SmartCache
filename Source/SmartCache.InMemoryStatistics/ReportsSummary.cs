using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCache.InMemoryStatistics
{
    public class ReportsSummary<T>
    {
        private List<Report<T>> reports;
        private List<RankItem<T>> reportsRank;
        private DateTime creationDate;
        private int maxReports;
        private int maxSummarySize;
        private object lockObject;
        private Func<T, T, bool> equalsFunction;

        public ReportsSummary(int maxReports, int maxSummarySize, Func<T, T, bool> equalsFunction)
        {
            reports = new List<Report<T>>();
            reportsRank = new List<RankItem<T>>();
            creationDate = DateTime.Now;
            this.maxReports = maxReports;
            this.maxSummarySize = maxSummarySize;
            this.lockObject = new object();
            this.equalsFunction = equalsFunction;
        }

        public void AddReport(Report<T> report)
        {
            if (this.reports.Count() == maxReports)
            {
                this.reports.Remove(this.reports.First());
            }
            this.reports.Add(report);
            ExcecuteTop();
        }

        public void ExcecuteTop()
        {
            var rank = new List<RankItem<T>>();
            foreach (var report in reports)
            {
                foreach (var reportItem in report.Items)
                {
                    if (rank.Any(x => equalsFunction(x.Id, reportItem.Id)))
                    {
                        rank.Single(x => equalsFunction(x.Id, reportItem.Id)).Hits += reportItem.Hits;
                    }
                    else
                    {
                        rank.Add(new RankItem<T>() { Hits = reportItem.Hits, Id = reportItem.Id });
                    }
                }
            }
            rank = rank.OrderByDescending(x => x.Hits).Take(maxSummarySize).ToList();
            lock (lockObject)
            {
                reportsRank = rank;
            }
        }

        public List<RankItem<T>> CloneReport()
        {
            lock (lockObject)
            {
                return reportsRank.ToList();
            }
        }

        public int GetCountOfReports()
        {
            lock (lockObject)
            {
                return reports.Count();
            }
        }
    }
}
