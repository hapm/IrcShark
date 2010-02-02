// <copyright file="ExitCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExitCommand class.</summary>

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
namespace IrcShark.Extensions.Terminal
{
    using System;

    /// <summary>
    /// The ExitCommand stops the execution of IrcShark and all its extensions.
    /// </summary>
    public class ExitCommand : TerminalCommand
    {
        /// <summary>
        /// Initializes a new instance of the ExitCommand class.
        /// </summary>
        /// <param name="extension">The reference to the TerminalExtension.</param>
        public ExitCommand(TerminalExtension extension)
            : base("exit", extension)
        {
        }
        
        /// <summary>
        /// Executing this command will close IrcShark.
        /// </summary>
        /// <param name="paramList">
        /// A list of parameters the user typed.
        /// </param>
        public override void Execute(params string[] paramList)
        {            
            Terminal.Context.Application.Dispose();
        }
    }
}
