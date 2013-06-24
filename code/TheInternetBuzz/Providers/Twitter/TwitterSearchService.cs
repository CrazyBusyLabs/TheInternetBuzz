using System;
using System.Web;

using TheInternetBuzz.Connectors.JSON;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Providers.Twitter.Data;
using TheInternetBuzz.Services.Logging;
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
                        urlTemplate = "https://api.twitter.com/1.1/search/tweets.json?q={0}&count={1}&result_type=mixed&include_entities=false";
                        break;

                    default:
                        return resultList;
                }

                TwitterCredentials accessToken = new TwitterAuthService().Authenticate();
                string headerName = "Authorization";
                string headerValue = accessToken.User + " " + accessToken.Token;


                string query = searchContext.Query;
                int countPerPage = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SEARCH_COUNT_PER_PAGE_PER_PROVIDER, 8);
                int page = searchContext.Page;

                string url = string.Format(urlTemplate, HttpUtility.UrlEncode(query), countPerPage);

                JSONConnector JSONConnector = new JSONConnector();
                JObject searchResultsJSONObject = JSONConnector.GetJSONObjectWithHeader(url, headerName, headerValue);

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
