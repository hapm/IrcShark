﻿// <copyright file="TerminalChattingConnector.cs" company="IrcShark Team">
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
namespace IrcShark.Connectors.TerminalChatting
{
    using System;
    using IrcShark.Extensions;
    using IrcShark.Extensions.Chatting;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The TerminalChattingConnector connects the TerminalExtension and the ChatManagerExtension.
    /// </summary>
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
            ExtensionInfo chattingInfo = Context.Application.Extensions["IrcShark.Extensions.Chatting"];
            ExtensionInfo terminalInfo = Context.Application.Extensions["IrcShark.Extensions.Terminal"];
            chatting = Context.Application.Extensions[chattingInfo] as ChatManagerExtension;
            terminal = Context.Application.Extensions[terminalInfo] as TerminalExtension;
            terminal.Commands.Add(new NetworksCommand(terminal));
        }
        
        /// <summary>
        /// Stops the connector.
        /// </summary>
        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}