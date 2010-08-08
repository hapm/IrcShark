// <copyright file="EchoScriptingMethod.cs" company="IrcShark Team">
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
    /// The EchoScriptingMethod can be used to show text from scripts on the console.
    /// </summary>
    [ScriptMethod("echo")]
    public class EchoScriptingMethod : IScriptMethod
    {
        /// <summary>
        /// The delegate of the echo method.
        /// </summary>
        private delegate void EchoDelegate(string[] line);
        
        /// <summary>
        /// A reference to the terminal extension.
        /// </summary>
        private TerminalExtension terminal;
        
        /// <summary>
        /// Initializes a new instance of the EchoScriptMethod class.
        /// </summary>
        public EchoScriptingMethod()
        {
        }
        
        /// <summary>
        /// Writes a line to the Terminal of the TerminalExtension.
        /// </summary>
        /// <param name="line"></param>
        public void Echo(string[] line)
        {
            terminal.WriteLine(string.Join(" ", line));
        }
        
        /// <summary>
        /// Gets the delegate of this method.
        /// </summary>
        /// <param name="scripting">The ScriptingExtension instance.</param>
        public Delegate GetMethodDelegat(ScriptingExtension scripting)
        {
            terminal = scripting.Context.Application.Extensions["IrcShark.Extensions.Terminal.TerminalExtension"] as TerminalExtension;
            return new EchoDelegate(Echo);
        }
    }
}
