using System;
using System.Threading;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Services.Trends
{
    public class TrendsWorkerThread
    {
        private ManualResetEvent DoneEvent = null;
        private ITrendsService TrendsService;

        public TrendsWorkerThread(ITrendsService trendsService, ManualResetEvent doneEvent)
        {
            this.TrendsService = trendsService;
            this.DoneEvent = doneEvent;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            TrendsList = TrendsService.GetTrends();
            DoneEvent.Set();
        }

        public TrendsList TrendsList
        {
            get;
            set;
        }
    }
}
