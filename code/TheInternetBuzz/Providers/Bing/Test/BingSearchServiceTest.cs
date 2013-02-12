using System;
using System.Collections.Generic;
using NUnit.Framework;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Providers.Bing;
using TheInternetBuzz.Services.Search;

namespace TheInternetBuzz.Providers.Bing.Test
{
    [TestFixture]
    public class BingSearchServiceTest
    {
        [Test]
        public void BingSearchWebTest()
        {
            string query = "Test";
            int page = 1;
            int count = 8;
            SearchContext searchContext = new SearchContext(SearchTypeEnum.Web, query, page, count);

            SearchResultList searchResultList = new BingSearchService().Search(searchContext);
            int result = searchResultList.Count();
            Assert.That(result > 0);

            foreach (SearchResultItem item in searchResultList)
            {
                Console.WriteLine(item.Title);
            }

            Console.ReadLine();
        }

        [Test]
        public void BingSearchNewsTest()
        {
            string query = "Test";
            int page = 1;
            int count = 8;
            SearchContext searchContext = new SearchContext(SearchTypeEnum.News, query, page, count);

            SearchResultList searchResultList = new BingSearchService().Search(searchContext);
            int result = searchResultList.Count();
            Assert.That(result > 0);

            foreach (SearchResultItem item in searchResultList)
            {
                Console.WriteLine(item.Title);
            }

            Console.ReadLine();
        }

        [Test]
        public void BingSearchImagesTest()
        {
            string query = "Lady Gaga";
            int page = 1;
            int count = 8;
            SearchContext searchContext = new SearchContext(SearchTypeEnum.Image, query, page, count);

            SearchResultList searchResultList = new BingSearchService().Search(searchContext);
            int result = searchResultList.Count();
            Assert.That(result > 0);

            foreach (SearchResultItem item in searchResultList)
            {
                Console.WriteLine(item.Title + " " + item.DisplayURL);
            }

            Console.ReadLine();
        }
    }
}