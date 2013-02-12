using System;

using TheInternetBuzz.Data.Topics;
using TheInternetBuzz.Services.Topics;

namespace TheInternetBuzz.Commands.Topics
{
    public class TopicCommand : ITopicCommand
    {
        private string query = null;

        public TopicCommand(string query)
        {
            this.query = query;
        }

        public TopicItem GetTopic()
        {
            return new TopicService().GetTopic(query);
        }
    }
}