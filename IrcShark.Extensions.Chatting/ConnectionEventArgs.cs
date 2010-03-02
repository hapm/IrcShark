// <copyright file="ConnectionEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ConnectionEventArgs class.</summary>

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
namespace IrcShark.Extensions.Chatting
{
    using System;
    using IrcShark.Chatting;

    /// <summary>
    /// Delegate for a connection event. 
    /// </summary>
    public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs args);
    
    /// <summary>
    /// The ConnectionEventArgs are used for any connection event.
    /// </summary>
    public class ConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// Saves the instance to the related connection.
        /// </summary>
        private IConnection con;
        
        /// <summary>
        /// Initializes a new instance of the ConnectionEventArgs class.
        /// </summary>
        /// <param name="connection">The connection, the evnt is related to.</param>
        public ConnectionEventArgs(IConnection connection)
        {
            con = connection;
        }
        
        /// <summary>
        /// Gets the connection to what the event is related to.
        /// </summary>
        /// <returns>The connection reference.</returns>
        public IConnection Connection
        {
            get { return con; }
        }
    }
}
