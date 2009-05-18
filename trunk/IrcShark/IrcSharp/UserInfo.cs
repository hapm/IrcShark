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

using System;

namespace IrcSharp
{	
    /// <summary>
    /// Holds host informations about a user.
    /// </summary>
	public class UserInfo : IIrcObject
	{
		/// <summary>
		/// Creates a new UserInfo based on the host
		/// </summary>
		/// <param name="client">
		/// the <see cref="IrcClient"/> this UserInfo belongs to
		/// </param>
		/// <param name="host">
		/// a host as described in rfc 1459<see cref="System.String"/>
		/// </param>
		public UserInfo(IrcClient client, string host)
		{
		}
		
		public UserInfo(IrcLine baseLine)
		{
		}
		
		/// <value>
		/// the nickname of this UserInfo
		/// </value>
		public string NickName {
			get {
				throw new System.NotImplementedException();
			}
		}
		
		/// <value>
		/// the ident of this UserInfo
		/// </value>
		public string Ident {
			get {
				throw new System.NotImplementedException();
			}
		}
		
		/// <value>
		/// the host of this UserInfo
		/// </value>
		public string Host {
			get {
				throw new System.NotImplementedException();
			}
		}
		
		/// <remarks>
		/// this property is null if UserInfo wasn't build from an IrcLine but from a raw user host
		/// </remarks>
		/// <value>
		/// the <see cref="IrcLine"/>, this UserInfo was build from
		/// </value>
		public IrcLine BaseLine
		{
			get {
				throw new System.NotImplementedException();
			}
		}

		#region IIrcObject implementation
		/// <value>
		/// the <see cref="IrcClient"/> this UserInfo belongs to
		/// </value>
		public IrcClient Client {
			get {
				throw new System.NotImplementedException();
			}
		}
		#endregion
		
		/// <summary>
		/// gives back the raw host this UserInfo was created from
		/// </summary>
		/// <returns>
		/// the full raw host as a <see cref="System.String"/>
		/// </returns>
		public override string ToString ()
		{
			return string.Format("[UserInfo: NickName={0}, Ident={1}, Host={2}, Client={3}]", NickName, Ident, Host, Client);
		}
		
		/// <summary>
		/// compare this UserInfo with other objects
		/// </summary>
		/// <param name="obj">
		/// the object to compare with
		/// </param>
		/// <returns>
		/// true if obj is a UserInfo representing the same host as this UserInfo
		/// false otherwise
		/// </returns>
		public override bool Equals (object obj)
		{
			return base.Equals (obj);
		}
		
		/// <summary>
		/// gets the hashcode of this UserInfo
		/// </summary>
		public override int GetHashCode ()
		{
			return base.GetHashCode();
		}
	}
}
