// <copyright file="IrcServerEndPoint.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IrcServerEndPoint class.</summary>

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
namespace IrcShark.Chatting.Irc
{
    using System;
    using System.Net;
    using IrcShark.Chatting;

    /// <summary>
    /// Represents an irc endpoint for an irc connection.
    /// </summary>
    public class IrcServerEndPoint : IServer
    {
        /// <summary>
        /// Saves the name of the server.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves the server host name as a string.
        /// </summary>
        private string address;
        
        /// <summary>
        /// Saves the port for the connection.
        /// </summary>
        private int port;
        
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
        /// <param name="hostName">
        /// The dns of the irc server as a <see cref="System.String"/>.
        /// </param>
        public IrcServerEndPoint(string hostName)
        {
            address = hostName;
            name = hostName;
            port = 6667;
        }
        
        /// <summary>
        /// Initializes a new instance of the IrcServerEndPoint class.
        /// </summary>
        /// <param name="hostName">
        /// The dns of the irc server as a <see cref="System.String"/>.
        /// </param>
        /// <param name="port">
        /// The port where the irc server is listening on.
        /// </param>
        public IrcServerEndPoint(string hostName, int port)
        {
            this.port = port;
            address = hostName;
            name = hostName;
        }
        
        /// <summary>
        /// Initializes a new instance of the IrcServerEndPoint class.
        /// </summary>
        /// <param name="name">
        /// The displayed name of the server.
        /// </param>
        /// <param name="address">
        /// The dns of the irc server as a <see cref="System.String"/>.
        /// </param>
        public IrcServerEndPoint(string name, string address)
        {
            this.address = address;
            this.name = name;
        }
        
        /// <summary>
        /// Initializes a new instance of the IrcServerEndPoint class.
        /// </summary>
        /// <param name="name">
        /// The displayed name of the server.
        /// </param>
        /// <param name="address">
        /// The dns of the irc server as a <see cref="System.String"/>.
        /// </param>
        /// <param name="port">
        /// The port where the irc server is listening on.
        /// </param>
        public IrcServerEndPoint(string name, string address, int port)
        {
            this.port = port;
            this.address = address;
            this.name = name;
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
        public IrcServerEndPoint(IPAddress address, int port)
        {
            name = address.ToString();
            this.port = port;
        }
        
        /// <summary>
        /// Initializes a new instance of the IrcServerEndPoint class.
        /// </summary>
        /// <param name="name">
        /// The displayed name of the server.
        /// </param>
        /// <param name="address">
        /// The ip address of the irc server.
        /// </param>
        /// <param name="port">
        /// The port where the irc server is listening on.
        /// </param>
        public IrcServerEndPoint(string name, IPAddress address, int port)
        {
            this.name = name;
            this.port = port;
            this.address = address.ToString();
        }

        /// <summary>
        /// Gets or sets the server host name.
        /// </summary>
        /// <value>
        /// The dns of the ircserver, if could be resolved, else null.
        /// </value>
        public string Address 
        {
            get { return address; }
            set { address = value; }
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
        
        /// <summary>
        /// Gets or sets the display name of a server.
        /// </summary>
        /// <value>The name as a string.</value>
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        
        /// <summary>
        /// Gets or sets the port where the server listens on.
        /// </summary>
        /// <value>The port, the server listens on.</value>
        public int Port 
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// Gets the network, the server configuration was created for.
        /// </summary>
        /// <value>The network instance.</value>
        public INetwork Network 
        {
            get { throw new NotImplementedException(); }
        }
        
        /// <summary>
        /// Compares this IrcNetwork with another object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>
        /// Returns true if the given object is equals to this, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            IrcServerEndPoint server = obj as IrcServerEndPoint;
            if (server == null)
            {
                return false;
            }
            
            return server != null && base.Equals(obj)
                && server.Name.Equals(name);
        }
        
        /// <summary>
        /// Gets the hashcode of this object.
        /// </summary>
        /// <returns>The hashcode.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() ^ name.GetHashCode();
        }
        
        /// <summary>
        /// Gets the ip for this IrcServerAddress.
        /// </summary>
        /// <returns>The IPAddress instance.</returns>
        public IPAddress GetIPAddress()
        {
            IPAddress[] addresses = Dns.GetHostEntry(address).AddressList;
            return addresses[0];            
        }
    }
}
