// <copyright file="TerminalCommandAttribute.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the TerminalCommand attribute.</summary>

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
// along with this program.  If not, see <http://www.gnu.org/licenses/>
namespace IrcShark.Extensions.Terminal
{
    using System;
    using Mono.Addins;

    /// <summary>
    /// Description of TerminalCommandAttribute.
    /// </summary>
    public class TerminalCommandAttribute : CustomExtensionAttribute
    {
        /// <summary>
        /// Saves the command name.
        /// </summary>
        private string commandName;
        
        /// <summary>
        /// Initializes a new instance of the TerminalCommandAttribute class.
        /// </summary>
        public TerminalCommandAttribute()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the TerminalCommandAttribute class.
        /// </summary>
        /// <param name="commandName">The command name the command should have.</param>
        public TerminalCommandAttribute([NodeAttribute("Name")] string commandName)
        {
            Name = commandName;
        }
        
        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        [NodeAttribute]
        public string Name {
            get {
                return commandName;
            }
            set {
                commandName = value;
            }
        }
    }
}
