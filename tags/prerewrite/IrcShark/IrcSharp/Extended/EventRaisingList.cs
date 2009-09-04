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

namespace IrcSharp.Extended
{
    public delegate void AddedEventHandler<T>(object sender, AddedEventArgs<T> args);
    public delegate void RemovedEventHandler<T>(object sender, RemovedEventArgs<T> args);

    public class EventRaisingList<T> : List<T>
    {
        #region "Events"
        public event AddedEventHandler<T> Added;
        public event RemovedEventHandler<T> Removed;
        #endregion

        #region "Event raising methods"
        protected void OnAdded(T item)
        {
            if (Added != null) 
                Added(this, new AddedEventArgs<T>(item));
        }

        protected void OnRemoved(T item)
        {
            if (Removed != null) 
                Removed(this, new RemovedEventArgs<T>(item));
        }
        #endregion

        #region "Public Methods"
        public new void Add(T item)
        {
            base.Add(item);
            OnAdded(item);
        }

        public new void AddRange(IEnumerable<T> items)
        {
            base.AddRange(items);
            if (Added == null) return;
            foreach (T item in items)
               OnAdded(item);
        }

        public new bool Remove(T item)
        {
            bool result;
            result = base.Remove(item);
            if (result) 
                OnRemoved(item);
            return result;
        }

        public new void RemoveAt(int index)
        {
            T item = this[index];
            base.RemoveAt(index);
            OnRemoved(item);
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            OnAdded(item);
        }

        public new void InsertRange(int index, IEnumerable<T> items)
        {
            base.InsertRange(index, items);
            if (Added == null) return;
            foreach (T item in items)
                OnAdded(item);
        }

        public new void RemoveRange(int index, int count)
        {
            List<T> toDel = base.GetRange(index, count);
            base.RemoveRange(index, count);
            if (Removed == null) return;
            foreach (T item in toDel)
                OnRemoved(item);
        }

        public new void Clear()
        {
            T[] items = ToArray();
            base.Clear();
            if (Removed == null) return;
            foreach (T item in items)
            {
                OnRemoved(item);
            }
        }
        #endregion
    }
}
