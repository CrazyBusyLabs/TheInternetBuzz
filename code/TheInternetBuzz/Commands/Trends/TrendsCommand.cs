using System;

using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Commands.Trends
{
    public class TrendsCommand : ITrendsCommand
    {
        public TrendsList GetTrends()
        {
            return new TrendsService().GetTrends();
        }
    }
}