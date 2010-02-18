// <copyright file="NetworksCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the NetworksCommand class.</summary>

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
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// Description of NetworksCommand.
    /// </summary>
    public class NetworksCommand : TerminalCommand
    {
        /// <summary>
        /// Initializes a new instance of the NetworksCommand class.
        /// </summary>
        /// <param name="terminal">The terminal to create the command for.</param>
        public NetworksCommand(TerminalExtension terminal) : base("network", terminal)
        {
        }
        
        /// <summary>
        /// Executes the networks command.
        /// </summary>
        /// <param name="paramList">A list of parameters.</param>
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length == 0)
            {
                ListNetworks();
            }
        }
        
        /// <summary>
        /// Shows a list of all networks.
        /// </summary>
        private void ListNetworks()
        {
            Terminal.WriteLine("Showing the networks here");
        }
    }
}
