using System;
using System.Web;

using TheInternetBuzz.Connectors.XML;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Util;

using System.Xml;
using System.Xml.XPath;

namespace TheInternetBuzz.Providers.Flickr
{
    public class FlickrSearchService : IProviderSearchService
    {

        public FlickrSearchService()
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
                    case SearchTypeEnum.Image:
                        urlTemplate = "http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&safe_search=1&per_page={1}&page={2}&format=rest&nojsoncallback=1&privacy_filter=1&content_type=1&sort=relevance&text={3}";
                        break;

                    default:
                        return resultList;
                }


                string query = searchContext.Query;
                int countPerPage = searchContext.Count;
                int page = searchContext.Page;
                string key = ConfigService.GetConfig(ConfigKeys.FLICKR_API_KEY, "");

                string url = string.Format(urlTemplate, key, countPerPage, page, HttpUtility.UrlEncode(query));

                Console.WriteLine(url);

                XMLConnector XMLConnector = new XMLConnector();
                XmlDocument xmlDocument = XMLConnector.GetXMLDocument(url);

                new FlickrSearchParser().Parse(xmlDocument, resultList);
            }
            catch (Exception exception)
            {
                ErrorService.Log("FlickrSearchService", "Search", searchContext.ToString(), exception);
            }

            return resultList;
        }
    } 
}
