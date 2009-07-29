using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    /// <summary>
    /// The arguments for the OnLogin event.
    /// </summary>
    public class LoginEventArgs : IrcEventArgs
    {
        private String NetworkNameValue;
        private String NickValue;

        public LoginEventArgs(String NetworkName, String Nick, IrcClient client) : base(client)
        {
            NetworkNameValue = NetworkName;
            NickValue = Nick;
        }

        /// <summary>
        /// The nickname you logged in with.
        /// </summary>
        /// <value>the current nickname</value>
        public String Nickname
        {
            get
            {
                return NickValue;
            }
        }

        /// <summary>
        /// This property is set to the network name, the irc client logged into.
        /// </summary>
        /// <value>the network name</value>
        public String NetworkName
        {
            get
            {
                return NetworkNameValue;
            }
        }

    }
}
