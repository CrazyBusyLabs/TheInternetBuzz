using System;
using System.Xml;
using System.Net;
using System.Collections.Generic;
using System.Data.Services.Client;
using TheInternetBuzz.Connectors.XML;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Util;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Providers.Bing
{
    public class BingSearchService : IProviderSearchService
    {
        private const string HAS_INSUFFICENT_BALANCE = "Insufficient balance";

        private static bool hasInsufficientBalance = false;

        public BingSearchService()
        {
        }

        public SearchResultList Search(SearchContext searchContext)
        {
            SearchResultList resultList = null;
            if (hasInsufficientBalance)
            {
                LogService.Warn(this.GetType(),"Insufficient blance for Bing search request");
            }
            else
            {
                switch (searchContext.SearchType)
                {
                    case SearchTypeEnum.News:
                        resultList = SearchNews(searchContext);
                        break;

                    case SearchTypeEnum.Web:
                        resultList = SearchWeb(searchContext);
                        break;

                    case SearchTypeEnum.Image:
                        resultList = SearchImages(searchContext);
                        break;

                    default:
                        resultList = new SearchResultList();
                        break;
                }
            }
            return resultList;
        }

        private SearchResultList SearchNews(SearchContext searchContext)
        {
            SearchResultList resultList = new SearchResultList();
            
            try
            {
                string query = searchContext.Query;
                //int page = searchContext.Page;
                //int count = searchContext.Count;
                //int start = (page - 1) * count;
                string accountKey = ConfigService.GetConfig(ConfigKeys.BING_API_KEY, "");

                BingSearchContainer bingContainer = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/Search/News"));
                bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);

                DataServiceQuery<NewsResult> newsQuery = bingContainer.News(query, "en-us", "strict", null, null, null, null, null);
                IEnumerable<NewsResult> newsResults = newsQuery.Execute();
                foreach (NewsResult result in newsResults)
                {
                    SearchResultItem searchResultItem = new SearchResultItem();
                    searchResultItem.Provider = ProviderEnum.Bing;
                    searchResultItem.Title = result.Title;
                    searchResultItem.URL = result.Url;
                    searchResultItem.Source = result.Source;
                    searchResultItem.PublishedDate = (DateTime) result.Date;
                    string abstractText = result.Description;
                    abstractText = TextCleaner.StripTag(abstractText, "<", ">"); // remove any html tags
                    abstractText = TextCleaner.RemoveHtml(abstractText);
                    searchResultItem.Abstract = abstractText;
                    resultList.Add(searchResultItem);

                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("BingSearchService", "SearchNews", searchContext.ToString(), exception);
                if (exception.ToString().Contains("Insufficient balance"))
                {
                    hasInsufficientBalance = true;
                }
            }

            return resultList;
        }

        private SearchResultList SearchWeb(SearchContext searchContext)
        {
            SearchResultList resultList = new SearchResultList();

            try
            {
                string query = searchContext.Query;
                //int page = searchContext.Page;
                //int count = searchContext.Count;
                //int start = (page - 1) * count;
                string accountKey = ConfigService.GetConfig(ConfigKeys.BING_API_KEY, "");

                BingSearchContainer bingContainer = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/SearchWeb/"));
                bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);

                DataServiceQuery<WebResult> webQuery = bingContainer.Web(query, "en-us", "strict", null, null, null);
                IEnumerable<WebResult> webResults = webQuery.Execute();
                foreach (WebResult result in webResults)
                {
                    SearchResultItem searchResultItem = new SearchResultItem();
                    searchResultItem.Provider = ProviderEnum.Bing;
                    searchResultItem.Title = result.Title;
                    searchResultItem.URL = result.Url;
                    string abstractText = result.Description;
                    abstractText = TextCleaner.StripTag(abstractText, "<", ">"); // remove any html tags
                    abstractText = TextCleaner.RemoveHtml(abstractText);
                    searchResultItem.Abstract = abstractText;
                    resultList.Add(searchResultItem);
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("BingSearchService", "SearchWeb", searchContext.ToString(), exception);
                if (exception.ToString().Contains("Insufficient balance"))
                {
                    hasInsufficientBalance = true;
                }
            }

            return resultList;
        }

        private SearchResultList SearchImages(SearchContext searchContext)
        {
            SearchResultList resultList = new SearchResultList();

            try
            {
                string query = searchContext.Query;
                //int page = searchContext.Page;
                //int count = searchContext.Count;
                //int start = (page - 1) * count;
                string accountKey = ConfigService.GetConfig(ConfigKeys.BING_API_KEY, "");

                BingSearchContainer bingContainer = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/Search/Images/"));
                bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);

                DataServiceQuery<ImageResult> webQuery = bingContainer.Image(query, "en-us", "strict", null, null, null);
                IEnumerable<ImageResult> imageResults = webQuery.Execute();
                foreach (ImageResult result in imageResults)
                {
                    SearchResultItem searchResultItem = new SearchResultItem();
                    searchResultItem.Provider = ProviderEnum.Bing;
                    searchResultItem.Title = result.Title;
                    searchResultItem.URL = result.MediaUrl;
                    searchResultItem.ImageURL = result.MediaUrl;
                    searchResultItem.Abstract = result.Title;
                    resultList.Add(searchResultItem);
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("BingSearchService", "SearchImages", searchContext.ToString(), exception);
                if (exception.ToString().Contains("Insufficient balance"))
                {
                    hasInsufficientBalance = true;
                }
            }

            return resultList;
        }
    } 
}




