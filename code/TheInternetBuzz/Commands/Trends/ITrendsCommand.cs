using System;

using TheInternetBuzz.Services.Trends;
using TheInternetBuzz.Data.Trends;

namespace TheInternetBuzz.Commands.Trends
{
    public interface ITrendsCommand
    {
        TrendsList GetTrends();
    }
}