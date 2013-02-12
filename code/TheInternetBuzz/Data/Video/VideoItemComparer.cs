using System;
using System.Collections;

namespace TheInternetBuzz.Data.Video
{
    public class VideoItemComparer : IComparer
    {
        public VideoItemComparerEnum SortBy
        {
            get;
            set;
        }

        public VideoItemComparer()
        {
            SortBy = VideoItemComparerEnum.Title;
        }

        public VideoItemComparer(VideoItemComparerEnum sortBy)
        {
            SortBy = sortBy;
        }


        public int Compare(object x, object y)
        {
            VideoItem item1;
            VideoItem item2;

            if (x is VideoItem)
                item1 = x as VideoItem;
            else
                throw new ArgumentException("Object is not of type VideoItem.");

            if (y is VideoItem)
                item2 = y as VideoItem;
            else
                throw new ArgumentException("Object is not of type VideoItem.");

            return item1.CompareTo(item2, SortBy);
        }

    }
}
