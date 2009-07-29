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
        private ChannelUser UserValue;
        private UserLeaveReason ReasonValue;

        public UserLeaveEventArgs(ChannelUser user, UserLeaveReason reason)
        {
            UserValue = user;
            ReasonValue = reason;
        }

        public Channel Channel
        {
            get { return UserValue.Channel; }
        }

        public ChannelUser ChannelUser
        {
            get { return UserValue; }
        }

        public UserLeaveReason Reason
        {
            get { return ReasonValue; }
        }
    }
}
