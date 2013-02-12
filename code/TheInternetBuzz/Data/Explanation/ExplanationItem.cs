using System;

namespace TheInternetBuzz.Data.Explanation
{
    public class ExplanationItem : IComparable
    {
        protected ExplanationItem()
        {
        }

        public ExplanationItem(string id, string query, string explanation)
        {
            ID = id;
            Query = query;
            Explanation = explanation;
        }


        public string ID
        {
            get;
            set;
        }

        public string Query
        {
            get;
            set;
        }

        public string Explanation
        {
            get;
            set;
        }

        public int CompareTo(object obj)
        {
            ExplanationItem t = (ExplanationItem)obj;
            return this.ID.CompareTo(t.ID);
        }

        public override string ToString()
        {
            return ID;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ExplanationItem item = (ExplanationItem)obj;
            return (ID == item.ID);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}