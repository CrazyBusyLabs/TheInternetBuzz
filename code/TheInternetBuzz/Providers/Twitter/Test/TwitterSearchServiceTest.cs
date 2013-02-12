using System;
using NUnit.Framework;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Providers.Twitter;
using TheInternetBuzz.Services.Search;

namespace TheInternetBuzz.Providers.Twitter.Test
{
    [TestFixture]
    public class TwitterSearchServiceTest
    {
        [Test]
        public void TwitterSearchWorldRecord()
        {
            string query = "World Record";
            int page = 1;
            int count = 8;
            SearchContext searchContext = new SearchContext(SearchTypeEnum.Tweet, query, page, count);

            SearchResultList searchResultList = new TwitterSearchService().Search(searchContext);
            int result = searchResultList.Count();
            Assert.That(result > 0);
        }

        [Test]
        public void TwitterSearchInvalidCaracters()
        {
            string query = "??37.2";
            int page = 1;
            int count = 8;
            SearchContext searchContext = new SearchContext(SearchTypeEnum.Tweet, query, page, count);

            SearchResultList searchResultList = new TwitterSearchService().Search(searchContext);
            int result = searchResultList.Count();
            System.Console.WriteLine("result : " + result);
            Assert.That(result > 0);
        }
    }
}