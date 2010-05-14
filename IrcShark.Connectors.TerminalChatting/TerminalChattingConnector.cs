// <copyright file="TerminalChattingConnector.cs" company="IrcShark Team">
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
namespace IrcShark.Connectors.TerminalChatting
{
    using System;
    using IrcShark.Extensions;
    using IrcShark.Extensions.Chatting;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The TerminalChattingConnector connects the TerminalExtension and the ChatManagerExtension.
    /// </summary>
    [DependsOn(new string[] { "85a0b0ad-6015-41e5-80aa-ccb6c0cad044",              "50562fac-c166-4c0f-8ef4-6d8456add5d9" },
               ClassNames = new string[] { "IrcShark.Extensions.Chatting.ChatManagerExtension", "IrcShark.Extensions.Terminal.TerminalExtension" })]
    [System.Runtime.InteropServices.Guid("c319eb31-313b-4770-824f-18d0110e3a37")]
    public class TerminalChattingConnector : Extension
    {
        /// <summary>
        /// Saves a reference to the ChatManagerExtension.
        /// </summary>
        private ChatManagerExtension chatting;
        
        /// <summary>
        /// Saves a reference to the TerminalExtension.
        /// </summary>
        private TerminalExtension terminal;
        
        /// <summary>
        /// Initializes a new instance of the TerminalChattingExtension.
        /// </summary>
        /// <param name="context">The context the connector runs in.</param>
        public TerminalChattingConnector(ExtensionContext context) : base(context)
        {
            
        }
        
        /// <summary>
        /// Starts the connector.
        /// </summary>
        public override void Start()
        {
            ExtensionInfo chattingInfo = Context.Application.Extensions["IrcShark.Extensions.Chatting.ChatManagerExtension"];
            ExtensionInfo terminalInfo = Context.Application.Extensions["IrcShark.Extensions.Terminal.TerminalExtension"];
            chatting = Context.Application.Extensions[chattingInfo] as ChatManagerExtension;
            terminal = Context.Application.Extensions[terminalInfo] as TerminalExtension;
            terminal.Commands.Add(new NetworksCommand(this));
            terminal.Commands.Add(new ServerCommand(this));
            terminal.Commands.Add(new SupportedProtocolsCommand(this));
            terminal.Commands.Add(new ConnectCommand(this));
        }
        
        /// <summary>
        /// Stops the connector.
        /// </summary>
        public override void Stop()
        {
        }
        
        /// <summary>
        /// Gets the ChatManagerExtension instnace this connector connects to.
        /// </summary>
        /// <value>
        /// The instance of the extension.
        /// </value>
        internal ChatManagerExtension Chatting
        {
            get { return chatting; }
        }
        
        /// <summary>
        /// Gets the TerminalExtension instance this connector connects to.
        /// </summary>
        /// <value>
        /// The instance of the extension.
        /// </value>
        internal TerminalExtension Terminal
        {
            get { return terminal; }
        }
    }
}
