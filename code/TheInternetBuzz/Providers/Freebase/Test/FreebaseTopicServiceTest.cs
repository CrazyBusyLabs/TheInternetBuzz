using System;
using System.Collections.Generic;
using NUnit.Framework;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Topics;
using TheInternetBuzz.Providers.Freebase;

namespace TheInternetBuzz.Providers.Freebase.Test
{
    [TestFixture]
    public class FreebaseTopicServiceTest
    {
        [Test]
        public void FreebaseTopicTest()
        {
            TopicItem topicItem = new TopicItem("ladygaga", "Lady Gaga");
            topicItem.Query = "Lady Gaga";

            new FreebaseTopicService().FillTopic(topicItem);

            Assert.That(topicItem.FreebaseSummary != null);
            Assert.That(topicItem.FreebaseSummary.Length > 0);

            Console.WriteLine(topicItem.FreebaseSummary);
        }
    }
}