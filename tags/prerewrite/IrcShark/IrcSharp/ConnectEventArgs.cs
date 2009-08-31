using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    /// <summary>
    /// Arguments for the OnConnect event
    /// </summary>
    public class ConnectEventArgs : IrcEventArgs
    {
        public ConnectEventArgs(IrcClient client) : base(client)
        {
        }
    }
}
