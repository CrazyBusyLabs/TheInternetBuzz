using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Connectors.JSON;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Providers.Twitter.Data;

namespace TheInternetBuzz.Providers.Twitter
{
    public class TwitterTrendsService : ITrendsService
    {
        static private string CURRENT_TRENDS_URL = "https://api.twitter.com/1.1/trends/place.json?id=1";

        static private int QUEUE_SIZE = ConfigService.GetConfig(ConfigKeys.TWITTER_CURRENT_TRENDS_QUEUE_SIZE, 5);
        static private Queue<TrendsList> twitterTrendsQueue = new Queue<TrendsList>(QUEUE_SIZE);

        public TwitterTrendsService()
        {
        }

        public TrendsList GetTrends()
        {
            TrendsList trendsList = new TrendsList();

            TrendsList currentTrendsList = new TrendsList();
            try
            {
                TwitterCredentials accessToken = new TwitterAuthService().Authenticate();
                string headerName = "Authorization";
                string headerValue = accessToken.User + " " + accessToken.Token;

                JSONConnector JSONConnector = new JSONConnector();
                JArray currentTrendsArray = JSONConnector.GetJSONArrayWithHeader(CURRENT_TRENDS_URL, headerName, headerValue);

                new TwitterTrendsParser().Parse(currentTrendsArray, currentTrendsList);
            }
            catch (Exception exception)
            {
                ErrorService.Log("TwitterTrendsService", "GetTrends", null, exception);
            }

            if (twitterTrendsQueue.Count >= QUEUE_SIZE)
            {
                twitterTrendsQueue.Dequeue();
            }
            twitterTrendsQueue.Enqueue(currentTrendsList);
            foreach (TrendsList twitterTrendsList in twitterTrendsQueue)
            {
                trendsList.AddTrends(twitterTrendsList);
            }

            return trendsList;
        }
    }
}