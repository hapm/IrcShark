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
	/// This class saves the standard, used by a server, the associated <see cref="IrcSharp.IrcClient"/> is connected to.
	/// </summary>
	public class IrcStandardDefinition : IIrcObject
	{
		private IrcClient client;
		
		/// <summary>
		/// Creates a new standard defintion and associates it with the given client
		/// </summary>
		/// <param name="client">
		/// the <see cref="IrcClient"/>, this StandardDefinition belongs to
		/// </param>
		public IrcStandardDefinition(IrcClient client)
		{
			this.client = client;
		}
		
		#region IIrcObject implementation
		/// <value>
		/// the client the standard instance belongs to
		/// </value>
		public IrcClient Client 
		{
			get { return client; }
		}
		#endregion
	}
}
