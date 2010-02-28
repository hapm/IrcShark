// <copyright file="SupportedProtocolsCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the SupportedProtocolsCommand class.</summary>

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
    using System.Text;
    using IrcShark.Extensions.Terminal;
    using IrcShark.Extensions.Chatting;

    /// <summary>
    /// Allows to display all currently installed chat protocols in the terminal.
    /// </summary>
    /// <remarks>
    /// This command doesn't do much. It simply shows a list of all registered protocols.
    /// </remarks>
    public class SupportedProtocolsCommand : TerminalCommand
    {
        /// <summary>
        /// Saves the reference to the TerminalChattingConnector instance.
        /// </summary>
        private TerminalChattingConnector con;
        
        /// <summary>
        /// Initializes a new instance of the SupportedProtocolsCommand class.
        /// </summary>
        /// <param name="terminal"></param>
        public SupportedProtocolsCommand(TerminalChattingConnector connector) : base("protocols", connector.Terminal)
        {
            this.con = connector;
        }
        
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="paramList">The parameters for this command.</param>
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length == 1 && paramList[0] == "-?")
            {
                Terminal.WriteLine(Translation.Messages.ListingInstalledProtocolsHelp);
                return;
            }
            StringBuilder line = null;
            Terminal.WriteLine(Translation.Messages.ListingInstalledProtocols);
            foreach (ProtocolExtension protocol in con.Chatting.Protocols)
            {
                if (line == null)
                {
                    line = new StringBuilder(protocol.Protocol.Name);
                }
                else
                {
                    line.Append(' ');
                    line.Append(protocol.Protocol.Name);
                }
                
                if (line.Length > 40)
                {
                    Terminal.WriteLine(line.ToString());
                    line = null;
                }
            }
                
            if (line != null)
            {
                Terminal.WriteLine(line.ToString());
            }
        }
    }
}
