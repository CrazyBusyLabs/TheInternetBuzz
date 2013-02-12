using System;
using System.Collections.Generic;
using NUnit.Framework;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Providers.Flickr;

namespace TheInternetBuzz.Providers.Flickr.Test
{
    [TestFixture]
    public class FlickrSearchServiceTest
    {
        [Test]
        public void FlickrSeachTest()
        {
            string query = "Lady Gaga";
            int page = 1;
            int count = 1;
            SearchContext searchContext = new SearchContext(SearchTypeEnum.Image, query, page, count);

            SearchResultList searchResultList = new FlickrSearchService().Search(searchContext);
            int result = searchResultList.Count();
            Assert.That(result > 0);

            foreach (SearchResultItem item in searchResultList)
            {
                Console.WriteLine(item.ImageURL);
            }

            Console.ReadLine();
        }
    }
}