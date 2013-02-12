using System;
using System.Threading;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Data.Search;

using TheInternetBuzz.Services.Audit;

namespace TheInternetBuzz.Services.Search
{
    public class SearchService
    {
        public SearchResultList Search(SearchContext searchContext)
        {
            AuditServiceItem auditServiceItem = AuditService.Register("SearchService", "Search", searchContext.ToString());
            AuditService.Start(auditServiceItem);

            SearchTypeEnum searchType = searchContext.SearchType;
            string query = searchContext.Query;
            int page = searchContext.Page;
            int count = searchContext.Count;

            SearchResultList resultList = SearchCacheHelper.ReadSearchResultList(searchType, query, page);

            if (resultList == null)
            {
                resultList = new SearchResultList();

                try
                {
                    if (searchType == SearchTypeEnum.Web || searchType == SearchTypeEnum.News || searchType == SearchTypeEnum.Tweet)
                    {
                        ManualResetEvent[] doneEvents = new ManualResetEvent[1];

                        //doneEvents[0] = new ManualResetEvent(false);
                        //SearchContext bingSearchContext = new SearchContext(ProviderEnum.Bing, searchType, query, page, count);
                        //SearchWorkerThread bingWorkerThread = new SearchWorkerThread(bingSearchContext, doneEvents[0]);
                        //ThreadPool.QueueUserWorkItem(bingWorkerThread.ThreadPoolCallback);

                        doneEvents[0] = new ManualResetEvent(false);
                        SearchContext twitterSearchContext = new SearchContext(ProviderEnum.Twitter, searchType, query, page, count);
                        SearchWorkerThread twitterWorkerThread = new SearchWorkerThread(twitterSearchContext, doneEvents[0]);
                        ThreadPool.QueueUserWorkItem(twitterWorkerThread.ThreadPoolCallback);

                        //doneEvents[1] = new ManualResetEvent(false);
                        //SearchContext googleSearchContext = new SearchContext(ProviderEnum.Google, searchType, query, page, count);
                        //SearchWorkerThread googleWorkerThread = new SearchWorkerThread(googleSearchContext, doneEvents[1]);
                        //ThreadPool.QueueUserWorkItem(googleWorkerThread.ThreadPoolCallback);

                        //doneEvents[2] = new ManualResetEvent(false);
                        //SearchContext yahooSearchContext = new SearchContext(ProviderEnum.Yahoo, searchType, query, page, count);
                        //SearchWorkerThread yahooWorkerThread = new SearchWorkerThread(yahooSearchContext, doneEvents[2]);
                        //ThreadPool.QueueUserWorkItem(yahooWorkerThread.ThreadPoolCallback);

                        WaitHandle.WaitAll(doneEvents);

                        //if (googleWorkerThread.SearchResultList != null) resultList.AddResults(googleWorkerThread.SearchResultList);
                        //if (bingWorkerThread.SearchResultList != null) resultList.AddResults(bingWorkerThread.SearchResultList);
                        //if (yahooWorkerThread.SearchResultList != null) resultList.AddResults(yahooWorkerThread.SearchResultList);
                        if (twitterWorkerThread.SearchResultList != null) resultList.AddResults(twitterWorkerThread.SearchResultList);

                        if (searchType == SearchTypeEnum.News)
                        {
                            FilterNewsByTitle(resultList, query);
                        }
                    }
                    else if (searchType == SearchTypeEnum.Image)
                    {
                        ManualResetEvent[] doneEvents = new ManualResetEvent[1];

                        doneEvents[0] = new ManualResetEvent(false);
                        SearchContext flickrSearchContext = new SearchContext(ProviderEnum.Flickr, searchType, query, page, count);
                        SearchWorkerThread flickrWorkerThread = new SearchWorkerThread(flickrSearchContext, doneEvents[0]);
                        ThreadPool.QueueUserWorkItem(flickrWorkerThread.ThreadPoolCallback);

                        //doneEvents[0] = new ManualResetEvent(false);
                        //SearchContext bingSearchContext = new SearchContext(ProviderEnum.Bing, searchType, query, page, count);
                        //SearchWorkerThread bingWorkerThread = new SearchWorkerThread(bingSearchContext, doneEvents[0]);
                        //ThreadPool.QueueUserWorkItem(bingWorkerThread.ThreadPoolCallback);

                        //doneEvents[0] = new ManualResetEvent(false);
                        //SearchContext twitterSearchContext = new SearchContext(ProviderEnum.Twitter, searchType, query, page, count);
                        //SearchWorkerThread twitterWorkerThread = new SearchWorkerThread(twitterSearchContext, doneEvents[0]);
                        //ThreadPool.QueueUserWorkItem(twitterWorkerThread.ThreadPoolCallback);

                        //doneEvents[1] = new ManualResetEvent(false);
                        //SearchContext googleSearchContext = new SearchContext(ProviderEnum.Google, searchType, query, page, count);
                        //SearchWorkerThread googleWorkerThread = new SearchWorkerThread(googleSearchContext, doneEvents[1]);
                        //ThreadPool.QueueUserWorkItem(googleWorkerThread.ThreadPoolCallback);

                        //doneEvents[2] = new ManualResetEvent(false);
                        //SearchContext yahooSearchContext = new SearchContext(ProviderEnum.Yahoo, searchType, query, page, count);
                        //SearchWorkerThread yahooWorkerThread = new SearchWorkerThread(yahooSearchContext, doneEvents[2]);
                        //ThreadPool.QueueUserWorkItem(yahooWorkerThread.ThreadPoolCallback);

                        WaitHandle.WaitAll(doneEvents);

                        //if (googleWorkerThread.SearchResultList != null) resultList.AddResults(googleWorkerThread.SearchResultList);
                        if (flickrWorkerThread.SearchResultList != null) resultList.AddResults(flickrWorkerThread.SearchResultList);
                        //if (yahooWorkerThread.SearchResultList != null) resultList.AddResults(yahooWorkerThread.SearchResultList);
                        //if (twitterWorkerThread.SearchResultList != null) resultList.AddResults(twitterWorkerThread.SearchResultList);

                    }
                    SearchCacheHelper.CacheSearchResultList(searchType, query, page, resultList);
                }
                catch (Exception exception)
                {
                    ErrorService.Log("SearchService", "Search", searchContext.ToString(), exception);
                    return null;
                }
            }

            AuditService.End(auditServiceItem);

            return resultList;
        }

        private void FilterNewsByTitle(SearchResultList resultList, String query)
        {
            SearchResultList removalList = new SearchResultList();
            foreach (SearchResultItem resultItem in resultList)
            {
                String title = resultItem.Title;
                if (title == null || (!title.Contains(query)))
                {
                    removalList.Add(resultItem);
                }
            }
            foreach (SearchResultItem removalItem in removalList)
            {
                resultList.Remove(removalItem);
            }
        }
    }
}
