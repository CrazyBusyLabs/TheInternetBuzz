using System;
using TheInternetBuzz.Data;

namespace TheInternetBuzz.Data.Audit
{
    public class AuditServiceItem : IComparable
    {
        protected AuditServiceItem()
        {
        }

        public AuditServiceItem(string service, string action, string label)
        {
            Service = service;
            Action = action;
            Label = label;
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

        public TimeSpan Duration
        {
            get;
            set;
        }

        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        public int CompareTo(object obj)
        {
            AuditServiceItem auditItem = (AuditServiceItem)obj;
            return this.StartTime.CompareTo(auditItem.StartTime);
        }

        public int CompareTo(AuditServiceItem item2, AuditServiceItemComparerEnum sortBy)
        {
            switch (sortBy)
            {
                case AuditServiceItemComparerEnum.StartTime:
                    return StartTime.CompareTo(item2.StartTime);
                case AuditServiceItemComparerEnum.Duration:
                    return Duration.CompareTo(item2.Duration);
                case AuditServiceItemComparerEnum.Service:
                    return Service.CompareTo(item2.Service);
                default:
                    return StartTime.CompareTo(item2.StartTime);
            }
        }

        public override string ToString()
        {
            if (Duration == null)
            {
                return "Service: " + Service + ", Action: " + Action + ", Label: " + Label + ", Duration:none";
            }
            else
            {
                return "Service: " + Service + ", Action: " + Action + " , Label: " + Label + ", Duration:" + Duration.TotalMilliseconds.ToString();
            }
        }
    }
}