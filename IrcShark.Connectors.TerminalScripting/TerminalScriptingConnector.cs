﻿// <copyright file="TerminalScriptingConnector.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the TerminalScriptingConnector class.</summary>

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
    using System.Runtime.InteropServices;
    using IrcShark.Extensions;
    using IrcShark.Extensions.Scripting;
    using IrcShark.Extensions.Terminal;

    /// <summary>
    /// Description of TerminalScriptingConnector.
    /// </summary>
    [Guid("bccdf064-5d63-4907-866e-14449e7b3949")]
    public class TerminalScriptingConnector : Extension
    {
        private delegate void EchoDelegate(string[] line);
        private ScriptingExtension scripting;
        private TerminalExtension terminal;
        
        public TerminalScriptingConnector(ExtensionContext context) : base(context)
        {
        }
        
        public void Echo(string[] line)
        {
            terminal.WriteLine(string.Join(" ", line));
        }
        
        public override void Start()
        {
            ExtensionInfo scriptingInfo = Context.Application.Extensions["IrcShark.Extensions.Scripting.ScriptingExtension"];
            ExtensionInfo terminalInfo = Context.Application.Extensions["IrcShark.Extensions.Terminal.TerminalExtension"];
            scripting = Context.Application.Extensions[scriptingInfo] as ScriptingExtension;
            terminal = Context.Application.Extensions[terminalInfo] as TerminalExtension;
            scripting.PublishedMethods.Add("echo", new EchoDelegate(Echo));
        }
        
        public override void Stop()
        {
        }
    }
}