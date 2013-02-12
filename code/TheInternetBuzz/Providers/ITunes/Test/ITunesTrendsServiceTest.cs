using System;
using System.Collections.Generic;
using NUnit.Framework;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Providers.ITunes;

namespace TheInternetBuzz.Providers.ITunes.Test
{
    [TestFixture]
    public class ITunesTrendsServiceTest
    {
        [Test]
        public void ITunesTrendsTop10SongsTest()
        {

            TrendsList trendsList = new ITunesTrendsService(ITunesTypeEnum.Top10Songs).GetTrends();

            Assert.That(trendsList.Count() > 0);

            foreach (TrendItem trend in trendsList)
            {
                Console.WriteLine(trend.Title);
            }

            Console.ReadLine();
        }

        [Test]
        public void ITunesTrendsTopMoviesTest()
        {

            TrendsList trendsList = new ITunesTrendsService(ITunesTypeEnum.TopMovies).GetTrends();

            Assert.That(trendsList.Count() > 0);

            foreach (TrendItem trend in trendsList)
            {
                Console.WriteLine(trend.Title);
            }

            Console.ReadLine();
        }
    }
}