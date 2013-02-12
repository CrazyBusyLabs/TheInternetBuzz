using System;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Trends
{
    public class TrendsList : List 
    {
        public void AddTrends(TrendsList trendsList)
        {
            if (trendsList != null)
            {
                foreach (TrendItem newTrend in trendsList)
                {
                    AddTrend(newTrend);
                }
            }
        }

        public void AddTrend(TrendItem newTrend)
        {
            TrendItem trend = GetTrend(newTrend.ID);
            if (trend == null)
            {
                Add(newTrend);
            }
            else
            {
                trend.Provider = newTrend.Provider;
                if (newTrend.Weight > trend.Weight)
                {
                    trend.Weight = newTrend.Weight;
                }
                if (trend.Weight < 5)
                {
                    trend.Weight = (short)(trend.Weight + 1);
                }
            }
        }

        public TrendItem GetTrend(string ID)
        {
            TrendItem foundTrendItem = null;

            foreach (TrendItem trendItem in this)
            {
                if (trendItem.ID.Equals(ID, StringComparison.CurrentCultureIgnoreCase))
                {
                    foundTrendItem = trendItem;
                    break;
                }
            }

            return foundTrendItem;
        }

        public bool IsTrend(string title)
        {
            bool hasFound = false;

            foreach (TrendItem trendItem in this)
            {
                if (trendItem.Title.Equals(title, StringComparison.CurrentCultureIgnoreCase))
                {
                    hasFound = true;
                    break;
                }
            }

            return hasFound;
        }

        public bool HasImage()
        {
            bool hasImages = false;

            foreach (TrendItem trendItem in this)
            {
                if (trendItem.TileImageURL != null && trendItem.TileImageURL.Length > 0)
                {
                    hasImages = true;
                    break;
                }
            }

            return hasImages;
        }
    }
}
