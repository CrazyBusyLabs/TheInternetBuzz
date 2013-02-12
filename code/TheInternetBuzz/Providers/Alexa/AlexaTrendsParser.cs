using System;
using System.Collections;
using TheInternetBuzz.Web;
using TheInternetBuzz.Connectors.RSS;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Services.Error;

using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Providers.Alexa
{

    public class AlexaTrendsParser {

        public void Parse(string html, TrendsList trendsList)
        {
            const string sectionStartTrendsMarker = "<h2>Hot Topics</h2>";
            const string sectionEndTrendsMarker = "</div>";
            const string trendsEndMarker = "'";
            const string trendStartMarker = "title='";

            int index = 0;

            int sectionStartMarkerPos = html.IndexOf(sectionStartTrendsMarker);
            if (sectionStartMarkerPos >= 0)
            {
                int sectionEndMarkerPos = html.IndexOf(sectionEndTrendsMarker, sectionStartMarkerPos + sectionStartTrendsMarker.Length);

                if (sectionEndMarkerPos >= sectionStartMarkerPos)
                {

                    int startPos = html.IndexOf(trendStartMarker, sectionStartMarkerPos + trendStartMarker.Length);
                    int endPos = html.IndexOf(trendsEndMarker, startPos + trendStartMarker.Length);

                    while (endPos > startPos && startPos >= 0 && endPos < sectionEndMarkerPos)
                    {
                        index++;

                        string trendName = html.Substring(startPos + trendStartMarker.Length, endPos - startPos - trendStartMarker.Length);

                        if (trendName.Length > 0 && trendName.IndexOf(">") < 0 && trendName.IndexOf("<") < 0)
                        {
                            TrendItem trendItem = new TrendItem(trendName, trendName, ProviderEnum.Alexa);

                            trendItem.Weight = CalculateWeight(index);
                            trendsList.AddTrend(trendItem);
                        }
                        else
                        {
                            LogService.Warn(typeof(AlexaTrendsParser), "Error parsing trendname " + trendName);
                        }

                        startPos = html.IndexOf(trendStartMarker, endPos + trendsEndMarker.Length);
                        endPos = html.IndexOf(trendsEndMarker, startPos + trendStartMarker.Length);
                    }
                }
                else
                {
                    ErrorService.Log("AlexaTrendsService", "GetTrends", "Parsing Html", new Exception("sectionEndMarkerPos is " + sectionEndMarkerPos));
                }
            }
            else
            {
                ErrorService.Log("AlexaTrendsService", "GetTrends", "Parsing Html", new Exception("sectionStartMarkerPos is " + sectionStartMarkerPos));
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