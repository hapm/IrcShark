using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class ManagedJoinEventArgs : JoinReceivedEventArgs
    {
        private Channel channel;

        public ManagedJoinEventArgs(ChannelManager manager, JoinReceivedEventArgs baseArgs) : base(baseArgs.BaseLine)
        {
            channel = manager[baseArgs.ChannelName];
        }

        public Channel Channel
        {
            get { return channel; }
        }
    }
}
