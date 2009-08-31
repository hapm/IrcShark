using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class NickChangeReceivedEventArgs : IrcEventArgs
    {
        private String newNick;
        private UserInfo user;

        public NickChangeReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
            user = new UserInfo(BaseLine);
            newNick = BaseLine.Parameters[0];
        }

        public UserInfo User 
        {
            get { return user; }
        }

        public String NewNick
        {
            get { return newNick; }
        }
    }
}
