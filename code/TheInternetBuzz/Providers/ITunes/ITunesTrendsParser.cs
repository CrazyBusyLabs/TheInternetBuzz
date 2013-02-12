using System;
using System.Collections;

using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data.Explanation;
using TheInternetBuzz.Connectors.Atom;
using TheInternetBuzz.Data;
using TheInternetBuzz.Services.Explanation;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Logging;
using TheInternetBuzz.Util;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheInternetBuzz.Providers.ITunes
{
    public class ITunesTrendsParser
    {
        public void Parse(EntryData data, ITunesTypeEnum iTunesType, TrendsList trendsList)
        {
            LogService.Debug(this.GetType(), "Entering Parse for ITunesTrendsParser");
            try
            {
                EntryList entryList = data.EntryList;
                int i = 0;

                foreach (Entry entry in entryList)
                {
                    string name;
                    CategoryEnum category;

                    switch (iTunesType)
                    {
                        case ITunesTypeEnum.Top10Songs:
                            name = entry.Get("im:artist");
                            category = CategoryEnum.Music; 
                            break;

                        default:
                            name = entry.Get("im:name");
                            category = CategoryEnum.Movie; 
                            break;
                    }
                    TrendItem trendItem = new TrendItem(name, name, ProviderEnum.ITunes);
                    trendItem.ImageURL = entry.Get("im:image");
                    trendItem.Weight = CalculateWeight(i);
                    trendItem.Category = category;
                    trendsList.Add(trendItem);

                    i++;
                }
            }
            catch (Exception exception)
            {
                ErrorService.Log("ITunesTrendsParser", "Parse", null, exception);
            }
            LogService.Debug(this.GetType(), "Exiting Parse for ITunesTrendsParser");
        }

        private short CalculateWeight(int index)
        {
            short weight = 0;

            if (index <= 1)
            {
                weight = 5;

            }
            else if (index <= 3)
            {
                weight = 4;

            }
            else if (index <= 6)
            {
                weight = 3;

            }
            else if (index <= 8)
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
