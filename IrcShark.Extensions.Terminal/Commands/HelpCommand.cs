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
namespace IrcShark.Extensions.Terminal.Commands
{
    using System;
    using System.Text;
    
    using Mono.Addins;
    
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The HelpCommand displays a list of all available commands to the console.
    /// </summary>
    [TerminalCommand("help")]
    public class HelpCommand : TerminalCommand
    {
        /// <summary>
        /// By executing the HelpCommand, the help of all added 
        /// <see cref="TerminalCommand">TerminalCommands</see> will be executed.
        /// </summary>
		/// <param name="terminal">
		/// The terminal, the command was called from.
		/// </param>
        /// <param name="paramList">All parameters of the command.</param>
        public override void Execute(ITerminal terminal, params string[] paramList)
        {
            if (paramList.Length == 0) 
            {
                StringBuilder line = null;
                terminal.WriteLine(Translation.Messages.ListingAvailableCommands);
                foreach (TypeExtensionNode<TerminalCommandAttribute> cmdNode in Terminal.CommandNodes)
                {
                    if (line == null)
                    {
                        line = new StringBuilder(cmdNode.Data.Name);
                    }
                    else
                    {
                        line.Append(' ');
                        line.Append(cmdNode.Data.Name);
                    }
                    
                    if (line.Length > 40)
                    {
                        terminal.WriteLine(line.ToString());
                        line = null;
                    }
                }
                
                if (line != null)
                {
                    terminal.WriteLine(line.ToString());
                }
                
                terminal.WriteLine(Translation.Messages.GetCommandDetails);
            } 
            else if (paramList.Length == 1)
            {
                foreach (TypeExtensionNode<TerminalCommandAttribute> cmdNode in Terminal.CommandNodes)
                {
                    if (paramList[0].ToString() == cmdNode.Data.Name) 
                    {
                        ITerminalCommand cmd = cmdNode.CreateInstance() as ITerminalCommand;
                        cmd.Init(Terminal);
                        cmd.Execute(terminal, "-?");
                        return;
                    }
                }
                
              terminal.WriteLine(Translation.Messages.UnknowCommand, paramList[0].ToString());    
            }
        }
    }
}
