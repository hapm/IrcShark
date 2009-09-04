// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
//  
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
