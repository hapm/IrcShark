using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    /// <summary>
    /// Represents the event arguments belonging to the BadNick-Event
    /// </summary>
    public class BadNickEventArgs : IrcEventArgs
    {
        private Boolean IsLoginValue;

        public BadNickEventArgs(IrcLine baseLine, bool inLogin) : base(baseLine)
        {
            IsLoginValue = inLogin;
        }

        /// <summary>
        /// Allows to determine if the connection is logging in at the moment.
        /// </summary>
        /// <remarks>The value of IsLogin is true if the nickname, you wanted to connect with, wasn't excepted by the server and therefor, the login isn't complete.</remarks>
        /// <value>true, if the bad nick was send when the connection is about to log in, else false</value>
        public Boolean IsLogin
        {
            get
            {
                return IsLoginValue;
            }
        }
    }
}
