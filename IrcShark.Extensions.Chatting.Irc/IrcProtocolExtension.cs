using System.Diagnostics;
// <copyright file="IrcProtocolExtension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IrcProtocolExtension class.</summary>

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
namespace IrcShark.Extensions.Chatting.Irc
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml;
    
    using IrcShark.Chatting;
    using IrcShark.Chatting.Irc;
    using IrcShark.Extensions;

    /// <summary>
    /// The IrcProtocolExtension allows the ChatManagerExtension to manage irc 
    /// protocol connections.
    /// </summary>
    [System.Runtime.InteropServices.Guid("1c0d853e-3e84-4451-a384-40a663669a9e")]
    [Mono.Addins.Extension]
    public class IrcProtocolExtension : IProtocolExtension
    {
    	private const string ns = "http://www.ircshark.net/2011/protocols/irc";
    	
        /// <summary>
        /// Saves the log channel identifier of the IrcProtocolExtension.
        /// </summary>
        private const string LogChannel = "IRC";
        
        /// <summary>
        /// Saves the regular expression to parse an irc address.
        /// </summary>
        private static Regex ircAddressRegex = new Regex(@"^(?:irc://)?([^:/]+)(?::([\d]+))?/?");
        
        /// <summary>
        /// Get the IrcProtocol instance.
        /// </summary>
        /// <value>
        /// The IrcProtocol instace.
        /// </value>
        public IrcShark.Chatting.IProtocol Protocol 
        {
            get 
            {
                return IrcProtocol.GetInstance();
            }
        }
        
        /// <summary>
        /// Loads the networks setinngs into a new IrcNetwork instance.
        /// </summary>
        /// <param name="settings">The settings to load.</param>
        /// <returns>The IrcNetwork instance as an INetwork.</returns>
        /// <exception cref="UnsupportedProtocolExteption">
        /// An UnsupportedProtocolExteption is thrown, if the given settings object is for
        /// another protocol.
        /// </exception>
        public IrcShark.Chatting.INetwork LoadNetwork(NetworkSettings settings)
        {
            if (!settings.Protocol.Equals(Protocol.Name))
            {
                throw new UnsupportedProtocolException();
            }
            
            IrcNetwork result = new IrcNetwork(IrcProtocol.GetInstance(), settings.Name);
            foreach (ServerSettings server in settings.Servers) 
            {
                IrcServerEndPoint ircsrv = result.AddServer(server.Name, server.Address);
                if (server.Parameters.ContainsKey("password"))
                {
                    ircsrv.Password = server.Parameters["password"];
                }
            }
            
            return result;
        }
        
        /// <summary>
        /// Saves an IrcNetwork to a NetworkSettings instance.
        /// </summary>
        /// <param name="network">The IrcNetwork to save.</param>
        /// <returns>The generated NetworkSettings instance.</returns>
        public NetworkSettings SaveNetwork(IrcShark.Chatting.INetwork network)
        {
            IrcNetwork net = network as IrcNetwork;
            if (net == null)
            {
                throw new ArgumentException("The given network is not an irc network");
            }
            
            NetworkSettings settings = new NetworkSettings();
            settings.Protocol = net.Protocol.Name;
            settings.Name = net.Name;
            
            foreach (IrcServerEndPoint server in net)
            {
                ServerSettings servSet = new ServerSettings();
                servSet.Name = server.Name;
                servSet.Address = string.Format("{0}:{1}", server.Address, server.Port);
                if (!string.IsNullOrEmpty(server.Password))
                {
                    servSet.Parameters.Add("Password", server.Password);
                }
                
                settings.Servers.Add(servSet);
            }
            
            return settings;
        }
        
        /// <summary>
        /// Saves all networks of the given collection belonging to this protocol.
        /// </summary>
        /// <param name="networks">The networks to save.</param>
        /// <param name="writer">The writer to save to.</param>
        public void SerializeNetworks(IEnumerable<INetwork> networks, XmlWriter writer)
        {
        	bool first = true;
        	
        	foreach (INetwork network in networks) 
        	{
        		if (!(network is IrcNetwork))
        			continue;
        		
        		if (first)
        		{
        			first = false;
        			writer.WriteStartElement("networks");
        			writer.WriteAttributeString("protocol", Protocol.Name);
        			writer.WriteAttributeString("ircp", "xmlns", ns);
        		}
        		
        		SerializeNetwork(network, writer);
        	}
        	
        	if (!first)
        	{
        		writer.WriteEndElement();
        	}
        }
    	
        /// <summary>
        /// Saves a network to the writer.
        /// </summary>
        /// <param name="network"></param>
        /// <param name="writer"></param>
		public void SerializeNetwork(INetwork network, XmlWriter writer)
		{
			writer.WriteStartElement("ircnetwork", ns);
			if (!(network is IrcNetwork))
			{
				throw new ArgumentException("Given network is no IrcNetwork.", "network");
			}
			
			IrcNetwork ircNet = network as IrcNetwork;
			
			writer.WriteAttributeString("name", ircNet.Name);
			foreach (IrcServerEndPoint server in ircNet)
			{
				writer.WriteStartElement("ircserver", ns);
				writer.WriteAttributeString("name", ns, server.Name);
				writer.WriteAttributeString("address", ns, string.Format("irc://{0}:{1}", server.Address, server.Port));
				writer.WriteAttributeString("identd", ns, server.IsIdentDRequired ? "1" : "0");
				writer.WriteEndElement();
			}
			
			writer.WriteEndElement();
		}
    	
		public IEnumerable<INetwork> DeserializeNetworks(XmlReader reader)
		{
			string protocolName;
			List<INetwork> result;
			if (reader.NodeType != XmlNodeType.Element 
			    || !reader.Name.Equals("networks"))
				throw new InvalidOperationException("The given reader is not at a networks tag start node.");
			
			if (!reader.MoveToAttribute("protocol"))
				throw new InvalidOperationException("Missing attribute protocol in networks node.");
			
			protocolName = reader.ReadContentAsString();
			if (!Protocol.Name.Equals(protocolName))
				throw new InvalidOperationException(string.Format("The protocol \"{0}\" is not supported by this extension.", protocolName));
			
			result = new List<INetwork>();
			while (reader.NodeType != XmlNodeType.EndElement || !reader.LocalName.Equals("networks"))
			{
				reader.Read();
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						try {
							result.Add(DeserializeNetwork(reader));							
						} catch (InvalidOperationException) {
							reader.Skip();
						}
						
						break;
				}
			}
			
			return result;
		}
		
		public INetwork DeserializeNetwork(XmlReader reader)
		{
			IrcNetwork result;
			string address;
			bool isEmpty;
			string name;
			IrcServerEndPoint server;
			
			if (!reader.LocalName.Equals("ircnetwork"))
				throw new InvalidOperationException("Given reader is not at an ircnetwork start node.");
			
			isEmpty = reader.IsEmptyElement;
			
			if (!reader.MoveToAttribute("name"))
				throw new InvalidOperationException("The ircnetwork doesn't have a name attribute.");
			
			result = new IrcNetwork(Protocol as IrcProtocol, reader.ReadContentAsString());
			
			if (!isEmpty)
			{
				while (reader.NodeType != XmlNodeType.EndElement || !reader.LocalName.Equals("ircnetwork"))
				{
					reader.Read();
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							try
							{
								if (reader.LocalName.Equals("ircserver"))
								{
									if (!reader.MoveToAttribute("name", ns))
										throw new InvalidOperationException("Current server tag has no name.");
									
									name = reader.ReadContentAsString();
									if (!reader.MoveToAttribute("address", ns))
										throw new InvalidOperationException("Current server tag has no address.");
									
									address = reader.ReadContentAsString();
									server = result.AddServer(name, address);
									
									if (reader.MoveToAttribute("identd", ns))
										server.IsIdentDRequired = reader.ReadContentAsBoolean();
								}
							}
							catch (Exception ex)
							{
								reader.Skip();
							}
							
							break;
					}
				}
			}
			
			return result;
		}
    }
}