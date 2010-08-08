// <copyright file="ExtensionAttribute.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Extension attribute.</summary>

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
namespace IrcShark.Extensions
{
    using System;
    using Mono.Addins;
    
    /// <summary>
    /// The Extension attribute is used to mark a class as an extension for IrcShark. The marked class need to
    /// inherit from IExtension interface or Extension class.
    /// </summary>
    public class ExtensionAttribute : CustomExtensionAttribute
    {
        /// <summary>
        /// Saves the name of the extension.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionAttribute class.
        /// </summary>
        public ExtensionAttribute()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ExtensionAttribute class.
        /// </summary>
        /// <param name="name">
        /// The name of the extension.
        /// </param>
        public ExtensionAttribute([NodeAttribute("Name")]string name)
        {
            Name = name;
        }
        
        
        /// <summary>
        /// Gets or sets the name of the extension.
        /// </summary>
        /// <value>
        /// The name of the extension as a string.
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
