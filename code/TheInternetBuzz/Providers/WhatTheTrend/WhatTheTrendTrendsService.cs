using System;
using System.Xml;

using TheInternetBuzz.Connectors.JSON;
using TheInternetBuzz.Data.Explanation;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Util;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheInternetBuzz.Providers.WhatTheTrend
{
    public class WhatTheTrendTrendsService : ITrendsService
    {
        private WhatTheTrendTypeEnum trendsType;

        public WhatTheTrendTrendsService(WhatTheTrendTypeEnum trendsType)
        {
            this.trendsType = trendsType;
        }

        private WhatTheTrendTrendsService()
        {
        }

        public TrendsList GetTrends()
        {
            string urlTemplate;
            switch (trendsType)
            {
                case WhatTheTrendTypeEnum.Current:
                    urlTemplate = "http://api.whatthetrend.com/api/v2/trends.json?api_key={0}";
                    break;

                case WhatTheTrendTypeEnum.MostEdited:
                    urlTemplate = "http://api.whatthetrend.com/api/v2/trends/active.json?api_key={0}";
                    break;

                default:
                    urlTemplate = "http://api.whatthetrend.com/api/v2/trends.json?api_key={0}";
                    break;
            }

            TrendsList trendsList = new TrendsList();

            try
            {
                string api = ConfigService.GetConfig(ConfigKeys.WHATTHETREND_API_KEY, "");
                string url = string.Format(urlTemplate, api);

                JSONConnector JSONConnector = new JSONConnector();
                JObject currentTrendsRootJSONObject = JSONConnector.GetJSONObject(url);
                new WhatTheThrendTrendsParser().Parse(currentTrendsRootJSONObject, trendsList);

            }
            catch (Exception exception)
            {
                ErrorService.Log("WhatTheTrendTrendsService", "GetTrends", urlTemplate, exception);
            }

            return trendsList;
        }
    }
}