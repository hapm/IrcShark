// <copyright file="HelpCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the HelpCommand class.</summary>

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
    using System.Text;

    /// <summary>
    /// The HelpCommand displays a list of all available commands to the console.
    /// </summary>
    public class HelpCommand : TerminalCommand
    {        
        /// <summary>
        /// Initializes a new instance of the HelpCommand class.
        /// </summary>
        /// <param name="extension">
        /// The instance of the TerminalExtension, the help should be shown for.
        /// </param>
        public HelpCommand(TerminalExtension extension)
            : base("help", extension)
        {
        }

        /// <summary>
        /// By executing the HelpCommand, the help of all added 
        /// <see cref="TerminalCommand">TerminalCommands</see> will be executed.
        /// </summary>
        /// <param name="paramList">
        /// A list of parameters the user typed.
        /// </param>
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length == 0) 
            {
                StringBuilder line = null;
                Terminal.WriteLine("Listing all available commands:");
                foreach (TerminalCommand cmd in Terminal.Commands)
                {
                    if (line == null)
                        line = new StringBuilder(cmd.CommandName);
                    else
                    {
                        line.Append(' ');
                        line.Append(cmd.CommandName);
                        if (line.Length > 40)
                        {
                            Terminal.WriteLine(line.ToString());
                            line = null;
                        }
                    }
                }
            }
        }
    }
}
