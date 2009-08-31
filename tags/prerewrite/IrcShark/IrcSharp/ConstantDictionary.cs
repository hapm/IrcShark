using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class ConstantDictionary<TKey,TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, TValue> baseDictionary;

        public ConstantDictionary(Dictionary<TKey, TValue> baseDict)
        {
            baseDictionary = baseDict;
        }

        public TValue this[TKey key] 
        {
            get { return baseDictionary[key]; }
        }

        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get { return baseDictionary.Values; }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get { return baseDictionary.Keys; }
        }

        public bool ContainsKey(TKey key)
        {
            return baseDictionary.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return baseDictionary.ContainsValue(value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return baseDictionary.TryGetValue(key, out value);
        }

        public int Count
        {
            get { return baseDictionary.Count; }
        }

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return baseDictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return baseDictionary.GetEnumerator();
        }

        #endregion
    }
}
