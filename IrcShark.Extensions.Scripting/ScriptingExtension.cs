﻿// <copyright file="ScriptingExtension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ScriptingExtension class.</summary>

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
namespace IrcShark.Extensions.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using IrcShark.Extensions;
    

    /// <summary>
    /// Description of MyClass.
    /// </summary>
    [GuidAttribute("a004129f-4013-4b15-ba2e-ba0c063b5530")]
    public class ScriptingExtension : Extension
    {
        private List<ScriptLanguageExtension> languages;
        
        public ScriptingExtension(ExtensionContext context) : base(context)
        {
            languages = new List<ScriptLanguageExtension>();
        }
        
        public void RegisterLanguage(ScriptLanguageExtension ext)
        {
            languages.Add(ext);
        }
        
        public override void Start()
        {
        }
        
        public override void Stop()
        {
        }
    }
}