using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp;

namespace IrcSharp.Extended
{
    public class RemovedEventArgs<T> : EventArgs
    {
        private T item;

        public RemovedEventArgs(T item)
        {
            this.item = item;
        }

        public T Item
        {
            get { return item; }
        }
    }
}
