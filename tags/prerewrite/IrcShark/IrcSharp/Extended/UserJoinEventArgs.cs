using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class UserJoinEventArgs : JoinReceivedEventArgs
    {
        private Channel channel;
        private ChannelUser user;

        public UserJoinEventArgs(Channel chan, ChannelUser user, JoinReceivedEventArgs baseArgs)
            : base(baseArgs.BaseLine)
        {
            channel = chan;
            this.user = user;
        }

        public Channel Channel
        {
            get { return channel; }
        }

        public ChannelUser ChannelUser
        {
            get { return user; }
        }
    }
}
