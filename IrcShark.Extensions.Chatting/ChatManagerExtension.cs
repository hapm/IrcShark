// <copyright file="ChatManagerExtension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChatManagerExtension class.</summary>

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
    using System.Collections.Generic;
    using IrcShark.Chatting;
    using IrcShark.Extensions;

    /// <summary>
    /// The ChatManagerExtension allows to manage connections to chat servers
    /// with different protocols.
    /// </summary>
    public class ChatManagerExtension : Extension
    {
        /// <summary>
        /// Saves if the extension is running or not.
        /// </summary>
        private bool running;
        
        /// <summary>
        /// Saves a list of all registred protocols.
        /// </summary>
        private List<ProtocolExtension> registredProtocols;
        
        /// <summary>
        /// Saves the list of configured networks.
        /// </summary>
        private List<INetwork> configuredNetworks;
        
        /// <summary>
        /// Saves a list of all open connections.
        /// </summary>
        private List<IConnection> openConnections;
        
        /// <summary>
        /// Initializes a new instance of the ChatManagerExtension class.
        /// </summary>
        /// <param name="context">The context, this extension runs in.</param>
        public ChatManagerExtension(ExtensionContext context) : base(context)
        {
            registredProtocols = new List<ProtocolExtension>();
            openConnections = new List<IConnection>();
            configuredNetworks = new List<INetwork>();
        }
        
        /// <summary>
        /// Gets a list of all configured networks.
        /// </summary>
        /// <value>A list of INetwork instances.</value>
        public List<INetwork> Networks
        {
            get { return configuredNetworks; }
        }
        
        /// <summary>
        /// Registeres a new chat protocol, that can be used by the chatting extension.
        /// </summary>
        /// <param name="prot">
        /// An instance of the IProtocol interface for the given protocol.
        /// </param>
        public void RegisterProtocol(ProtocolExtension prot)
        {
            if (registredProtocols.Contains(prot))
            {
                return;
            }
            
            registredProtocols.Add(prot);
        }
        
        /// <summary>
        /// Starts the ChatManagerExtension.
        /// </summary>
        public override void Start()
        {
            running = true;
        }
        
        /// <summary>
        /// Stops the ChatManagerExtension.
        /// </summary>
        public override void Stop()
        {
            running = false;
        }
    }
}
