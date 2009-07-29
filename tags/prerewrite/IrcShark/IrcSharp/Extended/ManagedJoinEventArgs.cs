using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class ManagedJoinEventArgs : JoinReceivedEventArgs
    {
        private Channel ChannelValue;

        public ManagedJoinEventArgs(ChannelManager manager, JoinReceivedEventArgs baseArgs) : base(baseArgs.BaseLine)
        {
            ChannelValue = manager[baseArgs.ChannelName];
        }

        public Channel Channel
        {
            get { return ChannelValue; }
        }
    }
}
