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
    
    using IrcShark.Chatting.Irc;
    using IrcShark.Extensions;

    /// <summary>
    /// The IrcProtocolExtension allows the ChatManagerExtension to manage irc 
    /// protocol connections.
    /// </summary>
    [System.Runtime.InteropServices.Guid("1c0d853e-3e84-4451-a384-40a663669a9e")]
    public class IrcProtocolExtension : ProtocolExtension
    {
        /// <summary>
        /// Saves the log channel identifier of the IrcProtocolExtension.
        /// </summary>
        private const string LogChannel = "IRC";
        
        /// <summary>
        /// Saves the regular expression to parse an irc address.
        /// </summary>
        private static Regex ircAddressRegex = new Regex(@"^(?:irc://)?([^:/]+)(?::([\d]+))?/?");
        
        /// <summary>
        /// Saves the IrcProtocol instance.
        /// </summary>
        private IrcProtocol protocol;
        
        /// <summary>
        /// Initializes a new instance of the IrcProtocolExtension class.
        /// </summary>
        /// <param name="context"></param>
        public IrcProtocolExtension(ExtensionContext context) : base(context)
        {
            protocol = IrcProtocol.GetInstance();
        }
        
        /// <summary>
        /// Get the IrcProtocol instance.
        /// </summary>
        /// <value>
        /// The IrcProtocol instace.
        /// </value>
        public override IrcShark.Chatting.IProtocol Protocol 
        {
            get 
            {
                return protocol;
            }
        }
        
        /// <summary>
        /// Starts the IrcProtocolExtension.
        /// </summary>
        /// <remarks>
        /// The extension registers the irc protocol with the ChatManagerExtension.
        /// </remarks>
        public override void Start()
        {
            ExtensionInfo info;
            ChatManagerExtension extension;
            try
            {
                info = Context.Application.Extensions["IrcShark.Extensions.Chatting.ChatManagerExtension"];
                extension = (ChatManagerExtension)Context.Application.Extensions[info];
            }
            catch (IndexOutOfRangeException)
            {
                Context.Application.Log.Log(new LogMessage("IRC", 1234, LogLevel.Error, "ChatManagerExtension isn't loaded!"));
                return;
            }
            catch (InvalidCastException)
            {
                Context.Application.Log.Log(new LogMessage("IRC", 1234, LogLevel.Error, "ChatManagerExtension has the wrong version!"));
                return;
            }
            
            extension.RegisterProtocol(this);
        }

        /// <summary>
        /// Stops the IrcProtocolExtension.
        /// </summary>
        public override void Stop()
        {
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
        public override IrcShark.Chatting.INetwork LoadNetwork(NetworkSettings settings)
        {
            if (!settings.Protocol.Equals(Protocol.Name))
            {
                throw new UnsupportedProtocolException();
            }
            
            IrcNetwork result = new IrcNetwork(protocol, settings.Name);
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
        public override NetworkSettings SaveNetwork(IrcShark.Chatting.INetwork network)
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
    }
}