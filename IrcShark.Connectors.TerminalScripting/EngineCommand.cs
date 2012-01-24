// <copyright file="EngineCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcShark.Connectors.TerminalScripting
{
    using System;
    using IrcShark.Extensions.Scripting;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// Description of EngineCommand.
    /// </summary>
    [TerminalCommand("engine")]
    public class EngineCommand : TerminalCommand
    {    	
        private ScriptingExtension scripting;
        
        public override void Init(TerminalExtension terminal)
        {
            base.Init(terminal);
            scripting = Terminal.Context.Application.Extensions["IrcShark.Extensions.Scripting.ScriptingExtension"] as ScriptingExtension;
        }
        
        /// <summary>
        /// Executes the EngineCommand.
        /// </summary>
		/// <param name="terminal">
		/// The terminal, the command was called from.
		/// </param>
        /// <param name="paramList">A list of parameters.</param>
        public override void Execute(ITerminal terminal, params string[] paramList)
        {
            if (paramList.Length == 0)
            {
                terminal.WriteLine("Please pecify a flag.");
                return;
            }
            
            switch (paramList[0])
            {
                case "-l":
                    terminal.WriteLine("Listing all supported scripting languages:");
                    foreach (IScriptEngine lang in scripting.GetRegisteredLanguages())
                    {
                        terminal.WriteLine(string.Format("{0}", lang.Language.LanguageName));
                    }
                    
                    break;
            }
        }
    }
}
