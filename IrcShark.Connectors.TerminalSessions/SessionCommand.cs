// <copyright file="SessionCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the SessionCommand class.</summary>

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
namespace IrcShark.Connectors.TerminalSessions
{
    using System;
    using System.Collections.Generic;
    using IrcShark.Extensions.Terminal;
    using IrcShark.Extensions.Sessions;

    /// <summary>
    /// The SessionCommand allows to show informations about running sessions.
    /// </summary>
    [TerminalCommand("session")]
    public class SessionCommand : TerminalCommand
    {
        private SessionManagementExtension sessions;
        
        /// <summary>
        /// Initializes the RawCommand.
        /// </summary>
        /// <param name="terminal">The terminal to create the command for.</param>
        public override void Init(TerminalExtension terminal)
        {
            base.Init(terminal);
            this.sessions = Terminal.Context.Application.Extensions["IrcShark.Extensions.Sessions.SessionManagerExtension"] as SessionManagementExtension;
        } 
        
        public override void Execute(ITerminal terminal, params string[] paramList)
        {
            throw new NotImplementedException();
        }
        
    }
}