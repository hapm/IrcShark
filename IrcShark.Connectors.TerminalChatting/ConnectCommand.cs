// <copyright file="ConnectCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ConnectCommand class.</summary>

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

    /// <summary>
    /// The ConnectCommand to create a new connection on the terminal.
    /// </summary>
    public class ConnectCommand : TerminalCommand
    {
        /// <summary>
        /// Saves the reference to rhe TerminalChattingConntector.
        /// </summary>
        private TerminalChattingConnector con;
        
        /// <summary>
        /// Initializes a new instance of the ConnectCommand class.
        /// </summary>
        /// <param name="connector">
        /// A reference to the connector instance.
        /// </param>
        public ConnectCommand(TerminalChattingConnector connector) : base("connect", connector.Terminal)
        {
            this.con = connector;
        }
        
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="paramList">The parameters to execute the command.</param>
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length < 1)
            {
                con.Terminal.WriteLine("Please specify a flag.");
                return;
            }
            
            switch (paramList[0])
            {
                case "-l":
                    ListConnections(paramList);
                    break;
                    
                case "-o":
                    OpenConnection(paramList);
                    break;
                    
                case "-c":
                    CloseConnection(paramList);
                    break;
            }
        }
        
        /// <summary>
        /// Lists all open connections.
        /// </summary>
        /// <param name="paramList">The parameters for this command.</param>
        private void ListConnections(string[] paramList)
        {
            if (con.Chatting.Connections.Count == 0)
            {
                con.Terminal.WriteLine("There are no open connections at the moment.");
                return;
            }
            
            con.Terminal.WriteLine("Listing open connections:");
            for (int i = 0; i < con.Chatting.Connections.Count; i++)
            {
                IConnection c = con.Chatting.Connections[i];
                con.Terminal.WriteLine("{0}. {1}", i, c.Server.Network.Name);
            }
        }
        
        /// <summary>
        /// Opens a new connection to the given network.
        /// </summary>
        /// <param name="paramList">
        /// The network to open the connection to.
        /// </param>
        private void OpenConnection(string[] paramList)
        {
            if (paramList.Length < 2)
            {
                con.Terminal.WriteLine("Please specify the network to connect to.");
                return;
            }
            
            INetwork network = GetNetwork(paramList[1]);
            if (network == null)
            {
                return;
            }
            
            if (network.ServerCount == 0)
            {
                con.Terminal.WriteLine("The network '{0}' doesn't have any configured servers, please configure one before trying to connect.", network.Name);
            }
            
            IConnection connection = network.CreateConnection();
            connection.Nickname = "IS|Test";
            connection.UserName = "Test";
            con.Chatting.Connections.Add(connection);
            connection.Open();
        }
        
        /// <summary>
        /// Closes a given conenction.
        /// </summary>
        /// <param name="paramList">The parameters for the command.</param>
        private void CloseConnection(string[] paramList)
        {
            int connectNr;
            IConnection connection;
            if (paramList.Length < 2 || paramList[1] == null)
            {
                con.Terminal.WriteLine("Please specify a connection number to close");
                return;
            }            
            
            if (!int.TryParse(paramList[1], out connectNr))
            {
                con.Terminal.WriteLine("The given connection number '{0}' is not numeric", paramList[1]);
                return;
            }
            
            if (connectNr < 1 || connectNr > con.Chatting.Connections.Count)
            {
                con.Terminal.WriteLine("The given number '{0}' is no valid connection number", connectNr);
                return;
            }
            
            connection = con.Chatting.Connections[connectNr-1];
            connection.Close();
            con.Chatting.Connections.Remove(connection);
            con.Terminal.WriteLine("Connection {0} to server '{1}' closed", connectNr, connection.Server.Network.Name);
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
                    network = con.Chatting.Networks[networkNr-1];
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
    }
}
