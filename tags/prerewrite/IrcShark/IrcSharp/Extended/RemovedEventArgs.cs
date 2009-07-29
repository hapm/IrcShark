using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp;

namespace IrcSharp.Extended
{
    public class RemovedEventArgs<T> : EventArgs
    {
        private T ItemValue;

        public RemovedEventArgs(T item)
        {
            ItemValue = item;
        }

        public T Item
        {
            get { return ItemValue; }
        }
    }
}
