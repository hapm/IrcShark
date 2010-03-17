// <copyright file="TalkingCollection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcShark.Extensions.Scripting
{
    using System;
    using System.Collections.Generic;

    public delegate void TalkingCollectionEventHandler<TKey, TValue>(object sender, TalkingCollectionEventArgs<TKey, TValue> args);
    /// <summary>
    /// Description of TalkingCollection.
    /// </summary>
    public class TalkingCollection<TKey, TValue> : MarshalByRefObject, IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> dict;
        
        public event TalkingCollectionEventHandler<TKey, TValue> Added;
        
        public event TalkingCollectionEventHandler<TKey, TValue> Removed;
        
        public TalkingCollection()
        {
            dict = new Dictionary<TKey, TValue>();
        }
        
        public TValue this[TKey key] {
            get {
                return dict[key];
            }
            set {
                dict[key] = value;
                OnAdded(key);
            }
        }
        
        public ICollection<TKey> Keys {
            get {
                return dict.Keys;
            }
        }
        
        public ICollection<TValue> Values {
            get {
                return dict.Values;
            }
        }
        
        public int Count {
            get {
                return dict.Count;
            }
        }
        
        public bool IsReadOnly {
            get {
                return false;
            }
        }
        
        public bool ContainsKey(TKey key)
        {
            return dict.ContainsKey(key);
        }
        
        public void Add(TKey key, TValue value)
        {
            dict.Add(key, value);
            OnAdded(key);
        }
        
        public bool Remove(TKey key)
        {
            bool result = dict.Remove(key);
            OnRemoved(key);
            return result;
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            return dict.TryGetValue(key, out value);
        }
        
        public void Clear()
        {
            ICollection<TKey> keys = Keys;
            dict.Clear();
            foreach (TKey key in keys)
            {
                OnRemoved(key);
            }
        }
        
        protected void OnAdded(TKey key)
        {
            if (Added != null)
            {
                Added(this, new TalkingCollectionEventArgs<TKey, TValue>(key));
            }
        }
        
        protected void OnRemoved(TKey key)
        {
            if (Removed != null)
            {
                Removed(this, new TalkingCollectionEventArgs<TKey, TValue>(key));
            }
        }
            
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
        
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            (dict as ICollection<KeyValuePair<TKey, TValue>>).Add(item);
            OnAdded(item.Key);
        }
        
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return (dict as ICollection<KeyValuePair<TKey, TValue>>).Contains(item);
        }
        
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            (dict as ICollection<KeyValuePair<TKey, TValue>>).CopyTo(array, arrayIndex);
        }
        
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            bool result = (dict as ICollection<KeyValuePair<TKey, TValue>>).Remove(item);
            OnRemoved(item.Key);
            return result;
        }
        
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return (dict as IEnumerable<KeyValuePair<TKey, TValue>>).GetEnumerator();
        }
    }
}
