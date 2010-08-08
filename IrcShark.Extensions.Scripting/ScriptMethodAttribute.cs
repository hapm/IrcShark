// <copyright file="ScriptMethodAttribute.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Scripting
{
    using System;
    using Mono.Addins;
    
    /// <summary>
    /// The ScriptMethodAttribute describes informations about a ScriptMethod.
    /// </summary>
    public class ScriptMethodAttribute : CustomExtensionAttribute
    {
        /// <summary>
        /// Saves the name of the method.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Initializes a new instance of the ScriptMethodAttribute class.
        /// </summary>
        public ScriptMethodAttribute()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ScriptMethodAttribute class.
        /// </summary>
        /// <param name="name">The name used to call the method.</param>
        public ScriptMethodAttribute([NodeAttribute("Name")]string name)
        {
            this.name = name;
        }
        
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>
        /// The name of the method as a string.
        /// </value>
        [NodeAttribute]
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
    }
}
