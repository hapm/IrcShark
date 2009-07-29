using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class PartReceivedEventArgs : IrcEventArgs
    {
        private String ChannelNameValue;
        private String PartMessageValue;
        private UserInfo UserValue;

        public PartReceivedEventArgs(IrcLine BaseLine)
            : base(BaseLine)
        {
            UserValue = new UserInfo(BaseLine);
            ChannelNameValue = BaseLine.Parameters[0];
            if (BaseLine.Parameters.Length > 1)
                PartMessageValue = BaseLine.Parameters[1];
        }

        public PartReceivedEventArgs(String ChannelName, UserInfo PartedUser) : base(PartedUser.Client)
        {
            ChannelNameValue = ChannelName;
            UserValue = PartedUser;
        }

        public String ChannelName
        {
            get
            {
                return ChannelNameValue;
            }
        }

        public String PartMessage
        {
            get
            {
                return PartMessageValue;
            }
        }

        public UserInfo User
        {
            get
            {
                return UserValue;
            }
        }
    }
}
