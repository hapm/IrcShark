using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class QuitReceivedEventArgs : IrcEventArgs
    {
        private UserInfo user;
        private String quitMessage;

        public QuitReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
            user = new UserInfo(BaseLine);
            if (BaseLine.Parameters.Length > 0)
                quitMessage = BaseLine.Parameters[0];
            else
                quitMessage = "";
        }

        public QuitReceivedEventArgs(UserInfo QuittedUser, String QuitMessage) : base(QuittedUser.Client)
        {
            user = QuittedUser;
        }

        public UserInfo User
        {
            get
            {
                return user;
            }
        }

        public String QuitMessage
        {
            get { return quitMessage; }
        }
    }
}
