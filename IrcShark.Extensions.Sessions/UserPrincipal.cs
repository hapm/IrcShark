// <copyright file="SessionPrincipal.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the SessionPrincipal class.</summary>

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
    using System.Security.Principal;

    /// <summary>
    /// The SessionPrincipal describes the principal for a given session.
    /// </summary>
    public class UserPrincipal : IPrincipal
    {
        /// <summary>
        /// Saves the identity the user is identified with.
        /// </summary>
        private IIdentity identity;
        
        /// <summary>
        /// Saves the User instance of this principal.
        /// </summary>
        private User user;
        
        /// <summary>
        /// Inititalizes a new instance of the UserPrincipal class for the given user.
        /// </summary>
        /// <param name="user">The user to create the principal for.</param>
        public UserPrincipal(User user)
        {
            identity = new GenericIdentity(user.Name);
            this.user = user;
        }
        
        /// <summary>
        /// The current identity of the user for this principal.
        /// </summary>
        public IIdentity Identity 
        {
            get {
                return identity;
            }
        }
        
        /// <summary>
        /// Checks if the user owns a given role or not.
        /// </summary>
        /// <param name="role">The role name to check for.</param>
        /// <returns>true, if the user is member of the given role and the user is authenticated, false otherwise.</returns>
        public bool IsInRole(string role)
        {
            if (!identity.IsAuthenticated)
                return false;
            
            return user.HasRole(role);
        }
    }
}
