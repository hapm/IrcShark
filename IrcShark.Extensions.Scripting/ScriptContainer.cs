// <copyright file="ScriptContainer.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ScriptContainer class.</summary>

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
    using System.CodeDom;

    /// <summary>
    /// A ScriptContainer contains a script of a given language.
    /// </summary>
    public class ScriptContainer
    {
        /// <summary>
        /// Contains the AppDomain, the script is running in.
        /// </summary>
        private AppDomain scriptDomain;
        
        private CodeCompileUnit scriptDom;
        
        public ScriptContainer()
        {
            
        }
        
        public void Compile()
        {
        }
        
        public void Execute()
        {
            
        }
        
        public void Unload()
        {
            
        }
    }
}
