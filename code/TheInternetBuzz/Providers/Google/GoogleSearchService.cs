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

namespace TheInternetBuzz.Providers.Google
{
    public class GoogleSearchService : IProviderSearchService
    {
        const string API_VERSION = "1.0";

        public GoogleSearchService()
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
                    case SearchTypeEnum.News:
                        urlTemplate = "http://ajax.googleapis.com/ajax/services/search/news?v={0}&rsz=large&key={1}&q={2}&start={3}";
                        break;

                    case SearchTypeEnum.Web:
                        urlTemplate = "http://ajax.googleapis.com/ajax/services/search/web?v={0}&rsz=large&key={1}&q={2}&start={3}";
                        break;

                    default:
                        return resultList;
                }


                string api = ConfigService.GetConfig(ConfigKeys.GOOGLE_API_KEY, "");
                string query = searchContext.Query;
                int countPerPage = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SEARCH_COUNT_PER_PAGE_PER_PROVIDER, 8);
                int page = (searchContext.Page - 1) * countPerPage;

                string url = string.Format(urlTemplate, API_VERSION, api, HttpUtility.UrlEncode(query), page);

                JSONConnector JSONConnector = new JSONConnector();
                JObject searchResultsJSONObject = JSONConnector.GetJSONObject(url);

                new GoogleSearchParser().Parse(searchResultsJSONObject, resultList);
            }
            catch (Exception exception)
            {
                ErrorService.Log("GoogleSearchService", "Search", searchContext.ToString(), exception);
            }

            return resultList;
        }
    } 
}
