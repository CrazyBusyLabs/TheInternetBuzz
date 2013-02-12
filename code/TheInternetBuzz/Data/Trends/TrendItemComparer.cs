using System;
using System.Collections;

namespace TheInternetBuzz.Data.Trends
{
    public class TrendItemComparer : IComparer
    {
        public TrendItemComparerEnum SortBy
        {
            get;
            set;
        }

        public TrendItemComparer()
        {
            SortBy = TrendItemComparerEnum.Title;
        }

        public TrendItemComparer(TrendItemComparerEnum sortBy)
        {
            SortBy = sortBy;
        }


        public int Compare(object x, object y)
        {
            TrendItem trend1;
            TrendItem trend2;

            if (x is TrendItem)
                trend1 = x as TrendItem;
            else
                throw new ArgumentException("Object is not of type TrendItem.");

            if (y is TrendItem)
                trend2 = y as TrendItem;
            else
                throw new ArgumentException("Object is not of type TrendItem.");

            return trend1.CompareTo(trend2, SortBy);
        }

    }
}
