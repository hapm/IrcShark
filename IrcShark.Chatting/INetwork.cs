// <copyright file="INetwork.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the INetwork interface.</summary>

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
    /// Defines the structure of a chat network configuration.
    /// </summary>
    public interface INetwork : System.Collections.Generic.IEnumerable<IServer>
    {
        /// <summary>
        /// Gets or sets the name of the network.
        /// </summary>
        /// <value>The name of the string.</value>
        string Name { get; set; }
        
        /// <summary>
        /// Gets the server configuration at the given index.
        /// </summary>
        /// <param name="index">The index of the server.</param>
        /// <value>The IServer instance at the given index.</value>
        IServer this[int index] 
        { 
            get; 
        }
        
        /// <summary>
        /// Gets the server configuration with the given name.
        /// </summary>
        /// <param name="name">The name of the server.</param>
        /// <value>The IServer instance.</value>
        IServer this[string name] 
        { 
            get; 
        }
        
        /// <summary>
        /// Adds a new server configuration to the network configuration.
        /// </summary>
        /// <param name="name">The name of the server configuration.</param>
        /// <param name="address">The network address as a string.</param>
        /// <returns>The new IServer instance for the implemented protocol.</returns>
        IServer AddServer(string name, string address);
        
        /// <summary>
        /// Removes the server configuration with the given name.
        /// </summary>
        /// <param name="name">The name of the server to remove.</param>
        /// <returns>If the server was found and removed true, false otherwise.</returns>
        bool RemoveServer(string name);
        
        /// <summary>
        /// Removes the server configuration at the given index.
        /// </summary>
        /// <param name="index">The index of the server configuration to remove.</param>
        void RemoveServer(int index);
    }
}
