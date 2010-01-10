// <copyright file="IProtocol.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IProtocol interface.</summary>

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
    /// Defines the minimum propertys and methods of a chat protocol.
    /// </summary>
    public interface IProtocol
    {
        /// <summary>
        /// Gets a value indicating whether the protocol supports multible networks.
        /// </summary>
        /// <value>Its true, if multible networks are supported, false otherwise.</value>
        bool MultiNetwork { get; }
        
        /// <summary>
        /// Gets a value indicating whether the protocol supports multible servers for one network.
        /// </summary>
        /// <value>Its true, if multible servers are supported, false otherwise.</value>
        bool MultiServer { get; }
        
        /// <summary>
        /// Gets the name of the protocol.
        /// </summary>
        /// <value>The name as a string.</value>
        string Name { get; }
        
        /// <summary>
        /// Creates a new network configuration, for the implemented protocol.
        /// </summary>
        /// <param name="name">The name of the network configuration.</param>
        /// <returns>The new instance of the network configuration.</returns>
        INetwork CreateNetwork(string name);
    }
}
