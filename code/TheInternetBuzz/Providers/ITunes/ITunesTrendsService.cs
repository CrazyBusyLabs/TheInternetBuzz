using System;
using System.Xml;

using TheInternetBuzz.Connectors.Atom;
using TheInternetBuzz.Data.Trends;
using TheInternetBuzz.Data;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Util;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheInternetBuzz.Providers.ITunes
{
    public class ITunesTrendsService : ITrendsService
    {
        private ITunesTypeEnum iTunesType;

        public ITunesTrendsService(ITunesTypeEnum iTunesType)
        {
            this.iTunesType = iTunesType;
        }

        private ITunesTrendsService()
        {
        }

        public TrendsList GetTrends()
        {
            string url;
            switch (iTunesType)
            {
                case ITunesTypeEnum.Top10Songs:
                    url = "http://ax.itunes.apple.com/WebObjects/MZStoreServices.woa/ws/RSS/topsongs/limit=10/xml";
                    break;

                case ITunesTypeEnum.TopMovies:
                    url = "http://ax.itunes.apple.com/WebObjects/MZStoreServices.woa/ws/RSS/topMovies/xml";
                    break;

                default:
                    url = "http://ax.itunes.apple.com/WebObjects/MZStoreServices.woa/ws/RSS/topsongs/limit=10/xml";
                    break;
            }

            TrendsList trendsList = new TrendsList();

            try
            {
                AtomConnector AtomConnector = new AtomConnector();
                EntryData data = AtomConnector.GetEntryData(url);
                new ITunesTrendsParser().Parse(data, iTunesType, trendsList);
            }
            catch (Exception exception)
            {
                ErrorService.Log("ITunesTrendsService", "GetTrends", url, exception);
            }

            return trendsList;
        }
    }
}