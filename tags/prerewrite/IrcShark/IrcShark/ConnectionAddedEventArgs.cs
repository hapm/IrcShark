using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp.Extended;

namespace IrcShark
{
    public class ConnectionAddedEventArgs : EventArgs
    {
        private IrcConnection ConnectionValue;

        public ConnectionAddedEventArgs(IrcConnection con)
        {
            ConnectionValue = con;
        }

        public IrcConnection Connection
        {
            get { return ConnectionValue; }
        }
    }
}
