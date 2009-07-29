using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class ChannelListEndEventArgs : IrcEventArgs
    {
        private IrcLine[] ChannelListLinesValue;

        public ChannelListEndEventArgs(IrcLine baseLine, IrcLine[] channellistLines) : base(baseLine)
        {
            ChannelListLinesValue = channellistLines;
        }

        public IrcLine[] ChannelListLines
        {
            get { return ChannelListLinesValue; }
        }
    }
}
