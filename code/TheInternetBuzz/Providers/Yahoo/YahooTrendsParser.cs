using System;
using System.Collections;
using TheInternetBuzz.Web;
using TheInternetBuzz.Connectors.RSS;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Services.Logging;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Providers.Yahoo
{

    public class YahooTrendsParser {

        public void Parse(IEnumerable channel, TrendsList trendsList) {
            int index = 0;

            foreach (Item item in channel) {
                index++;

                int pos = item.Title.IndexOf(". ");
                if (pos > 0) {
                    string trendName = item.Title.Substring(pos + 2);
                    TrendItem trendItem = new TrendItem(trendName, trendName, ProviderEnum.Yahoo); 

                    trendItem.Weight = CalculateWeight(index);

                    trendsList.AddTrend(trendItem);
                }
            }
        }

        public void Parse(string html, TrendsList trendsList)
        {
            const string trendsMarker = "<td class=\"subject\">";
            const string trendsEndMarker = "</a>";
            const string trendStartMarker = "\">";

            int index = 0;
            int endPos = 0;
            int startPos = 0;

            int startMarkerPos = html.IndexOf(trendsMarker);
            while (startMarkerPos >= 0) {
                index++;

                endPos = html.IndexOf(trendsEndMarker, startMarkerPos);
                startPos = html.IndexOf(trendStartMarker, startMarkerPos + trendsMarker.Length);

                string trendName = html.Substring(startPos + trendStartMarker.Length, endPos - startPos - trendStartMarker.Length);

                if (trendName.Length > 0 && trendName.IndexOf(">") < 0 && trendName.IndexOf("<") < 0)
                {
                    TrendItem trendItem = new TrendItem(trendName, trendName, ProviderEnum.Yahoo);

                    trendItem.Weight = CalculateWeight(index / 2);
                    trendsList.AddTrend(trendItem);
                }
                else
                {
                    LogService.Warn(typeof(YahooTrendsParser), "Error parsing trendname " + trendName);
                }

                startMarkerPos = html.IndexOf(trendsMarker, endPos);
            }
        }

        private short CalculateWeight(int index)
        {
            short weight = 0;

            if (index <= 2)
            {
                weight = 5;

            }
            else if (index <= 5)
            {
                weight = 4;

            }
            else if (index <= 8)
            {
                weight = 3;

            }
            else if (index <= 11)
            {
                weight = 2;

            }
            else
            {
                weight = 1;
            }

            return weight;
        }
    }
}