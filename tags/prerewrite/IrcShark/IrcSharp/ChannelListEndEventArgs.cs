using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class ChannelListEndEventArgs : IrcEventArgs
    {
        private IrcLine[] channelListLines;

        public ChannelListEndEventArgs(IrcLine baseLine, IrcLine[] channellistLines) : base(baseLine)
        {
            channelListLines = channellistLines;
        }

        public IrcLine[] ChannelListLines
        {
            get { return channelListLines; }
        }
    }
}
