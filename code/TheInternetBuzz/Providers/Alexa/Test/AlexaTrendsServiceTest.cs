using System;
using System.Collections.Generic;
using NUnit.Framework;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Providers.Alexa.Test
{
    [TestFixture]
    public class AlexaTrendsServiceTest
    {
        [Test]
        public void AlexaTrendsTest()
        {

            TrendsList trendsList = new AlexaTrendsService().GetTrends();

            Assert.That(trendsList.Count() > 0);

            foreach (TrendItem trend in trendsList)
            {
                Console.WriteLine(trend.Title);
            }

            Console.ReadLine();
        }
    }
}