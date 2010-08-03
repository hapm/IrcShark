// <copyright file="RawCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the RawCommand class.</summary>

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
    using IrcShark.Chatting;
    using IrcShark.Chatting.Irc.Extended;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The RawCommand class allows you to send irc raw commands over the terminal.
    /// </summary>
    [TerminalCommand("raw")]
    public class RawCommand : TerminalCommand
    {
        /// <summary>
        /// Saves a reference to the connector.
        /// </summary>
        private TerminalChattingIrcConnector con;
        
        /// <summary>
        /// Initializes the RawCommand.
        /// </summary>
        /// <param name="terminal">The terminal to create the command for.</param>
        public override void Init(TerminalExtension terminal)
        {
            base.Init(terminal);
            this.con = Terminal.Context.Application.Extensions.GetExtension("TerminalChattingIrcConnector") as TerminalChattingIrcConnector;
        } 
        
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="paramList">The parameters for the command.</param>
        public override void Execute(params string[] paramList)
        {
            int connectNr;
            IrcConnection connection;
            if (paramList.Length == 0 || paramList[0] == null)
            {
                con.Terminal.WriteLine("Please specify a connection number and the raw string to send");
                return;
            }            
            
            if (!int.TryParse(paramList[0], out connectNr))
            {
                con.Terminal.WriteLine("The given connection number '{0}' is not numeric", paramList[0]);
                return;
            }
            
            if (connectNr < 1 || connectNr > con.Chatting.Connections.Count)
            {
                con.Terminal.WriteLine("The given number '{0}' is no valid connection number", connectNr);
                return;
            }
            
            connection = con.Chatting.Connections[connectNr - 1] as IrcConnection;
            if (connection == null)
            {
                con.Terminal.WriteLine("The given connection is no irc connection");
                return;
            }
            
            if (paramList.Length < 2 || paramList[1] == null)
            {
                con.Terminal.WriteLine("Please specify a raw command to send.");
                return;
            }
            
            connection.SendLine(paramList[1]);
        }
    }
}
