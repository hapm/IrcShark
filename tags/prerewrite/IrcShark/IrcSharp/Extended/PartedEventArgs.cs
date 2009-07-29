using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class PartedEventArgs : IrcEventArgs
    {
        private Channel ChannelValue;

        public PartedEventArgs(Channel chan, IrcClient client)
            : base(client)
        {
            ChannelValue = chan;
        }

        public Channel Channel
        {
            get { return ChannelValue; }
        }
    }
}
