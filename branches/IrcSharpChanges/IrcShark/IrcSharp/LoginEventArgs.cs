// <copyright file="LoginEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the LoginEventArgs class.</summary>

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
namespace IrcSharp 
{
    using System;

    /// <summary>
    /// The LoginEventArgs belongs to the <see cref="LoginEventHandler" /> and the <see cref="IrcClient.OnLogin" /> event.
    /// </summary>
    /// <remarks>
    /// Gives information about the network and nickname of the successfully logged in connection.
    /// </remarks>
    public class LoginEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the networkname of the irc connection.
        /// </summary>
        private string networkName;
        
        /// <summary>
        /// Saves the accepted nickname on login.
        /// </summary>
        private string nick;
        
        /// <summary>
        /// Initializes a new instance of the LoginEventArgs class.
        /// </summary>
        /// <param name="networkName">The name of the network.</param>
        /// <param name="nick">The accepted nickname.</param>
        /// <param name="client">The client, what logged in.</param>
        public LoginEventArgs(string networkName, string nick, IrcClient client) : base(client)
        {
            this.networkName = networkName;
            this.nick = nick;
        }
        
        /// <summary>
        /// Gets the nick, that was accepted on login to the server.
        /// </summary>
        /// <value>
        /// The nickname as a string.
        /// </value>
        public string Nickname
        {
            get { return nick; }
        }
        
        /// <summary>
        /// Gets the networkname of the server, connected to.
        /// </summary>
        /// <value>
        /// The network name as received from the server.
        /// </value>
        public string NetworkName
        {
            get { return networkName; }
        }
    }
}
