using System;
using System.Collections;

using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Connectors.Atom;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Providers.Google
{
    public class GoogleTrendsParser 
    {
        public void Parse(EntryData entryData, TrendsList trendsList)
        {
            foreach (Entry entry in entryData.EntryList)
            {
                string content = entry.Content;
                int endPos = content.IndexOf("</a>");

                while (endPos > 0)
                {
                    int startPos = content.Substring(0, endPos).LastIndexOf(">") + 1;
                    if (startPos > 0 && endPos > startPos)
                    {
                        string trendName = content.Substring(startPos, endPos - startPos);
                        TrendItem trendItem = new TrendItem(trendName, trendName, ProviderEnum.Google);

                        int classStartPos = content.Substring(0, endPos).LastIndexOf("span class=") + 12;
                        int classEndPos = content.Substring(0, endPos).IndexOf(">", classStartPos) - 1;

                        if (classStartPos > 0 && classEndPos > classStartPos)
                        {
                            string className = content.Substring(classStartPos, classEndPos - classStartPos);
                            if (className.Contains("Volcanic"))
                            {
                                trendItem.Weight = 5;
                            } 
                            else if (className.Contains("On_Fire"))
                            {
                                trendItem.Weight = 4;
                            }
                            else if (className.Contains("Spicy"))
                            {
                                trendItem.Weight = 3;
                            }
                            else if (className.Contains("Medium"))
                            {
                                trendItem.Weight = 2;
                            }
                            else
                            {
                                trendItem.Weight = 1;
                            }
                        }

                        trendsList.AddTrend(trendItem);

                        endPos = content.IndexOf("</a>", endPos + 1);
                    }
                    else
                    {
                        endPos = -1;
                    }
                }
            }
        }
    }
}
