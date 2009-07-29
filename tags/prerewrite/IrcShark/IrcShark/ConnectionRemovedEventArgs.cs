using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp.Extended;

namespace IrcShark
{
    public class ConnectionRemovedEventArgs : EventArgs
    {
        private IrcConnection ConnectionValue;

        public ConnectionRemovedEventArgs(IrcConnection con)
        {
            ConnectionValue = con;
        }

        public IrcConnection Connection
        {
            get { return ConnectionValue; }
        }
    }
}
