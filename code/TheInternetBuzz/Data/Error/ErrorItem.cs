using System;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Error
{
    public class ErrorItem : IComparable
    {
        protected ErrorItem()
        {
        }

        public ErrorItem(string service, string action, string label, Exception exception)
        {
            Service = service;
            Action = action;
            Label = label;
            ErrorException = exception;
            ErrorDateTime = DateTime.UtcNow;
        }


        public string Service
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        public string Label
        {
            get;
            set;
        }

        public Exception ErrorException
        {
            get;
            set;
        }

        public DateTime ErrorDateTime
        {
            get;
            set;
        }

        public int CompareTo(object obj)
        {
            ErrorItem errorItem = (ErrorItem)obj;
            return this.ErrorDateTime.CompareTo(errorItem.ErrorDateTime);
        }

        public override string ToString()
        {
            return "Service:" + Service + ",Action:" + Action  + ",Label:" + Label + ",ErrorException:" + ErrorException.ToString();
        }
    }
}