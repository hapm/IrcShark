// <copyright file="Group.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Group class.</summary>

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
namespace IrcShark.Extensions.Sessions
{
    using System;
    using System.Collections.Generic;
    using IrcShark.Security;
    using System.Security.Permissions;
    
    /// <summary>
    /// The GroupType specifies the type of a Group instance.
    /// </summary>
    public enum GroupType
    {
        /// <summary>
        /// There are some predefined groups. All of them have the Special type, and can't
        /// be deleted from the group list of the SessionManager.
        /// </summary>
        Special,
        
        /// <summary>
        /// Custom groups are groups defined by the user.
        /// </summary>
        Custom
    }

    /// <summary>
    /// The Group class represents a group of users.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Saves the name of the group.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves a description for the group.
        /// </summary>
        private string description;
        
        /// <summary>
        /// Saves a list of all roles, this group has.
        /// </summary>
        private List<string> roles;
        
        /// <summary>
        /// Saves a value indicating the type of the group.
        /// </summary>
        private GroupType groupType;
        
        /// <summary>
        /// Initializes a new instance of the Group class.
        /// </summary>
        /// <param name="nane">The name of the grou that is created.</param>
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.UserManager")]
        public Group(string name)
        {
            this.groupType = GroupType.Custom;
            this.name = name;
            this.roles = new List<string>();
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the Group is a special group or not.
        /// </summary>
        /// <value>
        /// If the value is true, the group represented by the instance is a special group.
        /// </value>
        public bool IsSpecialGroup 
        {
            get { return groupType == GroupType.Special; }
            internal set { groupType = GroupType.Special; }
        }
        
        /// <summary>
        /// Gets the name of the group. 
        /// </summary>
        /// <value>The name of the group.</value>
        /// <remarks>If the group is an extension group, its name is automatically prefixed with "ext".</remarks>
        public string Name
        {
            get
            {
                return name;
            }
        }
        
        /// <summary>
        /// Gets or sets the description of the group.
        /// </summary>
        public string Description
        {
            get 
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        
        /// <summary>
        /// Checks if the group has the given role.
        /// </summary>
        /// <param name="role">The role to check for.</param>
        /// <returns>Its true, if the group has this role.</returns>
        public bool HasRole(string role) 
        {
            return roles.Contains(role);
        }
    }
}
