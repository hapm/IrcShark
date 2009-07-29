using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class ChannelListBeginEventArgs : IrcEventArgs
    {
        public ChannelListBeginEventArgs(IrcLine baseLine) : base(baseLine)
        {
        }
    }
}
