using System;

using TheInternetBuzz.Data.Topics;

namespace TheInternetBuzz.Commands.Topics
{
    public interface ITopicCommand
    {
        TopicItem GetTopic();
    }
}