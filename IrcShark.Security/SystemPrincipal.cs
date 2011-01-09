// <copyright file="SystemPrincipal.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the SystemPrincipal class.</summary>

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
    using System.Security.Principal;

    /// <summary>
    /// The SystemPrincipal is a special principal of IrcShark. It represents the 
    /// default principal when there is no one like the SessionManager, that sets
    /// a different one.
    /// </summary>
    public class SystemPrincipal : IPrincipal
    {
        /// <summary>
        /// Saves the identity of the principal.
        /// </summary>
        private IIdentity identity;
        
        /// <summary>
        /// Initializes a new instance of the SystemPrincipal class.
        /// </summary>
        public SystemPrincipal()
        {
            identity = new GenericIdentity("System", "System");
        }
        
        /// <summary>
        /// Gets the idenity of the SystemPrincipal.
        /// </summary>
        /// <value>
        /// A generic identity of the System.
        /// </value>
        public IIdentity Identity 
        {
            get 
            {
                return identity;
            }
        }
        
        /// <summary>
        /// Checks if the SystemPrincipal has the given role.
        /// </summary>
        /// <param name="role">The role to check for</param>
        /// <returns>It returns always true, as the system has any permission.</returns>
        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
