using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class IrcEventArgs : EventArgs, IIrcObject
    {
        private bool HandledValue;
        private IrcClient ClientValue;
        private IrcLine BaseLineValue;

        public IrcEventArgs(IrcClient Client)
        {
            HandledValue = false;
            ClientValue = Client;
        }

        public IrcEventArgs(IrcLine BaseLine)
        {
            HandledValue = false;
            ClientValue = BaseLine.Client;
            BaseLineValue = BaseLine;
        }

        public bool Handled
        {
            get
            {
                return HandledValue;
            }
            set
            {
                HandledValue = value;
            }
        }

        public IrcLine BaseLine
        {
            get { return BaseLineValue; }
        }

        #region IIrcObject Member

        public IrcClient Client
        {
            get { return ClientValue; }
        }

        #endregion
    }
}
