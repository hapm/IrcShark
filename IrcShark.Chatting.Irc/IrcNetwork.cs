// <copyright file="IrcNetwork.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IrcNetwork class.</summary>

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
namespace IrcShark.Chatting.Irc
{
    using System;
    using System.Collections.Generic;
    using IrcShark.Chatting;

    /// <summary>
    /// Represents a network definition for the irc protocol.
    /// </summary>
    public class IrcNetwork : INetwork
    {
        /// <summary>
        /// Saves the name of the network.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves a list of servers for this network.
        /// </summary>
        private List<IrcServerEndPoint> servers;
        
        /// <summary>
        /// Initializes a new instance of the IrcNetwork class.
        /// </summary>
        /// <param name="name">
        /// The network name.
        /// </param>
        public IrcNetwork(string name)
        {
            this.name = name;
            servers = new List<IrcServerEndPoint>();
        }
        
        /// <summary>
        /// Gets or sets the name of the network.
        /// </summary>
        /// <value>The name of the string.</value>
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }
        
        /// <summary>
        /// Gets the number of servers added to this network.
        /// </summary>
        /// <value>
        /// The number of servers added to this network.
        /// </value>
        public int ServerCount
        {
            get { return servers.Count; }
        }
        
        /// <summary>
        /// Gets the server configuration at the given index.
        /// </summary>
        /// <param name="index">The index of the server.</param>
        /// <value>The IServer instance at the given index.</value>
        IServer INetwork.this[int index] 
        {
            get { return this[index]; }
        }
        
        /// <summary>
        /// Gets the irc server configuration at the given index.
        /// </summary>
        /// <param name="index">The index of the server.</param>
        /// <value>The IrcServerEndPoint instance at the given index.</value>
        public IrcServerEndPoint this[int index] 
        {
            get { return servers[index]; }
        }
        
        /// <summary>
        /// Gets the server configuration with the given name.
        /// </summary>
        /// <param name="name">The name of the server.</param>
        /// <value>The IServer instance.</value>
        IServer INetwork.this[string name] 
        {
            get { return this[name]; }
        }
        
        /// <summary>
        /// Gets the server configuration with the given name.
        /// </summary>
        /// <param name="name">The name of the server.</param>
        /// <value>The IrcServerEndPoint instance.</value>
        public IrcServerEndPoint this[string name] 
        {
            get 
            {
                foreach (IrcServerEndPoint srv in servers) 
                {
                    if (srv.Name == name)
                    {
                        return srv;
                    }
                }
                
                return null;
            }
        }
        
        /// <summary>
        /// Adds a new server configuration to the network configuration.
        /// </summary>
        /// <param name="name">The name of the server configuration.</param>
        /// <param name="address">The network address as a string.</param>
        /// <returns>The new IServer instance for the implemented protocol.</returns>
        IServer INetwork.AddServer(string name, string address)
        {
            return AddServer(name, address);
        }
        
        /// <summary>
        /// Adds a new irc server configuration to the network configuration.
        /// </summary>
        /// <param name="name">The name of the server configuration.</param>
        /// <param name="address">The network address as a string.</param>
        /// <returns>
        /// The new IrcServerEndPoint instance for the implemented protocol.
        /// </returns>
        public IrcServerEndPoint AddServer(string name, string address)
        {
            IrcServerEndPoint newServer = null;
            if (this[name] != null)
            {
                throw new ArgumentException("The given name already exists in the list of servers", "name");
            }
            
            if (address.Contains(":"))
            {
                string addr;
                string strPort;
                int port;
                addr = address.Substring(0, address.IndexOf(':'));
                strPort = address.Substring(address.IndexOf(':') + 1);
                if (int.TryParse(strPort, out port))
                {
                    newServer = new IrcServerEndPoint(name, addr, port);
                }
                else
                {
                    newServer = new IrcServerEndPoint(name, addr, 6667);
                }
            }
            else 
            {
                newServer = new IrcServerEndPoint(name, address, 6669);
            }
            
            servers.Add(newServer);
            return newServer;
        }
        
        /// <summary>
        /// Removes the server configuration with the given name.
        /// </summary>
        /// <param name="name">The name of the server to remove.</param>
        /// <returns>If the server was found and removed true, false otherwise.</returns>
        public bool RemoveServer(string name)
        {
            foreach (IrcServerEndPoint server in servers)
            {
                if (server.Name == name)
                {
                    servers.Remove(server);
                    return true;
                }
            }
            
            return false;
        }
        
        /// <summary>
        /// Removes the server configuration at the given index.
        /// </summary>
        /// <param name="index">
        /// The index of the server configuration to remove.
        /// </param>
        public void RemoveServer(int index)
        {
            servers.RemoveAt(index);
        }
        
        /// <summary>
        /// Gets a generic enumerator for all servers in this network.
        /// </summary>
        /// <returns>The enumerator instance.</returns>
        public System.Collections.Generic.IEnumerator<IServer> GetEnumerator()
        {
            return ((IEnumerable<IServer>)servers).GetEnumerator();
        }
        
        /// <summary>
        /// Gets the enumerator for all servers in this network.
        /// </summary>
        /// <returns>The enumerator instance.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)servers).GetEnumerator();
        }
    }
}
