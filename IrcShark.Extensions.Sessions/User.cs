// <copyright file="User.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the User class.</summary>

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
    using System.Security.Principal;
    using System.Security.Permissions;
    using IrcShark.Security;

    /// <summary>
    /// The User class holds all informations about a user in the user database.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Saves the list of groups, the user is member of.
        /// </summary>
        private List<Group> groups;
        
        /// <summary>
        /// The local name of the user.
        /// </summary>
        private string localName;
        
        /// <summary>
        /// Saves the default unauthenticated identity of this user;
        /// </summary>
        private GenericIdentity identity;
        
        /// <summary>
        /// Initializes a instance of the User class with a given name.
        /// </summary>
        /// <param name="name">The local name of the user.</param>
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.UserManager")]
        public User(string name)
        {
            this.localName = name;
            this.identity = new GenericIdentity(name);
            this.groups = new List<Group>();
        }
        
        /// <summary>
        /// Gets the name of the user represented by this intance.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string Name
        {
            get { return localName; }
        }
        
        /// <summary>
        /// Checks if the user has the given role or not.
        /// </summary>
        /// <param name="roleName">The role to check.</param>
        /// <returns></returns>
        public bool HasRole(string roleName) 
        {
            foreach (Group group in groups)
            {
                if (group.HasRole(roleName))
                    return true;
            }
            
            return false;
        }
    }
}
