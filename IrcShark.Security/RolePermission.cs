// <copyright file="RolePermission.cs" company="IrcShark Team">
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
namespace IrcShark.Security
{
    using System;
    using System.Security;
    
    /// <summary>
    /// Represents the type of a RolePermission.
    /// </summary>
    public enum RolePermissionType {
        /// <summary>
        /// If the RolePermission has the And type, the current user needs to have
        /// all given roles.
        /// </summary>
        And,
        
        /// <summary>
        /// If the RolePermission has the Or type, the current user needs to have
        /// one of the given roles.
        /// </summary>
        Or
    }

    /// <summary>
    /// The RolePermission checks if the current other has the given rules or not.
    /// </summary>
    public class RolePermission : IPermission
    {
        /// <summary>
        /// Saves the list of rules.
        /// </summary>
        private string[] roles;
        
        /// <summary>
        /// Saves how the check shopuld run over the rules.
        /// </summary>
        private RolePermissionType type;
        
        /// <summary>
        /// Initializes a new instance of the RolePermission class.
        /// </summary>
        /// <param name="roles">The roles to check as an array.</param>
        /// <param name="type">The type of how to check the rules.</param>
        public RolePermission(string[] roles, RolePermissionType type)
        {
            this.roles = (string[])roles.Clone();
            this.type = type;
        }
        
        public IPermission Intersect(IPermission target)
        {
            throw new NotImplementedException();
        }
        
        public IPermission Union(IPermission target)
        {
            throw new NotImplementedException();
        }
        
        public bool IsSubsetOf(IPermission target)
        {
            throw new NotImplementedException();
        }
        
        public void Demand()
        {
            switch(type) {
                case RolePermissionType.And:
                    foreach (string role in roles)
                    {
                        if (!System.Threading.Thread.CurrentPrincipal.IsInRole(role))
                            throw new SecurityException(string.Format("You do not have the needed role \"{0}\" for this operation.", role));
                    }
                    
                    break;
                    
                case RolePermissionType.Or:
                    foreach (string role in roles) 
                    {
                        if (System.Threading.Thread.CurrentPrincipal.IsInRole(role))
                            return;
                    }
                    
                    throw new SecurityException(string.Format("You do not have one of needed roles \"{0}\" for this operation.", string.Join(" ,", roles)));
            }
        }
        
        public IPermission Copy()
        {
            throw new NotImplementedException();
        }
        
        public SecurityElement ToXml()
        {
            throw new NotImplementedException();
        }
        
        public void FromXml(SecurityElement e)
        {
            throw new NotImplementedException();
        }
    }
}
