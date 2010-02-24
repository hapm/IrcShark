// <copyright file="IConnection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IConnection interface.</summary>

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
namespace IrcShark.Chatting
{
    using System;

    /// <summary>
    /// Represents a connection to a chatting network.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Gets information about the server, the client ist connected to.
        /// </summary>
        /// <value>
        /// The server for the connection.
        /// </value>
        IServer Server { get; }
        
        /// <summary>
        /// Gets a value indicating whether the connection is open or not.
        /// </summary>
        /// <value>
        /// Its true if the connection is established and false otherwise.
        /// </value>
        bool IsConnected { get; }
        
        /// <summary>
        /// Gets or sets the alias name used in this connection.
        /// </summary>
        /// <value>
        /// The nickname as a string.
        /// </value>
        string Nickname { get; set; }
        
        /// <summary>
        /// Gets or sets the username as used for this connection.
        /// </summary>
        /// <value>The username as a string.</value>
        string UserName { get; set; }

        /// <summary>
        /// Closes the current connection.
        /// </summary>
        void Close();
    }
}
