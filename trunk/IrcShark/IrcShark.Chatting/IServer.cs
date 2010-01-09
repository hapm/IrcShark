// <copyright file="IServer.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IServer interface.</summary>

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
namespace IrcShark.Chatting
{
    using System;

    /// <summary>
    /// Defines the structur of a server address for chat protocols.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Gets or sets the display name of a server.
        /// </summary>
        /// <value>The name as a string.</value>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the address of the server as a string.
        /// </summary>
        /// <value>
        /// The address of the server as a string.
        /// </value>
        string Address { get; set; }
        
        /// <summary>
        /// Gets the network, the server configuration was created for.
        /// </summary>
        /// <value>The network instance.</value>
        INetwork Network { get; }
    }
}
