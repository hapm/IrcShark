// <copyright file="IrcConnection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IrcConnection class.</summary>

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
namespace IrcShark.Chatting.Irc.Extended
{
    using System;
    using IrcShark.Chatting;

    /// <summary>
    /// Represents a full irc-connection with channel management and internal address list.
    /// </summary>
    /// <remarks>This class uses all classes of IrcShark.Chatting.Irc to manage an irc connection.</remarks>
    public class IrcConnection : IrcClient, IConnection
    {        
        /// <summary>
        /// Saves the number of all available connections.
        /// </summary>
        private static int instanceCount = 0;
        
        /// <summary>
        /// Saves the ChannelManager instance.
        /// </summary>
        private ChannelManager channels;
        
        /// <summary>
        /// Saves the id of the current connection.
        /// </summary>
        private int connectionID;
        
        /// <summary>
        /// Saves the server to connect to.
        /// </summary>
        private IrcServerEndPoint server;

        /// <summary>
        /// Initializes a new instance of the IrcConnection class.
        /// </summary>
        public IrcConnection()
        {
            channels = new ChannelManager(this);
            connectionID = ++instanceCount;
        }

        /// <summary>
        /// Initializes a new instance of the IrcConnection class.
        /// </summary>
        /// <param name="server">The server configuration to use.</param>
        public IrcConnection(IrcServerEndPoint server)
        {
            this.server = server;
            channels = new ChannelManager(this);
            connectionID = ++instanceCount;
        }

        /// <summary>
        /// Gets the client, this connection bases on.
        /// </summary>
        /// <value>The IrcClient instance for this IrcConnection.</value>
        public IrcClient Client
        {
            get { return this; }
        }

        /// <summary>
        /// Gets the ChannelManager object for the curent IrcConnection.
        /// </summary>
        /// <value>The ChannelManager object of the current connection.</value>
        public ChannelManager Channels
        {
            get { return channels; }
        }

        /// <summary>
        /// Gets the id of the connection.
        /// </summary>
        /// <value>An identification number for this connection.</value>
        public int ConnectionID
        {
            get { return connectionID; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the connection is established or not.
        /// </summary>
        /// <value>
        /// Its true if the connection is established and false otherwise.
        /// </value>
        public bool IsConnected 
        {
            get { throw new NotImplementedException(); }
        }
        
        /// <summary>
        /// Gets information about the server, the client ist connected to.
        /// </summary>
        /// <value>
        /// The server for the connection.
        /// </value>
        IServer IConnection.Server 
        {
            get { return Server; }
        }
        
        /// <summary>
        /// Gets information about the irc server, the client ist connected to.
        /// </summary>
        /// <value>
        /// The server for the connection.
        /// </value>
        public IrcServerEndPoint Server 
        {
            get { return server; }
        }
        
        /// <summary>
        /// Gets or sets the alias name used in this connection.
        /// </summary>
        /// <value>
        /// The nickname as a string.
        /// </value>
        public string Nickname 
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        
        /// <summary>
        /// Gets or sets the username as used for this connection.
        /// </summary>
        /// <value>The username as a string.</value>
        public string UserName 
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
            
        /// <summary>
        /// Closes the current connection.
        /// </summary>
        public void Close()
        {
            if (IsConnected)
            {
                Dispose();
            }
        }
    }
}