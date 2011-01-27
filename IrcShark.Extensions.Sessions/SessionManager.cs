// <copyright file="SessionManager.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the SessionMananger class.</summary>

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

    /// <summary>
    /// The SessionManager class handles all sessions of the SessionManagementExtension.
    /// </summary>
    public class SessionManager : ICollection<Session>
    {
    	private Dictionary<Guid, Session> sessions;
    	
    	/// <summary>
    	/// Initializes a new instance of the SessionManager class.
    	/// </summary>
        public SessionManager()
        {
        	this.sessions = new Dictionary<Guid, Session>();
        }
    	
		public int Count {
			get {
				throw new NotImplementedException();
			}
		}
    	
		public bool IsReadOnly {
			get {
				throw new NotImplementedException();
			}
		}
    	
		public void Add(Session item)
		{
			throw new NotImplementedException();
		}
    	
		public void Clear()
		{
			throw new NotImplementedException();
		}
    	
		public bool Contains(Session item)
		{
			return sessions.ContainsValue(item);
		}
		
		public bool Contains(Guid sessionId) 
		{
			return sessions.ContainsKey(sessionId);
		}
    	
		public void CopyTo(Session[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}
    	
		public bool Remove(Session item)
		{
			throw new NotImplementedException();
		}
    	
		public IEnumerator<Session> GetEnumerator()
		{
			throw new NotImplementedException();
		}
    	
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Requests a new session for the given user.
		/// </summary>
		/// <param name="user">The name of the user, that the session is created for.</param>
		/// <returns></returns>
		public Session RequestSession(string user)
		{
			throw new NotImplementedException();
		}
    }
}
