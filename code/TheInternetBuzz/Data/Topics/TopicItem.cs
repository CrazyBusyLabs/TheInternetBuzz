using System;
using TheInternetBuzz.Data;
using TheInternetBuzz.Data.Suggestions;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Data.Topics
{
    public class TopicItem : IComparable
    {
        protected TopicItem()
        {
        }

        public TopicItem(string id, string title)
        {
            ID = TextCleaner.CleanID(id);
            Title = TextCleaner.CleanTitle(title);
        }


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

        public string Query
        {
            get;
            set;
        }

        public string WikipediaSummary
        {
            get;
            set;
        }

        public string FreebaseSummary
        {
            get;
            set;
        }

        public string FreebaseThumbnailURL
        {
            get;
            set;
        }

        public string FreebaseURL
        {
            get;
            set;
        }

        public string WikipediaURL
        {
            get;
            set;
        }

        public string TwitterURL
        {
            get;
            set;
        }

        public string MySpaceURL
        {
            get;
            set;
        }

        public string FacebookURL
        {
            get;
            set;
        }

        public string WebsiteURL
        {
            get;
            set;
        }

        public int CompareTo(object obj)
        {
            TopicItem t = (TopicItem)obj;
            return this.ID.CompareTo(t.ID);
        }

        public override string ToString()
        {
            return Title;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            TopicItem item = (TopicItem)obj;
            return (ID == item.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}