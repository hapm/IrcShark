// <copyright file="${FILENAME}" company="IrcShark Team">
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
    public class IrcProtocolExtension : ProtocolExtension
    {
        /// <summary>
        /// Saves the regular expression to parse an irc address.
        /// </summary>
        private static Regex ircAddressRegex = new Regex(@"^(?:irc://)?([^:/]+)(?::([\d]+))?/?");
        
        /// <summary>
        /// Saves the log channel identifier of the IrcProtocolExtension.
        /// </summary>
        private const string LogChannel = "IRC";
        
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
        /// Starts the IrcProtocolExtension
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
                info = Context.Application.Extensions["IrcSHark.Extensions.Chatting.ChatManagerExtension"];
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
            
            IrcNetwork result = new IrcNetwork(settings.Name);
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
        
        public override NetworkSettings SaveNetwork(IrcShark.Chatting.INetwork network)
        {
            throw new NotImplementedException();
        }
    }
}