using System.Collections.Specialized;

namespace TheInternetBuzz.Connectors.RSS
{

    public class Item {

        private StringDictionary dictionary = new StringDictionary();

        public Item() {
        }

        public string Title {
            get;
            set;
        }

        public string Description {
            get;
            set;
        }

        public string URL {
            get;
            set;
        }

        public void Set(string key, string value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public string Get(string key)
        {
            return dictionary[key];
        }

        public void Remove(string key)
        {
            dictionary.Remove(key);
        }
    }
}
