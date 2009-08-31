using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class PartedEventArgs : IrcEventArgs
    {
        private Channel channel;

        public PartedEventArgs(Channel chan, IrcClient client)
            : base(client)
        {
            channel = chan;
        }

        public Channel Channel
        {
            get { return channel; }
        }
    }
}
