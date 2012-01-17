// <copyright file="Session.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Session class.</summary>

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

    /// <summary>
    /// The Session instance identifys a users session and holds session specific variables.
    /// </summary>
    public class Session
    {
    	/// <summary>
    	/// Saves the principal for the user of this session.
    	/// </summary>
    	private UserPrincipal principal;
    	
        /// <summary>
        /// Saves the instance of the SessionManagerExtension
        /// </summary>
        private SessionManagementExtension manager;
        
        /// <summary>
        /// Saves the uniqueue id for this session.
        /// </summary>
        private Guid sessionId;
        
        /// <summary>
        /// Saves the state of this session.
        /// </summary>
        private bool active;
        
        /// <summary>
        /// Initializes a new instance of the Session class.
        /// </summary>
        internal Session(SessionManagementExtension manager, UserPrincipal principal)
        {
            this.manager = manager;
            do 
            {
            	this.sessionId = Guid.NewGuid();
            } 
            while (manager.HasSession(this.sessionId));
            this.principal = principal;
        }
        
        /// <summary>
        /// Gets the principal for the current session.
        /// </summary>
        public UserPrincipal Principal
        {
        	get 
        	{
        		return principal;
        	}
        }
        
        /// <summary>
        /// Gets the id of the session.
        /// </summary>
        public Guid SessionId 
        {
        	get 
        	{
        		return sessionId;
        	}
        }
        
        /// <summary>
        /// Gets a value indicating whether the session is active or not.
        /// </summary>
        public bool IsActive
        {
        	get 
        	{
        		return active;
        	}
        }
        
        /// <summary>
        /// Closes the Session and removes it from the sessions list.
        /// </summary>
        public void Close()
        {
        	this.active = false;
        }
    }
}
