// <copyright file="TerminalChattingIrcConnector.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the TerminalChattingIrcConnector class.</summary>

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
namespace IrcShark.Connectors.TerminalIrc
{
    using System;
    using System.Runtime.InteropServices;
    using IrcShark.Extensions;
    using IrcShark.Extensions.Chatting;
    using IrcShark.Extensions.Terminal;
    
    /// <summary>
    /// Description of TerminalChattingIrcConnector.
    /// </summary>
    [DependsOn(new string[] { "50562fac-c166-4c0f-8ef4-6d8456add5d9",           "85a0b0ad-6015-41e5-80aa-ccb6c0cad044"}, 
               ClassNames = new string[] { "IrcShark.Extensions.Terminal.TerminalExtension", "IrcShark.Extensions.Chatting.ChatManagerExtension" })]
    [GuidAttribute("18c85c99-28f7-49c6-9f1a-f6ab4274d0fe")]
    public class TerminalChattingIrcConnector : Extension
    { 
        /// <summary>
        /// Saves the reference to the ChatManagerExtension.
        /// </summary>
        private ChatManagerExtension chatting;
        
        /// <summary>
        /// Saves the reference to the TerminalExtension.
        /// </summary>
        private TerminalExtension terminal;
        
        /// <summary>
        /// Initializes a new instance of the TerminalChattingIrcConnector class.
        /// </summary>
        /// <param name="context">The context of the extension to run in.</param>
        public TerminalChattingIrcConnector(ExtensionContext context) : base(context)
        {
        }
        
        /// <summary>
        /// Gets a reference to the ChatManagerExtension.
        /// </summary>
        /// <value>
        /// The reference to the ChatManagerExtension.
        /// </value>
        public ChatManagerExtension Chatting
        {
            get { return chatting; }
        }
        
        /// <summary>
        /// Gets a reference to the TerminalExtension.
        /// </summary>
        /// <value>
        /// The reference to the TerminalExtension.
        /// </value>
        public TerminalExtension Terminal
        {
            get { return terminal; }
        }
        
        /// <summary>
        /// Starts the extension.
        /// </summary>
        public override void Start(ExtensionContext context)
        {
            ExtensionInfo chattingInfo = Context.Application.Extensions["IrcShark.Extensions.Chatting.ChatManagerExtension"];
            ExtensionInfo terminalInfo = Context.Application.Extensions["IrcShark.Extensions.Terminal.TerminalExtension"];
            chatting = Context.Application.Extensions[chattingInfo] as ChatManagerExtension;
            terminal = Context.Application.Extensions[terminalInfo] as TerminalExtension;
        }
        
        /// <summary>
        /// Stops the extension.
        /// </summary>
        public override void Stop()
        {
        }
    }
}
