using System;

using TheInternetBuzz.Connectors.RSS;
using TheInternetBuzz.Connectors.HTML;
using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Providers.Alexa
{
    public class AlexaTrendsService : ITrendsService
    {
        public AlexaTrendsService()
        {
        }

        public TrendsList GetTrends()
        {
            TrendsList buzzList = new TrendsList();
            try
            {
                string html = new HTMLConnector().GetHTMLDocument("http://www.alexa.com/whatshot");

                AlexaTrendsParser parser = new AlexaTrendsParser();
                parser.Parse(html, buzzList);

            }
            catch (Exception exception)
            {
                ErrorService.Log("AlexaTrendsService", "GetTrends", null, exception);
            }

            return buzzList;
        }

    }
}
