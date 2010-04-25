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
namespace IrcShark.Connectors.TerminalChatting
{
    using System;
    using System.Collections.Generic;
    using IrcShark.Chatting;
    using IrcShark.Extensions.Chatting;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// Description of NetworksCommand.
    /// </summary>
    public class NetworksCommand : TerminalCommand
    {
        /// <summary>
        /// Saves the instance of the TerminalChattingConnector.
        /// </summary>        
        private TerminalChattingConnector con;
        
        /// <summary>
        /// Initializes a new instance of the NetworksCommand class.
        /// </summary>
        /// <param name="terminal">The terminal to create the command for.</param>
        public NetworksCommand(TerminalChattingConnector connector) : base("network", connector.Terminal)
        {
            this.con = connector;
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
                    
                case "-d":
                case "--delete":
                    DeleteNetwork(paramList);
                    break;
            }
        }
        
        /// <summary>
        /// Autocompletes networknames on deletion of networks.
        /// </summary>
        /// <param name="call">The current line.</param>
        /// <param name="paramIndex">The parameter where the cursor stand on.</param>
        /// <returns></returns>
        public override string[] AutoComplete(CommandCall call, int paramIndex)
        {
            switch (paramIndex)
            {
                case 1:
                    switch (call.Parameters[0])
                    {
                        case "-d":
                        case "--delete":
                            List<string> completitions = new List<string>();
                            foreach (INetwork n in con.Chatting.Networks)
                            {
                                if (!string.IsNullOrEmpty(call.Parameters[1]) && n.Name.StartsWith(call.Parameters[1]))
                                {
                                    continue;
                                }
                                
                                completitions.Add(n.Name);
                            }
                            
                            if (completitions.Count > 0)
                            {
                                return completitions.ToArray();
                            }
                            
                            break;
                    }
                    
                    break;
            }
            
            return base.AutoComplete(call, paramIndex);
        }
        
        /// <summary>
        /// Shows a list of all networks.
        /// </summary>
        private void ListNetworks()
        {
            int i = 0;
            if (con.Chatting.Networks.Count == 0)
            {            
                Terminal.WriteLine("There are no configured networks. Use network -a to add some.");                
                return;
            }
            
            Terminal.WriteLine("Listing all configured networks:");
            foreach (INetwork network in con.Chatting.Networks)
            {
                i++;
                Terminal.WriteLine("{0}. {1}", i, network.Name);
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
            
            foreach (ProtocolExtension pro in con.Chatting.Protocols)
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
            con.Chatting.Networks.Add(network);
            Terminal.WriteLine("The network '{0}' was successfully created.", networkName);            
        }
        
        /// <summary>
        /// Removes a network from the configuration.
        /// </summary>
        /// <param name="paramList">The parameters of the command.</param>
        private void DeleteNetwork(string[] paramList)
        {
            string networkName;
            int networkNr = 0;
            INetwork toDelete = null;
            if (paramList.Length < 2)
            {
                Terminal.WriteLine("Please specify a network numbor or name.");
                return;
            }
            
            networkName = paramList[1];
            
            if (int.TryParse(networkName, out networkNr)) 
            {
                if (networkNr < 1 || networkNr > con.Chatting.Networks.Count)
                {
                    Terminal.WriteLine("There is no network with the number {0}, type network to get a list of configured networks.", networkNr);
                    return;
                }
                
                toDelete = con.Chatting.Networks[networkNr - 1];
            }
            else
            {
                foreach (INetwork net in con.Chatting.Networks)
                {
                    if (net.Name.Equals(networkName))
                    {
                        toDelete = net;
                        break;
                    }
                }
                
                if (toDelete == null)
                {
                    Terminal.WriteLine("There is no network with the name {0}, type network to get a list of configured networks.", networkName);
                    return;                    
                }
            }
            
            con.Chatting.Networks.Remove(toDelete);
            Terminal.WriteLine("The network {0} was successfully deleted.", toDelete.Name);
        }
    }
}