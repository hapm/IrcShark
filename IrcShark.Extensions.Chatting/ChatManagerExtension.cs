using System.IO;
using System.Xml.Serialization;

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
namespace IrcShark.Extensions.Chatting
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    
    using IrcShark.Chatting;
    using IrcShark.Extensions;

    /// <summary>
    /// The ChatManagerExtension allows to manage connections to chat servers
    /// with different protocols.
    /// </summary>
    [GuidAttribute("85a0b0ad-6015-41e5-80aa-ccb6c0cad044")]
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
        /// Saves a list of networks, that couldn't be loaded because of missing protocols.
        /// </summary>
        private List<NetworkSettings> unloadedNetworks;
        
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
            unloadedNetworks = new List<NetworkSettings>();
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
        /// Gets an array of all registered protocols.
        /// </summary>
        /// <value>
        /// The array of all registred protocols.
        /// </value>
        public ProtocolExtension[] Protocols
        {
            get 
            {
                ProtocolExtension[] protocols = new ProtocolExtension[registredProtocols.Count];
                registredProtocols.CopyTo(protocols);
                return protocols;
            }
        }
        
        /// <summary>
        /// Gets the ProtocolExtension for the protocol with the given name.
        /// </summary>
        /// <param name="name">The name of the protocol.</param>
        /// <returns>The ProtocolExtension instance or null if there is no extension for the given protocol.</returns>
        public ProtocolExtension GetProtocol(string name)
        {
            foreach (ProtocolExtension ext in registredProtocols)
            {
                if (ext.Protocol.Name.ToLower().Equals(name.ToLower()))
                {
                    return ext;
                }
            }
            
            return null;
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
            List<NetworkSettings> loaded = new List<NetworkSettings>();
            foreach (NetworkSettings setting in unloadedNetworks)
            {
                if (setting.Protocol.Equals(prot.Protocol.Name))
                {
                    Networks.Add(prot.LoadNetwork(setting));
                    loaded.Add(setting);
                }
            }
            
            foreach (NetworkSettings setting in loaded)
            {
                unloadedNetworks.Remove(setting);
            }
        }
        
        /// <summary>
        /// Starts the ChatManagerExtension.
        /// </summary>
        public override void Start()
        {
            LoadSettings();
            running = true;
        }
        
        /// <summary>
        /// Stops the ChatManagerExtension.
        /// </summary>
        public override void Stop()
        {
            running = false;
            SaveSettings();
        }
        
        /// <summary>
        /// Saves all network settings.
        /// </summary>
        public void SaveSettings()
        {
            TextWriter writer = new StreamWriter(Path.Combine(Context.SettingPath, "networks.xml"));
            List<NetworkSettings> settings = new List<NetworkSettings>();
            foreach (INetwork network in Networks)
            {
                ProtocolExtension ext = GetProtocol(network.Protocol.Name);
                if (ext == null)
                {
                    Context.Application.Log.Log(new LogMessage("Chatting", 1234, LogLevel.Error, "Couldn't save network '{0}', there was no protocol found to handle it."));
                }
                else
                {
                    settings.Add(ext.SaveNetwork(network));
                }
            }
            
            XmlSerializer serializer = new XmlSerializer(settings.GetType(), new XmlRootAttribute("networks"));
            serializer.Serialize(writer, settings);
        }
        
        /// <summary>
        /// Loads all network settings.
        /// </summary>
        public void LoadSettings()
        {
            string file = Path.Combine(Context.SettingPath, "networks.xml");
            if (!File.Exists(file))
            {
                return;
            }
            
            TextReader reader = new StreamReader(Path.Combine(Context.SettingPath, "networks.xml"));
            List<NetworkSettings> settings;
            XmlSerializer serializer = new XmlSerializer(typeof(List<NetworkSettings>), new XmlRootAttribute("networks"));
            settings = serializer.Deserialize(reader) as List<NetworkSettings>;
            foreach (NetworkSettings setting in settings)
            {
                ProtocolExtension ext = GetProtocol(setting.Protocol);
                
                if (ext != null)
                {
                    Networks.Add(ext.LoadNetwork(setting));
                }
                else
                {
                    unloadedNetworks.Add(setting);
                }
            }
        }
    }
}
