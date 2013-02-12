using System;
using TheInternetBuzz.Services.Topics;
using TheInternetBuzz.Util;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Topics;

namespace TheInternetBuzz.Data.Trends
{
    public class TrendItem : IComparable
    {
        public string ID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }


        public string ImageURL
        {
            get;
            set;
        }

        public string TileImageURL
        {
            get;
            set;
        }

        public ProviderEnum Provider
        {
            get;
            set;
        }

        public CategoryEnum Category
        {
            get;
            set;
        }

        public short Weight
        {
            get;
            set;
        }

        protected TrendItem()
        {
        }

        public TrendItem(string id, string title, ProviderEnum provider)
        {
            ID = TextCleaner.CleanID(id);
            Title = TextCleaner.CleanTitle(title);
            Provider = provider;
            Category = CategoryEnum.Unknown;
        }

        public int CompareTo(object obj)
        {
            TrendItem t = (TrendItem)obj;
            return this.ID.CompareTo(t.ID);
        }

        public int CompareTo(TrendItem trend2, TrendItemComparerEnum sortBy)
        {
            switch (sortBy)
            {
                case TrendItemComparerEnum.ID:
                    return ID.CompareTo(trend2.ID);
                case TrendItemComparerEnum.Title:
                    return Title.CompareTo(trend2.Title);
                case TrendItemComparerEnum.Weight:
                    return Weight.CompareTo(trend2.Weight);
                default:
                    return ID.CompareTo(trend2.ID);
            }
        }
    }
}
