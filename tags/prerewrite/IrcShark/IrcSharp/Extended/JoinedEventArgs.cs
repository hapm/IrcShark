using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class JoinedEventArgs : IrcEventArgs
    {
        private Channel ChannelValue;

        public JoinedEventArgs(Channel chan, IrcClient client) : base(client)
        {
            ChannelValue = chan;
        }

        public Channel Channel
        {
            get { return ChannelValue; }
        }
    }
}
