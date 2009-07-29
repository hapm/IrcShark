using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class QuitReceivedEventArgs : IrcEventArgs
    {
        private UserInfo UserValue;
        private String QuitMessageValue;

        public QuitReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
            UserValue = new UserInfo(BaseLine);
            if (BaseLine.Parameters.Length > 0)
                QuitMessageValue = BaseLine.Parameters[0];
            else
                QuitMessageValue = "";
        }

        public QuitReceivedEventArgs(UserInfo QuittedUser, String QuitMessage) : base(QuittedUser.Client)
        {
            UserValue = QuittedUser;
        }

        public UserInfo User
        {
            get
            {
                return UserValue;
            }
        }

        public String QuitMessage
        {
            get { return QuitMessageValue; }
        }
    }
}
