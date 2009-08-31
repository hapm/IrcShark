using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class JoinReceivedEventArgs : IrcEventArgs
    {
        private String channelName;
        private UserInfo user;

        public JoinReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
            user = new UserInfo(BaseLine);
            channelName = BaseLine.Parameters[0];
        }

        public JoinReceivedEventArgs(String ChannelName, UserInfo JoinedUser) : base(JoinedUser.Client)
        {
            channelName = ChannelName;
            user = JoinedUser;
        }

        public String ChannelName
        {
            get { return channelName; }
        }

        public UserInfo User
        {
            get { return user; }
        }
    }
}
