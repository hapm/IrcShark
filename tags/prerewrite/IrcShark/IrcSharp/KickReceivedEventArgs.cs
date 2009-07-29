using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class KickReceivedEventArgs : IrcEventArgs
    {
        private UserInfo KickerValue;
        private String KickedNameValue;
        private String ChannelNameValue;
        private String ReasonValue;

        public KickReceivedEventArgs(IrcLine BaseLine)
            : base(BaseLine)
        {
            KickerValue = new UserInfo(BaseLine);
            KickedNameValue = BaseLine.Parameters[1];
            ChannelNameValue = BaseLine.Parameters[0];
            ReasonValue = BaseLine.Parameters[2];
        }

        public UserInfo Kicker
        {
            get { return KickerValue; }
        }

        public String KickedName
        {
            get { return KickedNameValue; }
        }

        public String ChannelName
        {
            get { return ChannelNameValue; }
        }

        public String Reason
        {
            get { return ReasonValue; }
        }
    }
}
