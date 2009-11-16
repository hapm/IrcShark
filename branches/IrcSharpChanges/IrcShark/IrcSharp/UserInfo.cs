// $Id$
// 
// Note:
// 
// Copyright (C) 2009 IrcShark Team
//  
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

namespace IrcSharp
{
    using System;

    /// <summary>
    /// Holds host informations about a user.
    /// </summary>
    public class UserInfo : IIrcObject
    {
        /// <summary>
        /// Initializes a new instance of the UserInfo class based on the host.
        /// </summary>
        /// <param name="client">
        /// The <see cref="IrcClient"/> this UserInfo belongs to.
        /// </param>
        /// <param name="host">
        /// A host as described in rfc 1459 as a <see cref="System.String"/>.
        /// </param>
        public UserInfo(IrcClient client, string host)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the UserInfo class based on the given <see cref="IrcLine" />.
        /// </summary>
        /// <param name="baseLine">
        /// The <see cref="IrcLine"/> this UserInfo was build from.
        /// </param>
        public UserInfo(IrcLine baseLine)
        {
        }
        
        /// <summary>
        /// Gets the nickname of the user.
        /// </summary>
        /// <value>
        /// The nickname of this UserInfo.
        /// </value>
        public string NickName 
        {
            get { throw new System.NotImplementedException(); }
        }
        
        /// <summary>
        /// Gets the ident of the user.
        /// </summary>
        /// <value>
        /// The ident of this UserInfo.
        /// </value>
        public string Ident 
        {
            get { throw new System.NotImplementedException(); }
        }
        
        /// <summary>Gets the host of the user.</summary>
        /// <value>
        /// The host of this UserInfo.
        /// </value>
        public string Host 
        {
            get { throw new System.NotImplementedException(); }
        }
        
        /// <summary>
        /// Gets the IrcLine, this UserInfo was build from.
        /// </summary>
        /// <value>
        /// The <see cref="IrcLine"/>, this UserInfo was build from.
        /// </value>
        /// <remarks>
        /// This property is null if UserInfo wasn't build from an IrcLine but from a raw user host.
        /// </remarks>
        public IrcLine BaseLine
        {
            get { throw new System.NotImplementedException(); }
        }

        #region IIrcObject implementation
        /// <summary>
        /// Gets the IrcClient, this UserInfo belongs to.
        /// </summary>
        /// <value>
        /// The <see cref="IrcClient"/> this UserInfo belongs to.
        /// </value>
        public IrcClient Client 
        {
            get { throw new System.NotImplementedException(); }
        }
        #endregion
        
        /// <summary>
        /// Gives back the raw host this UserInfo was created from.
        /// </summary>
        /// <returns>
        /// The full raw host as a <see cref="System.String"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[UserInfo: NickName={0}, Ident={1}, Host={2}, Client={3}]", NickName, Ident, Host, Client);
        }
        
        /// <summary>
        /// Compare this UserInfo with other objects.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with.
        /// </param>
        /// <returns>
        /// True if obj is a UserInfo representing the same host as this UserInfo,
        /// false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        
        /// <summary>
        /// Gets the hashcode of this UserInfo.
        /// </summary>
        /// <returns>The hashcode as an int.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
