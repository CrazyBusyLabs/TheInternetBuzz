using System;

using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Connectors.Atom;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Providers.Google
{
    public class GoogleTrendsService : ITrendsService
    {
        public GoogleTrendsService()
        {
        }

        public TrendsList GetTrends()
        {
            TrendsList trendsList = new TrendsList();
            try
            {
                AtomConnector atomConnector = new AtomConnector();

                EntryData entryData = atomConnector.GetEntryData("http://www.google.com/trends/hottrends/atom/hourly");
                new GoogleTrendsParser().Parse(entryData, trendsList);

            }
            catch (Exception exception)
            {
                ErrorService.Log("GoogleTrendsService", "GetTrends", null, exception);
            }
    
            return trendsList;
        }

    }
}
