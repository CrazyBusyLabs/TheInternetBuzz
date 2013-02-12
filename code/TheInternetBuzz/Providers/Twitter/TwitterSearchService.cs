using System;
using System.Web;

using TheInternetBuzz.Connectors.JSON;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Util;

using Newtonsoft.Json.Linq;

namespace TheInternetBuzz.Providers.Twitter
{
    public class TwitterSearchService : IProviderSearchService
    {

        public TwitterSearchService()
        {
        }

        public SearchResultList Search(SearchContext searchContext)
        {
            SearchResultList resultList = new SearchResultList();

            try
            {
                string urlTemplate;
                switch (searchContext.SearchType)
                {
                    case SearchTypeEnum.Tweet:
                        urlTemplate = "http://search.twitter.com/search.json?q={0}&rpp={1}&page={2}&result_type=mixed&include_entities=false&with_twitter_user_id=false";
                        break;

                    default:
                        return resultList;
                }


                string query = searchContext.Query;
                int countPerPage = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SEARCH_COUNT_PER_PAGE_PER_PROVIDER, 8);
                int page = searchContext.Page;

                string url = string.Format(urlTemplate, HttpUtility.UrlEncode(query), countPerPage, page);

                JSONConnector JSONConnector = new JSONConnector();
                JObject searchResultsJSONObject = JSONConnector.GetJSONObject(url);

                new TwitterSearchParser().Parse(searchResultsJSONObject, resultList);
            }
            catch (Exception exception)
            {
                ErrorService.Log("TwitterSearchService", "Search", searchContext.ToString(), exception);
            }

            return resultList;
        }
    } 
}
