using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class KickReceivedEventArgs : IrcEventArgs
    {
        private UserInfo kicker;
        private String kickedName;
        private String channelName;
        private String ReasonValue;

        public KickReceivedEventArgs(IrcLine BaseLine)
            : base(BaseLine)
        {
            kicker = new UserInfo(BaseLine);
            kickedName = BaseLine.Parameters[1];
            channelName = BaseLine.Parameters[0];
            ReasonValue = BaseLine.Parameters[2];
        }

        public UserInfo Kicker
        {
            get { return kicker; }
        }

        public String KickedName
        {
            get { return kickedName; }
        }

        public String ChannelName
        {
            get { return channelName; }
        }

        public String Reason
        {
            get { return ReasonValue; }
        }
    }
}
