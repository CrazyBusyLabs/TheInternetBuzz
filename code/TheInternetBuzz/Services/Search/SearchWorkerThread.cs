using System;
using System.Web;
using System.Threading;
using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Data;
using TheInternetBuzz.Providers.Bing;
using TheInternetBuzz.Providers.Flickr;
using TheInternetBuzz.Providers.Google;
using TheInternetBuzz.Providers.Twitter;
using TheInternetBuzz.Providers.Yahoo;
using TheInternetBuzz.Data.Search;

namespace TheInternetBuzz.Services.Search
{
    public class SearchWorkerThread
    {
        private ManualResetEvent DoneEvent = null;
        private SearchContext SearchContext;

        public SearchWorkerThread(SearchContext searchContext, ManualResetEvent doneEvent)
        {
            this.SearchContext = searchContext;
            this.DoneEvent = doneEvent;
        }

        public void ThreadPoolCallback(Object threadContext)
        {
            IProviderSearchService searchService;
            switch (SearchContext.Provider)
            {
                case ProviderEnum.Google:
                    searchService = new GoogleSearchService();
                    break;

                case ProviderEnum.Bing:
                    searchService = new BingSearchService();
                    break;

                case ProviderEnum.Yahoo:
                    searchService = new YahooSearchService();
                    break;

                case ProviderEnum.Twitter:
                    searchService = new TwitterSearchService();
                    break;

                case ProviderEnum.Flickr:
                    searchService = new FlickrSearchService();
                    break;

                default:
                    searchService = new BingSearchService();
                    break;

            }
            SearchResultList = searchService.Search(SearchContext);
            DoneEvent.Set();
        }

        public SearchResultList SearchResultList
        {
            get;
            set;
        }
    }
}
