// <copyright file="TerminalCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the TerminalCommand class.</summary>

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
namespace IrcSharkTerminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// By inheriting from the TerminalCommand class, you can write your own 
    /// commands for the TerminalExtension.
    /// </summary>
    public abstract class TerminalCommand
    {
        /// <summary>
        /// Saves the name of the command.
        /// </summary>
        private string commandName;
        
        /// <summary>
        /// Saves a reference to the TerminalExtension instance.
        /// </summary>
        private TerminalExtension extension;
        
        /// <summary>
        /// Initializes a new instance of the TerminalCommand class.
        /// </summary>
        /// <param name="command">
        /// The name of the command as typed on the terminal.
        /// </param>
        /// <param name="extension">
        /// A reference to the ExtensionTerminal instance, this command is 
        /// created for.
        /// </param>
        public TerminalCommand(string command, TerminalExtension extension)
        {
            commandName = command;
            this.extension = extension;
        }
        
        /// <summary>
        /// Gets the command name of this command.
        /// </summary>
        /// <value>
        /// The command name as a string that is used to call this command.
        /// </value>
        public string CommandName
        {
            get { return commandName; }
        }
        
        /// <summary>
        /// Gets the TerminalExtension, this command works on.
        /// </summary>
        /// <value>
        /// The TerminalExtension instance, this command was created for.
        /// </value>
        public TerminalExtension Terminal 
        {
            get { return extension; }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="paramList">
        /// A list of parameters the user typed.
        /// </param>
        /// <remarks>
        /// Implement this method with the behavior of your command.
        /// </remarks>
        public abstract void Execute(params string[] paramList);
    }
}
