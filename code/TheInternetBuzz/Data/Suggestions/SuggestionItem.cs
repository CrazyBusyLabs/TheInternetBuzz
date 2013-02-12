using System;
using TheInternetBuzz.Data;
using TheInternetBuzz.Util;

namespace TheInternetBuzz.Data.Suggestions
{
    public class SuggestionItem : IComparable
    {
        public SuggestionItem()
        {
        }
        
        public SuggestionItem(string name)
        {
            Weight = 1;
            Name = TextCleaner.CleanTitle(name);
        }

        public ProviderEnum Provider
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public double NumberQueries
        {
            get;
            set;
        }

        public short Weight
        {
            get;
            set;
        }

        public int CompareTo(object obj) {
            SuggestionItem t = (SuggestionItem)obj;
            return this.Name.CompareTo(t.Name);
        }

        public override string ToString() {
            return Name;
        }

        public override bool Equals(Object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            SuggestionItem item = (SuggestionItem)obj;
            return (Name == item.Name);
        }

        public override int GetHashCode() {
            return Name.GetHashCode();
        }
    }
}