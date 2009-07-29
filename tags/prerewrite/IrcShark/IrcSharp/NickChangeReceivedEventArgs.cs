using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class NickChangeReceivedEventArgs : IrcEventArgs
    {
        private String NewNickValue;
        private UserInfo UserValue;

        public NickChangeReceivedEventArgs(IrcLine BaseLine)
            : base(BaseLine)
        {
            UserValue = new UserInfo(BaseLine);
            NewNickValue = BaseLine.Parameters[0];
        }

        public UserInfo User 
        {
            get { return UserValue; }
        }

        public String NewNick
        {
            get { return NewNickValue; }
        }
    }
}
