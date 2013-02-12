using System;
using System.Globalization;
using System.Web;
using System.Xml;

using TheInternetBuzz.Connectors.XML;
using TheInternetBuzz.Services.Config;
using TheInternetBuzz.Services.Error;

using TheInternetBuzz.Util;
using TheInternetBuzz.Data.Explanation;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Providers.WhatTheTrend
{
    public class WhatTheTrendExplanationService
    {
        public WhatTheTrendExplanationService()
        {
        }

        public ExplanationItem GetExplanation(string query)
        {
            String displayQuery = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(query);

            ExplanationItem explanationLItem = null;
            return explanationLItem;
        }

    } 
}
