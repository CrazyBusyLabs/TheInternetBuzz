using System;
using System.Collections;

using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Topics
{
    public class TopicList : List 
    {
        public void AddTopics(TopicList topicList)
        {
            if (topicList != null)
            {
                foreach (TopicItem newTopic in topicList)
                {
                    AddTopic(newTopic);
                }
            }
        }

        public void AddTopic(TopicItem newTopic)
        {
            TopicItem topic = GetTopic(newTopic.ID);
            if (topic == null)
            {
                Add(topic);
            }
        }

        public bool ContainsTopic(string ID)
        {
            return GetTopic(ID) != null;
        }

        public TopicItem GetTopic(string ID)
        {
            TopicItem foundTopicItem = null;

            foreach (TopicItem topicItem in this)
            {
                if (topicItem.ID.Equals(ID, StringComparison.CurrentCultureIgnoreCase))
                {
                    foundTopicItem = topicItem;
                    break;
                }
            }

            return foundTopicItem;
        }
    }
}
