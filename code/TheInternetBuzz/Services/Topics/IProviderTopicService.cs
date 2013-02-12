using System;

using TheInternetBuzz.Data.Topics;

namespace TheInternetBuzz.Services.Topics
{
    public interface IProviderTopicService
    {
        void FillTopic(TopicItem topicItem);
    }
}
