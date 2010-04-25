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
    public class EngineCommand : TerminalCommand
    {
        private ScriptingExtension scripting;
        
        public EngineCommand(TerminalExtension terminal, ScriptingExtension scripting) : base("engine", terminal)
        {
            this.scripting = scripting;
        }
        
        public override void Execute(params string[] paramList)
        {
            if (paramList.Length == 0)
            {
                Terminal.WriteLine("Please pecify a flag.");
                return;
            }
            
            switch (paramList[0])
            {
                case "-l":
                    Terminal.WriteLine("Listing all supported scripting languages:");
                    foreach (ScriptLanguageExtension lang in scripting.GetRegisteredLanguages())
                    {
                        Terminal.WriteLine(string.Format("{0}", lang.Engine.Language.LanguageName));
                    }
                    
                    break;
            }
        }
    }
}
