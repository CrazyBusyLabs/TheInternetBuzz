using System;
using System.Globalization;
using System.Threading;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Data.Topics;

using TheInternetBuzz.Providers.Freebase;
using TheInternetBuzz.Providers.Wikipedia;

using TheInternetBuzz.Services.Audit;
using TheInternetBuzz.Services.Error;

namespace TheInternetBuzz.Services.Topics
{
    public class TopicService
    {
        public TopicItem GetTopic(string query)
        {
            AuditServiceItem auditServiceItem = AuditService.Register("TopicService", "GetTopic", query);
            AuditService.Start(auditServiceItem);

            TopicItem topicItem = TopicCacheHelper.ReadTopicItem(query);

            if (topicItem == null)
            {
                try
                {
                    String displayQuery = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(query);
                    topicItem = new TopicItem(displayQuery, displayQuery);
                    topicItem.Query = displayQuery;

                    ManualResetEvent[] doneEvents = new ManualResetEvent[2];

                    doneEvents[0] = new ManualResetEvent(false);
                    TopicWorkerThread wikipediaTopicWorkerThread = new TopicWorkerThread(ProviderEnum.Wikipedia, topicItem, doneEvents[0]);
                    ThreadPool.QueueUserWorkItem(wikipediaTopicWorkerThread.ThreadPoolCallback);

                    doneEvents[1] = new ManualResetEvent(false);
                    TopicWorkerThread freebaseTopicWorkerThread = new TopicWorkerThread(ProviderEnum.Freebase, topicItem, doneEvents[1]);
                    ThreadPool.QueueUserWorkItem(freebaseTopicWorkerThread.ThreadPoolCallback);

                    WaitHandle.WaitAll(doneEvents);

                    TopicCacheHelper.CacheTopicItem(query, topicItem);
                }
                catch (Exception exception)
                {
                    ErrorService.Log("TopicService", "GetTopic", query, exception);
                }
            }

            AuditService.End(auditServiceItem);

            return topicItem;
        }
    }
}
