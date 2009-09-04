// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
//  
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
