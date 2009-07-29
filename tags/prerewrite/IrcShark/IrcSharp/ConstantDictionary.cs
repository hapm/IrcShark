using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class ConstantDictionary<TKey,TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, TValue> BaseDictionary;

        public ConstantDictionary(Dictionary<TKey, TValue> baseDict)
        {
            BaseDictionary = baseDict;
        }

        public TValue this[TKey key] 
        {
            get { return BaseDictionary[key]; }
        }

        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get { return BaseDictionary.Values; }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get { return BaseDictionary.Keys; }
        }

        public bool ContainsKey(TKey key)
        {
            return BaseDictionary.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return BaseDictionary.ContainsValue(value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return BaseDictionary.TryGetValue(key, out value);
        }

        public int Count
        {
            get { return BaseDictionary.Count; }
        }

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return BaseDictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return BaseDictionary.GetEnumerator();
        }

        #endregion
    }
}
