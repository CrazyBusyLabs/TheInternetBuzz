using System;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Event
{
    public class EventItem
    {
        protected EventItem()
        {
        }

        public EventItem(SourceEnum source, SourceTypeEnum sourceType, string data)
        {
            Data = data;
            Source = source;
            SourceType = sourceType;
        }


        public string Data
        {
            get;
            set;
        }

        public SourceEnum Source
        {
            get;
            set;
        }

        public SourceTypeEnum SourceType
        {
            get;
            set;
        }
    }
}