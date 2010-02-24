// <copyright file="ServerCommand.cs" company="IrcShark Team">
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
    using IrcShark.Chatting;
    using IrcShark.Extensions.Chatting;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The ServerCommand allows to configure the servers in the ChatManagerExtension by using the TerminalExtension.
    /// </summary>
    public class ServerCommand : TerminalCommand
    {
        /// <summary>
        /// Saves the reference to the ChatManagerExtension instance.
        /// </summary>
        private ChatManagerExtension chatting;
        
        /// <summary>
        /// Initializes a new instance of the ServerCommand class.
        /// </summary>
        /// <param name="chatting">
        /// The reference to the ChatManagerExtension.
        /// </param>
        /// <param name="terminal">
        /// The reference to the TerminalExtension.
        /// </param>
        public ServerCommand(ChatManagerExtension chatting, TerminalExtension terminal) : base("server", terminal)
        {
            this.chatting = chatting;
        }
        
        /// <summary>
        /// Executes the command with the given parameters.
        /// </summary>
        /// <param name="paramList">The liost of parameters for this command.</param>
        public override void Execute(params string[] paramList)
        {
        }
    }
}
