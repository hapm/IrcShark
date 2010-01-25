using System;
using System.Collections.Generic;
using System.Text;

namespace IrcCloneShark
{
    public class WindowOpenedEventArgs : EventArgs
    {
        private BaseWindow OpenedWindowValue;

        public WindowOpenedEventArgs(BaseWindow window)
        {
            OpenedWindowValue = window;
        }

        public BaseWindow OpenedWindow
        {
            get { return OpenedWindowValue; }
        }
    }
}
