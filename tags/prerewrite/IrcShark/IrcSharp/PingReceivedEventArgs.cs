using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class PingReceivedEventArgs : IrcEventArgs
    {
        public PingReceivedEventArgs(IrcClient Client) : base(Client)
        {
        }

        public PingReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
        }
    }
}
