using System;

using TheInternetBuzz.Connectors.RSS;
using TheInternetBuzz.Connectors.HTML;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Providers.Yahoo
{
    public class YahooTrendsService : ITrendsService
    {
        public YahooTrendsService()
        {
        }

        public TrendsList GetTrends()
        {
            TrendsList buzzList = new TrendsList();
            try
            {
                //RSSConnector rss = new RSSConnector();

                //Channel buzzLeaderChannel = rss.GetChannel("http://buzz.yahoo.com/feeds/buzzoverl.xml");
                //Channel buzzMoversChannel = rss.GetChannel("http://buzz.yahoo.com/feeds/buzzoverm.xml");

                //string html = new HTMLConnector().GetHTMLDocument("http://buzzlog.yahoo.com/overall/");

                //YahooTrendsParser parser = new YahooTrendsParser();
                //parser.Parse(buzzLeaderChannel, buzzList);
                //parser.Parse(buzzMoversChannel, buzzList);
                //parser.Parse(html, buzzList);

            }
            catch (Exception exception)
            {
                ErrorService.Log("YahooTrendsService", "GetTrends", null, exception);
            }

            return buzzList;
        }

    }
}
