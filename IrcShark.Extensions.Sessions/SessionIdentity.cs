// <copyright file="SessionIdentity.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the SessionIdentity class.</summary>

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
    /// The SessionIdentity is used to identify a user of a session.
    /// </summary>
    public class SessionIdentity : IIdentity
    {
        public SessionIdentity(Session session)
        {
        }
        
        public string Name {
            get {
                throw new NotImplementedException();
            }
        }
        
        public string AuthenticationType {
            get {
                throw new NotImplementedException();
            }
        }
        
        public bool IsAuthenticated {
            get {
                throw new NotImplementedException();
            }
        }
    }
}
