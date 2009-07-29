using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp;

namespace IrcSharp.Extended
{
    /// <summary>
    /// Arguments for the Added event of the <see cref="EventRaisingList"/>
    /// </summary>
    public class AddedEventArgs<T> : EventArgs
    {
        private T ItemValue;

        public AddedEventArgs(T item)
        {
            ItemValue = item;
        }

        /// <summary>
        /// The new item what was added to the list.
        /// </summary>
        /// <value>the new item</value>
        public T Item
        {
            get { return ItemValue; }
        }
    }
}
