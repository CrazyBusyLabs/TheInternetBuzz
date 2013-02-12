using System;
using System.Collections.Specialized;

namespace TheInternetBuzz.Connectors.Atom
{

    public class Entry {

        private StringDictionary dictionary = new StringDictionary();

        public Entry() {
        }

        public string ID {
            get;
            set;
        }

        public string Title {
            get;
            set;
        }

        public string Content {
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
