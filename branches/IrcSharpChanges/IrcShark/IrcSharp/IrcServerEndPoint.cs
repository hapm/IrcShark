// $Id$
// 
// Note:
// 
// Copyright (C) 2009 IrcShark Team
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

namespace IrcSharp
{
    using System;
    using System.Net;

    /// <summary>
    /// Represents an irc endpoint for an irc connection.
    /// </summary>
    public class IrcServerEndPoint : System.Net.IPEndPoint
    {
        /// <summary>
        /// Saves the server host name as a string.
        /// </summary>
        private string serverHostName;
        
        /// <summary>
        /// Saves if an identd is required or not.
        /// </summary>
        private bool isIdentDRequired;
        
        /// <summary>
        /// Saves the password for the server.
        /// </summary>
        private string password;
        
        /// <summary>
        /// Initializes a new instance of the IrcServerEndPoint class.
        /// </summary>
        /// <param name="hostname">
        /// The dns of the irc server as a <see cref="System.String"/>.
        /// </param>
        /// <param name="port">
        /// The port where the irc server is listening on.
        /// </param>
        public IrcServerEndPoint(string hostname, int port) : base(0, 0)
        {
            IPAddress[] addresses = Dns.GetHostEntry(hostname).AddressList;
            Address = addresses[0];
            Port = port;
            serverHostName = hostname;
        }
        
        /// <summary>
        /// Initializes a new instance of the IrcServerEndPoint class.
        /// </summary>
        /// <param name="address">
        /// The ip address of the irc server.
        /// </param>
        /// <param name="port">
        /// The port where the irc server is listening on.
        /// </param>
        public IrcServerEndPoint(IPAddress address, int port) : base(address, port)
        {
        }
        
        /// <summary>
        /// Gets or sets the server host name.
        /// </summary>
        /// <value>
        /// The dns of the ircserver, if could be resolved, else null.
        /// </value>
        public string ServerHostName 
        {
            get 
            { 
                return serverHostName;
            }
            set 
            { 
                IPAddress[] addresses = Dns.GetHostEntry(value).AddressList;
                Address = addresses[0];
                serverHostName = value; 
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether this irc end point needs a running identd when establishing the connection.
        /// </summary>
        /// <value>
        /// True, if the identd is needed, false otherwises.
        /// </value>
        public bool IsIdentDRequired
        {
            get { return isIdentDRequired; }
            set { isIdentDRequired = value; }
        }
        
        /// <summary>
        /// Gets or sets the password to use when establishing a connection to this irc end point.
        /// </summary>
        /// <value>
        /// The password as a string, use null to use no password.
        /// </value>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
