using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp.Extended
{
    public enum UserLeaveReason
    {
        Parted,
        Quit,
        Kicked
    }

    public class UserLeaveEventArgs : EventArgs
    {
        private ChannelUser user;
        private UserLeaveReason reason;

        public UserLeaveEventArgs(ChannelUser user, UserLeaveReason reason)
        {
            this.user = user;
            this.reason = reason;
        }

        public Channel Channel
        {
            get { return user.Channel; }
        }

        public ChannelUser ChannelUser
        {
            get { return user; }
        }

        public UserLeaveReason Reason
        {
            get { return reason; }
        }
    }
}
