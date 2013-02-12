using System;

using TheInternetBuzz.Data;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Data.Video
{
    public class VideoItem : IComparable
    {
        protected VideoItem()
        {
        }

        public VideoItem(string id, string title, ProviderEnum provider)
        {
            ID = TextCleaner.CleanID(id);
            Title = TextCleaner.CleanTitle(title);
            Provider = provider;
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

        public ProviderEnum Provider
        {
            get;
            set;
        }

        public string ThumbnailImageURL
        {
            get;
            set;
        }

        public string ThumbnailImageWidth
        {
            get;
            set;
        }

        public string ThumbnailImageHeight
        {
            get;
            set;
        }

        public int CompareTo(object obj)
        {
            VideoItem t = (VideoItem)obj;
            return this.ID.CompareTo(t.ID);
        }

        public int CompareTo(VideoItem item2, VideoItemComparerEnum sortBy)
        {
            switch (sortBy)
            {
                case VideoItemComparerEnum.ID:
                    return ID.CompareTo(item2.ID);
                case VideoItemComparerEnum.Title:
                    return Title.CompareTo(item2.Title);
                default:
                    return ID.CompareTo(item2.ID);
            }
        }

        public override string ToString()
        {
            return Title;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            VideoItem item = (VideoItem)obj;
            return (ID == item.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
