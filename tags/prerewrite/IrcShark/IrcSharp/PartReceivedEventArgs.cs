using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class PartReceivedEventArgs : IrcEventArgs
    {
        private String channelName;
        private String partMessage;
        private UserInfo user;

        public PartReceivedEventArgs(IrcLine BaseLine)
            : base(BaseLine)
        {
            user = new UserInfo(BaseLine);
            channelName = BaseLine.Parameters[0];
            if (BaseLine.Parameters.Length > 1)
                partMessage = BaseLine.Parameters[1];
        }

        public PartReceivedEventArgs(String ChannelName, UserInfo PartedUser) : base(PartedUser.Client)
        {
            channelName = ChannelName;
            user = PartedUser;
        }

        public String ChannelName
        {
            get
            {
                return channelName;
            }
        }

        public String PartMessage
        {
            get
            {
                return partMessage;
            }
        }

        public UserInfo User
        {
            get
            {
                return user;
            }
        }
    }
}
