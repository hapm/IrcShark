// <copyright file="ServerCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ServerCommand class.</summary>

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
    using IrcShark.Chatting;
    using IrcShark.Extensions.Chatting;
    using IrcShark.Extensions.Terminal;
    using IrcShark.Extensions.Terminal.Translation;

    /// <summary>
    /// The ServerCommand allows to configure the servers in the ChatManagerExtension by using the TerminalExtension.
    /// </summary>
    [TerminalCommand("server")]
    public class ServerCommand : TerminalCommand
    {
        /// <summary>
        /// Saves the reference to the TerminalChattingConnector instance.
        /// </summary>
        private TerminalChattingConnector con;
        
        /// <summary>
        /// Initializes the ServerCommand.
        /// </summary>
        /// <param name="terminal">The terminal to create the command for.</param>
        public override void Init(TerminalExtension terminal)
        {
            base.Init(terminal);
            this.con = Terminal.Context.Application.Extensions.GetExtension("TerminalChattingConnector") as TerminalChattingConnector;
        } 
        
        /// <summary>
        /// Executes the command with the given parameters.
        /// </summary>
        /// <param name="paramList">The liost of parameters for this command.</param>
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length == 0)
            {
                Terminal.WriteLine(PublicMessages.PleaseSpecifyFlag);
                return;
            }
            
            switch (paramList[0])
            {
                case "-l":
                    ListServers(paramList);
                    break;
                
                case "-a":
                    AddServer(paramList);
                    break;
                    
                case "-r":
                    RemoveServer(paramList);
                    break;
                    
                default:
                    Terminal.WriteLine(PublicMessages.UnknownFlag, paramList[0]);
                    break;
            }
        }
        
        /// <summary>
        /// Gets the network instance for the given identication string.
        /// </summary>
        /// <param name="ident">The identication string can be the network name or a network number.</param>
        /// <returns>The network instance or null if the network doesn't exist.</returns>
        private INetwork GetNetwork(string ident)
        {
            INetwork network = null;
            int networkNr;
            if (int.TryParse(ident, out networkNr))
            {
                if (networkNr < 1 || networkNr > con.Chatting.Networks.Count)
                {
                    Terminal.WriteLine(string.Format("There is no network with the number {0}.", ident));   
                }
                else
                {
                    network = con.Chatting.Networks[networkNr - 1];
                }
            }
            else
            {
                foreach (INetwork net in con.Chatting.Networks)
                {
                    if (net.Name.Equals(ident))
                    {
                        network = net;
                        break;
                    }
                }
                
                if (network == null)
                {
                    Terminal.WriteLine(string.Format("There is no network with the name '{0}'.", ident));
                }
            }
            
            return network;
        }
        
        /// <summary>
        /// Gets the server instance for the given identication string.
        /// </summary>
        /// <param name="network">The network to search in.</param>
        /// <param name="ident">The identication string can be the network name or a network number.</param>
        /// <returns>The network instance or null if the network doesn't exist.</returns>
        private IServer GetServer(INetwork network, string ident)
        {
            IServer server = null;
            int serverNr;
            if (int.TryParse(ident, out serverNr))
            {
                if (serverNr < 1 || serverNr > network.ServerCount)
                {
                    Terminal.WriteLine(string.Format("There is no server with the number {0}.", ident));   
                }
                else
                {
                    server = network[serverNr - 1];
                }
            }
            else
            {
                foreach (IServer srv in network)
                {
                    if (srv.Name.Equals(ident))
                    {
                        server = srv;
                        break;
                    }
                }
                
                if (server == null)
                {
                    Terminal.WriteLine(string.Format("There is no server with the name '{0}' in the network '{1}'.", ident, network.Name));
                }
            }
            
            return server;            
        }
        
        /// <summary>
        /// Shows a list of servers for a given network.
        /// </summary>
        /// <param name="paramList">The list of parameters for this command.</param>
        private void ListServers(string[] paramList)
        {
            if (paramList.Length < 2)
            {
                Terminal.WriteLine("Please specify a networkname or network number you want to see the serverlist for.");
                return;
            }
            
            INetwork network = GetNetwork(paramList[1]);
            if (network == null)
            {
                return;
            }
            
            if (network.ServerCount == 0)
            {
                Terminal.WriteLine("The network '{0}' doesn't have any servers yet.", network.Name);
                return;
            }
            
            Terminal.WriteLine("Listing servers for network '{0}':", network.Name);
            for (int i = 0; i < network.ServerCount; i++)
            {
                IServer server = network[i];
                Terminal.WriteLine("{0}. {1} ({2})", i + 1, server.Name, server.Address);
            }
        }
        
        /// <summary>
        /// Adds a new server to a network.
        /// </summary>
        /// <param name="paramList">The parameters for the command.</param>
        private void AddServer(string[] paramList)
        {
            INetwork network;
            if (paramList.Length < 2)
            {
                Terminal.WriteLine("Please specify a networkname or network number you want to add the server to.");
                return;
            }
            
            network = GetNetwork(paramList[1]);
            if (network == null)
            {
                return;
            }
            
            if (paramList.Length < 3)
            {
                Terminal.WriteLine("Please specify a servername for the new server.");
                return;
            }
            
            if (paramList.Length < 4)
            {
                Terminal.WriteLine("Please specify server address for the {0} protocol.", network.Protocol.Name);
                return;
            }
            
            try
            {
                network.AddServer(paramList[2], paramList[3]);
            }
            catch (Exception ex)
            {
                Terminal.WriteLine("Couldn't add the server: {0}", ex.Message);
                return;
            }
            
            Terminal.WriteLine("Server '{0}' was successfully added to network '{1}'.", paramList[2], network.Name);
        }
        
        /// <summary>
        /// Removes a server form the server list of the network.
        /// </summary>
        /// <param name="paramList">The parameters for this command.</param>
        private void RemoveServer(string[] paramList)
        {
            INetwork network;
            IServer server;
            if (paramList.Length < 2)
            {
                Terminal.WriteLine("Please specify a networkname or network number you want to add the server to.");
                return;
            }
            
            network = GetNetwork(paramList[1]);
            if (network == null)
            {
                return;
            }
            
            if (paramList.Length < 3)
            {
                Terminal.WriteLine("Please specify a servername or server number you want to remove.");
                return;
            }
            
            server = GetServer(network, paramList[2]);
            if (server == null)
            {
                return;
            }
            
            network.RemoveServer(server.Name);
            Terminal.WriteLine("Server '{0}' was successfully removed from network '{1}'", server.Name, network.Name);
        }
    }
}
