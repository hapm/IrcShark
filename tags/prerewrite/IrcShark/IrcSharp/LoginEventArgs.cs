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
        private String networkName;
        private String nick;

        public LoginEventArgs(String NetworkName, String Nick, IrcClient client) : base(client)
        {
            networkName = NetworkName;
            nick = Nick;
        }

        /// <summary>
        /// The nickname you logged in with.
        /// </summary>
        /// <value>the current nickname</value>
        public String Nickname
        {
            get { return nick; }
        }

        /// <summary>
        /// This property is set to the network name, the irc client logged into.
        /// </summary>
        /// <value>the network name</value>
        public String NetworkName
        {
            get { return networkName; }
        }

    }
}
