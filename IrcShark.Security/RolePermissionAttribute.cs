// <copyright file="RolePermissionAttribute.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the RolePermissionAttribute class.</summary>

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
namespace IrcShark.Security
{
    using System;
    using System.Security.Permissions;

    /// <summary>
    /// An extension can use this class to secure methods from being executed,
    /// when the user of the current session is not allowed to to so.
    /// </summary>
    public class RolePermissionAttribute : CodeAccessSecurityAttribute
    {
        /// <summary>
        /// Saves a list of roles the user must have to be able to execute
        /// the marked method.
        /// </summary>
        private string roles;
        
        /// <summary>
        /// Saves the type of the permission.
        /// </summary>
        private RolePermissionType type;
        
        /// <summary>
        /// Initializes a new instance of the RolePermissionAttribute class.
        /// </summary>
        /// <param name="action">A value of the SecurityAction enum.</param>
        public RolePermissionAttribute(SecurityAction action) : base(action)
        {
            type = RolePermissionType.And;
        }
        
        /// <summary>
        /// Gets or sets a comma seperated list of roles to check for.
        /// </summary>
        /// <value>
        /// A comma seperated list of roles to check.
        /// </value>
        public string Roles
        {
            get { return roles; }
            set { roles = value; }
        }
        
        /// <summary>
        /// Gets or sets the type of the check that should be executed over the list of roles.
        /// </summary>
        /// <value>
        /// A value of the RolePermissionType enum.
        /// </value>
        public RolePermissionType Type
        {
            get { return type; }
            set { type = value; }
        }
        
        /// <summary>
        /// Creates the permission for this attribute.
        /// </summary>
        /// <returns>The new Permssion instance.</returns>
        public override System.Security.IPermission CreatePermission()
        {
            return new RolePermission(roles.Split(','), type);
        }
    }
}
