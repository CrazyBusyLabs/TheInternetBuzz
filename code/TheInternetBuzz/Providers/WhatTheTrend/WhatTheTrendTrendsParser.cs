using System;
using System.Collections;

using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data.Explanation;
using TheInternetBuzz.Data;
using TheInternetBuzz.Services.Explanation;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Util;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheInternetBuzz.Providers.WhatTheTrend
{
    public class WhatTheThrendTrendsParser
    {
        public void Parse(JObject rootObject, TrendsList trendsList)
        {
            LogService.Debug(this.GetType(), "Entering Parse for WhatTheThrendTrendsParser");
            try
            {
                JArray trendsJArray = rootObject.Value<JArray>("trends");
                if (trendsJArray != null)
                {
                    foreach (JObject trendJObject in trendsJArray)
                    {
                        string name = trendJObject.Value<string>("name");
                        if (name != null && !name.Contains("#"))
                        {
                            JToken t;
                            trendJObject.TryGetValue("description", out t);
                            if (t.HasValues)
                            {
                                JObject descriptionJSONObject = trendJObject.Value<JObject>("description");
                                if (descriptionJSONObject != null)
                                {
                                    string text = descriptionJSONObject.Value<string>("text");
                                    if (text != null)
                                    {
                                        TrendItem trendItem = new TrendItem(name, name, ProviderEnum.WhatTheTrend);
                                        trendItem.Weight = 5;
                                        trendsList.Add(trendItem);

                                        ExplanationItem explanationLItem = new ExplanationItem(trendItem.Title, trendItem.Title, text);
                                        ExplanationCacheHelper.CacheExplanationItem(trendItem.Title, explanationLItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("WhatTheTrendTrendsService", "Parse", null, exception);
            }
            LogService.Debug(this.GetType(), "Exiting Parse for WhatTheThrendTrendsParser");
        }
    }
}
