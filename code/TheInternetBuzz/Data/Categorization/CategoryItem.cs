using System;
using TheInternetBuzz.Data.Topics;

namespace TheInternetBuzz.Data.Categorization
{
    public class CategoryItem : IComparable
    {
        private CategoryItem()
        {
        }

        public CategoryItem(string id)
        {
            ID = id;
            ProviderMappingList = new ProviderMappingList();
            TopicList = new TopicList();
        }

        public string ID
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            set;
        }

        public ProviderMappingList ProviderMappingList
        {
            get;
            private set;
        }

        public TopicList TopicList
        {
            get;
            private set;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is CategoryItem))
            {
                throw new ArgumentException("Object is not of type CategoryItem.");
            }
            return ID.CompareTo(((CategoryItem)obj).ID);
        }
    }
}
