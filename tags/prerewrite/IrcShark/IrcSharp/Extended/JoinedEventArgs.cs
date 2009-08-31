using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class JoinedEventArgs : IrcEventArgs
    {
        private Channel channel;

        public JoinedEventArgs(Channel chan, IrcClient client) : base(client)
        {
            channel = chan;
        }

        public Channel Channel
        {
            get { return channel; }
        }
    }
}
