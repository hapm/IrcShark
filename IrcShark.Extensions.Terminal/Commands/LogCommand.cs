// <copyright file="LogCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the LogCommand class.</summary>

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
    using IrcShark;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The LogCommand allows the configuration of all available log handler.
    /// </summary>
    [TerminalCommand("log")]
    public class LogCommand : TerminalCommand
    {        
        /// <summary>
        /// Executes the log command.
        /// </summary>
		/// <param name="terminal">
		/// The terminal, the command was called from.
		/// </param>
        /// <param name="paramList">All parameters of the command.</param>
        public override void Execute(ITerminal terminal, params string[] paramList)
        {
        	//TODO change code to use parameter terminal
            if (paramList == null || paramList.Length == 0) 
            {
                foreach (LogHandlerSetting s in Terminal.Context.Application.Settings.LogSettings)
                {
                    Terminal.Write(string.Format("{0}\n  Target: {1}\n  Filter: ", s.HandlerName, s.Target));
                    bool written = false;
                    if (s.Error)
                    {
                        written = true;
                        Terminal.ForegroundColor = ConsoleColor.Red;
                        Terminal.Write("Error");
                        Terminal.ResetColor();
                    }
                    
                    if (s.Warning)
                    {
                        if (written)
                        {
                            Terminal.Write(", ");
                        }
                        else
                        {
                            written = true;
                        }
                        
                        Terminal.ForegroundColor = ConsoleColor.Yellow;
                        Terminal.Write("Warning");
                        Terminal.ResetColor();
                    }
                    
                    if (s.Information)
                    {
                        if (written)
                        {
                            Terminal.Write(", ");
                        }
                        else
                        {
                            written = true;
                        }
                        
                        Terminal.Write("Information");
                    }
                    
                    if (s.Debug)
                    {
                        if (written)
                        {
                            Terminal.Write(", ");
                        }
                        else
                        {
                            written = true;
                        }
                        
                        Terminal.ForegroundColor = ConsoleColor.Gray;
                        Terminal.Write("Debug");
                        Terminal.ResetColor();
                    }
                    
                    if (!written)
                    {
                        Terminal.Write("ALL");
                    }
                    
                    Terminal.WriteLine("\n");
                    foreach (ChannelFilter cf in s) 
                    {
                    }
                }
            }
        }
    }
}
