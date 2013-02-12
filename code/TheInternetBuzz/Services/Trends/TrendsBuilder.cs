using System;
using System.Threading;
using System.Net;

using TheInternetBuzz.Web;
using TheInternetBuzz.Services.Audit;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Services.Search;
using TheInternetBuzz.Services.Topics;
using TheInternetBuzz.Services.Video;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Audit;
using TheInternetBuzz.Data.Search;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data.Video;
using TheInternetBuzz.Providers.Alexa;
using TheInternetBuzz.Providers.Google;
using TheInternetBuzz.Providers.Twitter;
using TheInternetBuzz.Providers.Yahoo;
using TheInternetBuzz.Providers.WhatTheTrend;
using TheInternetBuzz.Providers.YouTube;
using TheInternetBuzz.Providers.ITunes;

using TheInternetBuzz.Connectors.HTTP;

namespace TheInternetBuzz.Services.Trends
{
    public static class TrendsBuilder
    {
        static private bool RELOAD_ON_TIMER = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_TRENDS_RELOAD_ON_TIMER, false);
        static private int REFRESH_TIME = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_TRENDS_RELOAD_INTERVAL, 30) * 60000;
        static private int TIMER_SHUTDOWN_WAIT = 1000;

        static private Timer timer = null;
        static private bool inProgress;

        static public void StartTimer()
        {
            if (RELOAD_ON_TIMER)
            {
                if (timer == null)
                {
                    LogService.Info(typeof(TrendsBuilder), "Starting Trends Builder timer");
                    timer = new Timer(BuildTrendsData, null, 0, REFRESH_TIME);
                }
            }
        }

        static public void EndTimer()
        {
            if (RELOAD_ON_TIMER)
            {
                if (timer != null)
                {
                    while (inProgress)
                    {
                        LogService.Warn(typeof(TrendsBuilder), "Waiting for timer to terminate before shutting down");
                        Thread.Sleep(TIMER_SHUTDOWN_WAIT);
                    }

                    LogService.Info(typeof(TrendsBuilder), "Stopping Trends Builder timer");
                    timer = null;
                }
            }
        }

        static public void BuildTrendsData(object data)
        {
            if (!inProgress)
            {
                AuditServiceItem AuditServiceItem = AuditService.Register("TrendsBuilder", "BuildTrendsData", "");
                AuditService.Start(AuditServiceItem);

                inProgress = true;

                LogService.Info(typeof(TrendsBuilder), "Building Trends Data");

                TrendsList trendsList = BuildTrends();
                VideoList videoList = BuildVideoTrends();

                PreLoadTrendsImage(trendsList);

                if (ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_TRENDS_PRELOAD_DATA, false))
                {
                    PreLoadTrends(trendsList);
                }
                if (trendsList != null && trendsList.Count() > 0) TrendsSerializer.Serialize(trendsList);
                if (videoList != null && videoList.Count() > 0) VideoListSerializer.Serialize(videoList);

                if (trendsList != null && trendsList.Count() > 0)
                {
                    TrendsCacheHelper.CacheTrends(trendsList);
                }

                LogService.Info(typeof(TrendsBuilder), "Building Trends Data is done");

                AuditService.End(AuditServiceItem);

                inProgress = false;
            }
        }

        static private TrendsList BuildTrends()
        {
            TrendsList trendsList = null;

            try
            {
                ManualResetEvent[] doneEvents = new ManualResetEvent[8];

                doneEvents[0] = new ManualResetEvent(false);
                TrendsWorkerThread googleWorkerThread = new TrendsWorkerThread(new GoogleTrendsService(), doneEvents[0]);
                ThreadPool.QueueUserWorkItem(googleWorkerThread.ThreadPoolCallback);

                doneEvents[1] = new ManualResetEvent(false);
                TrendsWorkerThread twitterWorkerThread = new TrendsWorkerThread(new TwitterTrendsService(), doneEvents[1]);
                ThreadPool.QueueUserWorkItem(twitterWorkerThread.ThreadPoolCallback);

                doneEvents[2] = new ManualResetEvent(false);
                TrendsWorkerThread yahooWorkerThread = new TrendsWorkerThread(new YahooTrendsService(), doneEvents[2]);
                ThreadPool.QueueUserWorkItem(yahooWorkerThread.ThreadPoolCallback);

                doneEvents[3] = new ManualResetEvent(false);
                TrendsWorkerThread whatTheTrendCurrentWorkerThread = new TrendsWorkerThread(new WhatTheTrendTrendsService(WhatTheTrendTypeEnum.Current), doneEvents[3]);
                ThreadPool.QueueUserWorkItem(whatTheTrendCurrentWorkerThread.ThreadPoolCallback);

                doneEvents[4] = new ManualResetEvent(false);
                TrendsWorkerThread whatTheTrendMostEditedWorkerThread = new TrendsWorkerThread(new WhatTheTrendTrendsService(WhatTheTrendTypeEnum.MostEdited), doneEvents[4]);
                ThreadPool.QueueUserWorkItem(whatTheTrendMostEditedWorkerThread.ThreadPoolCallback);

                doneEvents[5] = new ManualResetEvent(false);
                TrendsWorkerThread alexaWorkerThread = new TrendsWorkerThread(new AlexaTrendsService(), doneEvents[5]);
                ThreadPool.QueueUserWorkItem(alexaWorkerThread.ThreadPoolCallback);

                doneEvents[6] = new ManualResetEvent(false);
                TrendsWorkerThread iTunesSongsWorkerThread = new TrendsWorkerThread(new ITunesTrendsService(ITunesTypeEnum.Top10Songs), doneEvents[6]);
                ThreadPool.QueueUserWorkItem(iTunesSongsWorkerThread.ThreadPoolCallback);

                doneEvents[7] = new ManualResetEvent(false);
                TrendsWorkerThread iTunesMoviesWorkerThread = new TrendsWorkerThread(new ITunesTrendsService(ITunesTypeEnum.TopMovies), doneEvents[7]);
                ThreadPool.QueueUserWorkItem(iTunesMoviesWorkerThread.ThreadPoolCallback);

                WaitHandle.WaitAll(doneEvents);

                trendsList = new TrendsList();
                trendsList.AddTrends(googleWorkerThread.TrendsList);
                trendsList.AddTrends(yahooWorkerThread.TrendsList);
                trendsList.AddTrends(twitterWorkerThread.TrendsList);
                trendsList.AddTrends(whatTheTrendCurrentWorkerThread.TrendsList);
                trendsList.AddTrends(whatTheTrendMostEditedWorkerThread.TrendsList);
                trendsList.AddTrends(alexaWorkerThread.TrendsList);
                trendsList.AddTrends(iTunesSongsWorkerThread.TrendsList);
                trendsList.AddTrends(iTunesMoviesWorkerThread.TrendsList);

                if (trendsList != null && trendsList.Count() > 0)
                {
                    trendsList.Sort(new TrendItemComparer(TrendItemComparerEnum.Title));
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("TrendsBuilder", "BuildTrends", "", exception);
            }

            return trendsList;
        }

        static private VideoList BuildVideoTrends()
        {
            VideoList videoList = null;

            try
            {
                videoList = new YouTubeTrendsService().GetVideo();

                if (videoList != null && videoList.Count() > 0)
                {
                    videoList.Sort(new VideoItemComparer(VideoItemComparerEnum.Title));
                    TrendsCacheHelper.CacheVideoTrends(videoList);
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("TrendsBuilder", "BuildVideoTrends", null, exception);
            }

            return videoList;
        }

        static private void PreLoadTrendsImage(TrendsList trendsList)
        {
            if (trendsList != null)
            {
                SearchService searchService = new SearchService();

                foreach (TrendItem trendItem in trendsList)
                {
                    if (trendItem.ImageURL == null)
                    {
                        String query = trendItem.Title;
                        SearchContext searchContext = new SearchContext(query, 1, 1);
                        searchContext.SearchType = SearchTypeEnum.Image;
                        SearchResultList searchResultList = searchService.Search(searchContext);
                        if (searchResultList.Count() > 0)
                        {
                            SearchResultItem item = (SearchResultItem)searchResultList.Item(0);
                            trendItem.ImageURL = item.ImageURL;
                        }
                    }

                    if (trendItem.TileImageURL == null && trendItem.ImageURL != null)
                    {
                        int imageSize;
                        switch (trendItem.Weight)
                        {
                            case 1: imageSize = 150;
                                break;
                            case 2: imageSize = 150;
                                break;
                            case 3: imageSize = 150;
                                break;
                            case 4: imageSize = 150;
                                break;
                            case 5: imageSize = 150;
                                break;
                            default: imageSize = 150;
                                break;
                        }

                        string url = "http://src.sencha.io/" + imageSize + "/" + trendItem.ImageURL;

                        HttpStatusCode status = new HTTPConnector().GetStatus(url, "image/jpeg,image/gif.image/png");
                        if (status == HttpStatusCode.OK)
                        {
                            trendItem.TileImageURL = url;
                        }
                    }
                }
            }
        }

        static private void PreLoadTrends(TrendsList trendsList)
        {
            if (trendsList != null)
            {
                TopicService topicService = new TopicService();
                SearchService searchService = new SearchService();
                VideoService videoService = new VideoService();

                foreach (TrendItem trendItem in trendsList)
                {
                    String query = trendItem.Title;

                    topicService.GetTopic(query);

                    int pageMax = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SEARCH_COUNT_PER_PAGE_PER_PROVIDER, 8);
                    SearchContext searchContext = new SearchContext(query, 1, pageMax);
                    searchContext.SearchType = SearchTypeEnum.Web;
                    searchService.Search(searchContext);
                    searchContext.SearchType = SearchTypeEnum.News;
                    searchService.Search(searchContext);

                    videoService.Search(query);
                }
            }
        }
    }
}