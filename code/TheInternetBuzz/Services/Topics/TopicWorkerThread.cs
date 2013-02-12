using System;
using System.Web;
using System.Threading;
using TheInternetBuzz.Data;
using TheInternetBuzz.Providers.Freebase;
using TheInternetBuzz.Providers.Wikipedia;
using TheInternetBuzz.Data.Topics;

namespace TheInternetBuzz.Services.Topics
{
    public class TopicWorkerThread
    {
        private ManualResetEvent DoneEvent = null;
        private ProviderEnum provider;
        private TopicItem topicItem = null;

        public TopicWorkerThread(ProviderEnum provider, TopicItem topicItem, ManualResetEvent doneEvent)
        {
            this.provider = provider;
            this.topicItem = topicItem;
            this.DoneEvent = doneEvent;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            IProviderTopicService topicService;
            switch (provider)
            {
                case ProviderEnum.Freebase:
                    topicService = new FreebaseTopicService();
                    break;

                case ProviderEnum.Wikipedia:
                    topicService = new WikipediaTopicService();
                    break;

                default:
                    topicService = new FreebaseTopicService();
                    break;

            }
            topicService.FillTopic(topicItem);
            DoneEvent.Set();
        }
    }
}
