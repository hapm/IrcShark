using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class JoinReceivedEventArgs : IrcEventArgs
    {
        private String ChannelNameValue;
        private UserInfo UserValue;

        public JoinReceivedEventArgs(IrcLine BaseLine)
            : base(BaseLine)
        {
            UserValue = new UserInfo(BaseLine);
            ChannelNameValue = BaseLine.Parameters[0];
        }

        public JoinReceivedEventArgs(String ChannelName, UserInfo JoinedUser) : base(JoinedUser.Client)
        {
            ChannelNameValue = ChannelName;
            UserValue = JoinedUser;
        }

        public String ChannelName
        {
            get { return ChannelNameValue; }
        }

        public UserInfo User
        {
            get { return UserValue; }
        }
    }
}
