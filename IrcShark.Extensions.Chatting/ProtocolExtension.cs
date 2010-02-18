// <copyright file="ProtocolExtension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcShark.Extensions.Chatting
{
    using System;
    using IrcShark.Chatting;

    /// <summary>
    /// Defines all methods an propertys a ProtocolExtension should implement.
    /// </summary>
    public abstract class ProtocolExtension : Extension
    {
        /// <summary>
        /// Initializes a new instance of the ProtocolExtension class.
        /// </summary>
        /// <param name="context">The context of the extension.</param>
        public ProtocolExtension(ExtensionContext context) : base(context)
        {
        }
        
        /// <summary>
        /// Gets the protocol, represented by this extension.
        /// </summary>
        /// <value>
        /// The instance for the supported protocol.
        /// </value>
        public abstract IProtocol Protocol
        {
            get;
        }
        
        /// <summary>
        /// Loads a network of the supported protocol from the given NetworkSettings.
        /// </summary>
        /// <param name="settings">The settings to load from.</param>
        /// <returns>The generated instance for the network.</returns>
        /// <exception cref="UnsupportedProtocolExteption">
        /// An UnsupportedProtocolExteption is thrown, if the given settings object is for
        /// another protocol.
        /// </exception>
        public abstract INetwork LoadNetwork(NetworkSettings settings);
        
        /// <summary>
        /// Saves the given network to a new NetworkSettings instance.
        /// </summary>
        /// <param name="network">The network to save.</param>
        /// <returns>The setting instance holding all settings of the network.</returns>
        /// <exception cref="UnsupportedProtocolExteption">
        /// An UnsupportedProtocolExteption is thrown, if the given network object is for
        /// another protocol.
        /// </exception>
        public abstract NetworkSettings SaveNetwork(INetwork network);
    }
}
