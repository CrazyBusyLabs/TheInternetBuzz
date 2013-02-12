using System;
using System.Web.Hosting;

using TheInternetBuzz.Util;
using TheInternetBuzz.Data.Suggestions;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Services.Trends
{
    static public class TrendsSerializer
    {
        private static Object guard = new Object();

        static public void Serialize(TrendsList trendsList)
        {

            string filepath = HostingEnvironment.MapPath("~/data/Trends.xml");
            Type type = typeof(TrendsList);
            Type[] extraTypes = new Type[] { typeof(TrendItem), typeof(SuggestionItem) };

            lock (guard)
            {
                XMLSerializer.Serialize(trendsList, type, extraTypes, filepath);
            }
        }

        static public TrendsList Deserialize()
        {
            string filepath = HostingEnvironment.MapPath("~/data/Trends.xml");

            TrendsList trendsList = null;

            Type type = typeof(TrendsList);
            Type[] extraTypes = new Type[] { typeof(TrendItem), typeof(SuggestionItem) };

            lock (guard)
            {
                trendsList = (TrendsList)XMLSerializer.Deserialize(filepath, type, extraTypes);
            }

            return trendsList;
        }
    }
}