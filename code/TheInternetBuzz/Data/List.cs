using System;
using System.Collections;

namespace TheInternetBuzz.Data
{
    public class List : IEnumerable 
    {
        private ArrayList arraylist;

        public List() 
        {
            arraylist = new ArrayList();
        }

        public void Add(Object obj) 
        {
            arraylist.Add(obj);
        }

        public void Remove(Object obj)
        {
            arraylist.Remove(obj);
        }

        public bool Contains(Object obj) 
        {
            return arraylist.Contains(obj);
        }

        public void AddRange(List list)
        {
            if (list != null)
            {
                arraylist.AddRange(list.arraylist);
            }
        }


        public void Sort() 
        {
            arraylist.Sort();
        }

        public void Sort(IComparer comparer)
        {
            arraylist.Sort(comparer);
        }

        public Object Item(int i)
        {
            return arraylist[i];
        }

        public int Count() {
            return arraylist.Count;
        }

        public IEnumerator GetEnumerator() {
            return new NestedEnumerator(arraylist);
        }

        //private enumerator class
        private class NestedEnumerator : IEnumerator {
            public ArrayList list;
            int position = -1;

            public NestedEnumerator(ArrayList list) {
                this.list = list;
            }

            private IEnumerator getEnumerator() {
                return (IEnumerator)this;
            }

            public bool MoveNext() {
                position++;
                return (position < list.Count);
            }

            public void Reset() { 
                position = -1; 
            }

            public object Current {
                get {
                    try {
                        return list[position];

                    } catch (IndexOutOfRangeException) {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
