// <copyright file="NetworksCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the NetworksCommand class.</summary>

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
namespace IrcShark.Connectors.TerminalChatting
{
    using System;
    using IrcShark.Extensions.Terminal;
    using IrcShark.Extensions.Chatting;
    using IrcShark.Chatting;

    /// <summary>
    /// Description of NetworksCommand.
    /// </summary>
    public class NetworksCommand : TerminalCommand
    {
        /// <summary>
        /// Saves the instance of the ChatManagerExtension.
        /// </summary>
        private ChatManagerExtension chatting;
        
        /// <summary>
        /// Initializes a new instance of the NetworksCommand class.
        /// </summary>
        /// <param name="terminal">The terminal to create the command for.</param>
        public NetworksCommand(ChatManagerExtension chatting, TerminalExtension terminal) : base("network", terminal)
        {
            this.chatting = chatting;
        }
        
        /// <summary>
        /// Executes the networks command.
        /// </summary>
        /// <param name="paramList">A list of parameters.</param>
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length == 0)
            {
                ListNetworks();
                return;
            }
            switch (paramList[0])
            {
                case "-a":
                case "--add":
                    AddNetwork(paramList);
                    break;
            }
        }
        
        /// <summary>
        /// Shows a list of all networks.
        /// </summary>
        private void ListNetworks()
        {
            if (chatting.Networks.Count == 0)
            {            
                Terminal.WriteLine("There are no configured networks. Use network -a to add some.");                
                return;
            }
            
            Terminal.WriteLine("Listing all configured networks:");
            foreach (INetwork network in chatting.Networks)
            {
                Terminal.WriteLine(network.Name);
            }
        }
        
        /// <summary>
        /// Adds a new network to the networ configuration.
        /// </summary>
        /// <param name="paramList">
        /// The parameters of the network.
        /// </param>
        private void AddNetwork(string[] paramList) 
        {
            ProtocolExtension protocol = null;
            string protocolName;
            string networkName;
            INetwork network;
            
            if (paramList.Length < 2)
            {
                Terminal.WriteLine("Please specify a protocol to use.");
                return;
            }
            protocolName = paramList[1].ToUpper();
            
            if (paramList.Length != 3)
            {
                Terminal.WriteLine("Please specify a network name.");
                return;
            }
            networkName = paramList[2];
            
            foreach (ProtocolExtension pro in chatting.Protocols)
            {
                if (pro.Protocol.Name.ToUpper().Equals(protocolName))
                {
                    protocol = pro;
                    break;
                }
            }
            
            if (protocol == null)
            {
                Terminal.WriteLine("The protocol '{0}' doesn't exist, type protcols to get a list of currently installed protocols.", protocolName);
                return;
            }
            
            network = protocol.Protocol.CreateNetwork(networkName);
            chatting.Networks.Add(network);
            Terminal.WriteLine("The network '{0}' was successfully created.", networkName);            
        }
    }
}
