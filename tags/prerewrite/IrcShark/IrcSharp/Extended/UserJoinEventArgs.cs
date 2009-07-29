using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public class UserJoinEventArgs : JoinReceivedEventArgs
    {
        private Channel ChannelValue;
        private ChannelUser UserValue;

        public UserJoinEventArgs(Channel chan, ChannelUser user, JoinReceivedEventArgs baseArgs)
            : base(baseArgs.BaseLine)
        {
            ChannelValue = chan;
            UserValue = user;
        }

        public Channel Channel
        {
            get { return ChannelValue; }
        }

        public ChannelUser ChannelUser
        {
            get { return UserValue; }
        }
    }
}
