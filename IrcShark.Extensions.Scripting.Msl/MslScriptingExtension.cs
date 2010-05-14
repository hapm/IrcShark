// <copyright file="MslScriptingExtension.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Scripting.Msl
{
    using System;
    using System.Runtime.InteropServices;
    using IrcShark.Extensions;
    using IrcShark.Extensions.Scripting;

    /// <summary>
    /// Description of MslScriptingExtension.
    /// </summary>
    [DependsOn(new string[] { "a004129f-4013-4b15-ba2e-ba0c063b5530" },
               ClassNames = new string[] { "IrcShark.Extensions.Scripting.ScriptingExtension" })]
    [Guid("fc67bb8e-076e-4144-a436-3d44848d1258")]
    public class MslScriptingExtension : ScriptLanguageExtension
    {
        private ScriptingExtension scripting;
        
        private MslScriptEngine engine;
        
        public MslScriptingExtension(ExtensionContext context) : base(context)
        {
            engine = new MslScriptEngine();
        }
        
        public override IScriptEngine Engine 
        {
            get { return engine; }
        }
        
        public override void Start()
        {
            ExtensionInfo info = Context.Application.Extensions["IrcShark.Extensions.Scripting.ScriptingExtension"];
            scripting = Context.Application.Extensions[info] as ScriptingExtension;
            scripting.RegisterLanguage(this);
        }
        
        public override void Stop()
        {
        }
    }
}
