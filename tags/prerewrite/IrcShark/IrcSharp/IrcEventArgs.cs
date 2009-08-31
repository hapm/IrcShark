using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class IrcEventArgs : EventArgs, IIrcObject
    {
        private bool handled;
        private IrcClient client;
        private IrcLine baseLine;

        public IrcEventArgs(IrcClient Client)
        {
            handled = false;
            client = Client;
        }

        public IrcEventArgs(IrcLine BaseLine)
        {
            handled = false;
            client = BaseLine.Client;
            baseLine = BaseLine;
        }

        public bool Handled
        {
            get
            {
                return handled;
            }
            set
            {
                handled = value;
            }
        }

        public IrcLine BaseLine
        {
            get { return baseLine; }
        }

        #region IIrcObject Member

        public IrcClient Client
        {
            get { return client; }
        }

        #endregion
    }
}
