using System;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TheInternetBuzz.Connectors.RSS;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Util;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Providers.Twitter
{
    public class TwitterTrendsParser
    {
        public void Parse(JArray rootTrendsArray, TrendsList trendsList)
        {
            LogService.Debug(this.GetType(), "Entering Parse for TwitterTrendsParser");

            if (rootTrendsArray != null && rootTrendsArray.Count > 0)
            {
                JObject rootObject = (JObject) rootTrendsArray.First;
                if (rootObject != null)
                {
                    JArray trendsJArray = rootObject.Value<JArray>("trends");
                    if (trendsJArray != null)
                    {
                        foreach (JObject trendJObject in trendsJArray)
                        {
                            string name = trendJObject.Value<string>("name");

                            if (name != null && !name.Contains("#"))
                            {
                                TrendItem trendItem = new TrendItem(name, name, ProviderEnum.Twitter);
                                trendItem.Weight = 1;

                                trendsList.AddTrend(trendItem);
                            }
                        }
                    }
                }
            }
            LogService.Debug(this.GetType(), "Exiting Parse for TwitterTrendsParser");
        }
    }
}
