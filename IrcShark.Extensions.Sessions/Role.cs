// <copyright file="Role.cs" company="IrcShark Team">
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
using System;

namespace IrcShark.Extensions.Sessions
{
    /// <summary>
    /// The Role class represents a role.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Saves the internal name of the role.
        /// </summary>
        private string internalName;
        
        /// <summary>
        /// Saves the display name of the role.
        /// </summary>
        private string displayName;
        
        /// <summary>
        /// Saves the description of a role.
        /// </summary>
        private string description;
        
        /// <summary>
        /// Initializes a new instance of the Role class.
        /// </summary>
        /// <param name="name">The internal name used by this role.</param>
        /// <param name="displayName">The name used to display the role.</param>
        /// <param name="description">The description of the role.</param>
        public Role(string name, string displayName, string description)
        {
            this.internalName = name;
            if (displayName == null) 
                this.displayName = name;
            else
                this.displayName = displayName;
            
            this.description = description;
        }
        
        /// <summary>
        /// Gets the internal name of the role.
        /// </summary>
        /// <value>A string containing the name.</value>
        public string InternalName 
        {
            get { return internalName; }
        }
        
        /// <summary>
        /// Gets the name used to display this role in user interfaces.
        /// </summary>
        /// <value>A localized name of the role.</value>
        public string DisplayName
        {
            get { return displayName; }
        }
        
        /// <summary>
        /// Gets the description of this role if specified.
        /// </summary>
        /// <value>
        /// The description as a localized string, or null if the provide was to lazy
        /// to specify one.
        /// </value>
        public string Description
        {
            get { return description; }
        }
    }
}
