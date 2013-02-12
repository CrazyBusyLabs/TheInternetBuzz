using System;
using System.Collections;

namespace TheInternetBuzz.Data.Audit
{
    public class AuditDerviceItemComparer : IComparer
    {
        public AuditServiceItemComparerEnum SortBy
        {
            get;
            set;
        }

        public AuditDerviceItemComparer()
        {
            SortBy = AuditServiceItemComparerEnum.StartTime;
        }

        public AuditDerviceItemComparer(AuditServiceItemComparerEnum sortBy)
        {
            SortBy = sortBy;
        }


        public int Compare(object x, object y)
        {
            AuditServiceItem item1;
            AuditServiceItem item2;

            if (x is AuditServiceItem)
                item1 = x as AuditServiceItem;
            else
                throw new ArgumentException("Object is not of type AuditItem.");

            if (y is AuditServiceItem)
                item2 = y as AuditServiceItem;
            else
                throw new ArgumentException("Object is not of type AuditItem.");

            return item1.CompareTo(item2, SortBy);
        }

    }
}
