// <copyright file="AddinCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the AddinCommand class.</summary>

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
    using System.Collections.Generic;
    using Mono.Addins;    
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// The AddinCommand displays the list of available and enabled addins and allows to manipulate them.
    /// </summary>
    [TerminalCommand("addin")]
    public class AddinCommand : TerminalCommand
    {
        /// <summary>
        /// Initializes a new instance of the AddinCommand.
        /// </summary>
        public AddinCommand()
        {
        }
        
        /// <summary>
        /// Executes the AddinCommand.
        /// </summary>
		/// <param name="terminal">
		/// The terminal, the command was called from.
		/// </param>
        /// <param name="paramList">The Parameters for the command.</param>
        public override void Execute(ITerminal terminal, params string[] paramList)
        {
            if (paramList.Length < 1)
            {
                Terminal.WriteLine(Translation.Messages.SpecifyFlag);
                return;
            }
            
            switch (paramList[0])
            {
                case "-l":
                    ListAddins(terminal);
                    break;
                case "-e":
                    EnableAddin(paramList, terminal);
                    break;
                case "-d":
                    DisableAddin(paramList, terminal);
                    break;
                default:
                    terminal.WriteLine(string.Format(Translation.Messages.UnknownFlag, paramList[0]));
                    break;
            }
        }
        
        /// <summary>
        /// Compeltes the parameters of the AddinCommand.
        /// </summary>
        /// <param name="call">The current command call.</param>
        /// <param name="paramIndex">The parameter index to complete.</param>
        /// <returns>A list of possible completitions.</returns>
        public override string[] AutoComplete(CommandCall call, int paramIndex)
        {
            if (paramIndex == 0) 
            {
                if (string.IsNullOrEmpty(call.Parameters[0]) || call.Parameters[0] == "-") 
                {
                    return new string[] { "-d", "-e", "-l" };
                }
            }
            else if (paramIndex > 0 && (call.Parameters[0] == "-e" || call.Parameters[0] == "-d"))
            {
                List<string> result = new List<string>();
                if (call.Parameters.Length <= paramIndex || string.IsNullOrEmpty(call.Parameters[paramIndex]))
                {
                    foreach (Addin addin in AddinManager.Registry.GetAddins()) 
                    {
                        if ((addin.Enabled && call.Parameters[0] == "-d") || (!addin.Enabled && call.Parameters[0] == "-e"))
                        {
                            result.Add(addin.LocalId);
                        }
                    }                    
                }
                else
                {
                    string id = call.Parameters[paramIndex];
                    foreach (Addin addin in AddinManager.Registry.GetAddins()) 
                    {
                        if (((addin.Enabled && call.Parameters[0] == "-d") || (!addin.Enabled && call.Parameters[0] == "-e")) && addin.LocalId.StartsWith(id))
                        {
                            result.Add(addin.LocalId);
                        }                        
                    }
                }
                return result.ToArray();
            }
            
            return base.AutoComplete(call, paramIndex);
        }
        
        /// <summary>
        /// Writes a list of addins to the terminal.
        /// </summary>
        /// <param name="terminal">
        /// The terminal to execute the command on.
        /// </param>
        private void ListAddins(ITerminal terminal) 
        {
            foreach (Addin addin in AddinManager.Registry.GetAddins()) 
            {
                if (!string.IsNullOrEmpty(addin.Description.Description))
                {
                    terminal.Write("{0} {1} - {2} ", addin.LocalId, addin.Version, addin.Description.Description);
                }
                else 
                {
                    terminal.Write("{0} {1} ", addin.LocalId, addin.Version);
                }
                
                if (addin.Enabled)
                {
                    terminal.ForegroundColor = ConsoleColor.DarkGreen;
                    terminal.WriteLine("ENABLED");
                }
                else
                {
                    terminal.ForegroundColor = ConsoleColor.DarkRed;
                    terminal.WriteLine("DISABLED");                    
                }
                
                terminal.ResetColor();
            }
        }
        
        /// <summary>
        /// Enables all Addins with the given ids.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="terminal">
        /// The terminal to execute the command on.
        /// </param>
        private void EnableAddin(string[] args, ITerminal terminal) 
        {
            for (int i = 1; i < args.Length; i++)
            {
                string id = args[i];
                Addin addin = AddinManager.Registry.GetAddin(id);
                if (addin == null)
                {
                    terminal.ForegroundColor = ConsoleColor.DarkRed;
                    //TODO translation
                    terminal.WriteLine("The addin with id {0} wasn't found.", id);
                    terminal.ResetColor();
                }
                else if (addin.Enabled)
                {
                    //TODO translation
                    terminal.WriteLine("The addin with id {0} was already enabled.", id);                    
                }
                else 
                {
                    addin.Enabled = true;
                    //TODO translation
                    terminal.WriteLine("The addin with id {0} was enabled.", id);
                }
            }
        }
        
        /// <summary>
        /// Disables all Addins with the given ids.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="terminal">
        /// The terminal to execute the command on.
        /// </param>
        private void DisableAddin(string[] args, ITerminal terminal) 
        {
            for (int i = 1; i < args.Length; i++)
            {
                string id = args[i];
                Addin addin = AddinManager.Registry.GetAddin(id);
                if (addin == null)
                {
                    terminal.ForegroundColor = ConsoleColor.DarkRed;
                    //TODO translation
                    terminal.WriteLine("The addin with id {0} wasn't found.", id);
                    terminal.ResetColor();
                }
                else if (!addin.Enabled)
                {
                    //TODO translation
                    terminal.WriteLine("The addin with id {0} was already disabled.", id);                    
                }
                else 
                {
                    addin.Enabled = false;
                    //TODO translation
                    terminal.WriteLine("The addin with id {0} was disabled.", id);
                }
            }
        }
    }
}
