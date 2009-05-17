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
using System.Net;

namespace IrcSharp
{
	/// <summary>
	/// Represents an irc endpoint for an irc connection
	/// </summary>
	public class IrcServerEndPoint : System.Net.IPEndPoint
	{
		private string serverHostName;
		private bool isIdentDRequired;
		private string password;
		
		/// <summary>
		/// creates a new end point for an irc connection
		/// </summary>
		/// <param name="address">
		/// the dns of the irc server as a <see cref="System.String"/>
		/// </param>
		/// <param name="port">
		/// the port where the irc server is listening on
		/// </param>
		public IrcServerEndPoint(string hostname, int port) : base(0,0)
		{
            IPAddress[] addresses = Dns.GetHostEntry(hostname).AddressList;
            Address = addresses[0];
			Port = port;
			serverHostName = hostname;
		}
		
		public IrcServerEndPoint(IPAddress address, int port) : base(address, port)
		{
		}
		
		/// <value>
		/// The dns of the ircserver, if could be resolved, else null
		/// </value>
		public string ServerHostName 
		{
			get { return serverHostName; }
			set 
			{ 
            	IPAddress[] addresses = Dns.GetHostEntry(value).AddressList;
            	Address = addresses[0];				
				serverHostName = value; 
			}
		}
		
		/// Gets or sets if this irc end point needs a running identd when establishing the connection
		/// <value>
		/// true, if the identd is needed
		/// false, otherwises
		/// </value>
		public bool IsIdentDRequired
		{
			get { return isIdentDRequired; }
			set { isIdentDRequired = value; }
		}
		
		/// Gets or sets the password to use when establishing a connection to this irc end point
		/// <value>
		/// the password as a string, use null to use no password
		/// </value>
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
	}
}
