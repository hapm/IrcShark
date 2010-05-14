// <copyright file="StatusChangedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the StatusChangedEventArgs class.</summary>

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
namespace IrcShark.Chatting
{
    using System;
    using IrcShark.Chatting;
    
    /// <summary>
    /// The StatusChangedEventHandler ist used by the <see cref="IConnection.StatusChanged" /> event.
    /// </summary>
    /// <param name="sender">The IConnection that changed its status.</param>
    /// <param name="args">The arguments for the event.</param>
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs args);
    
    /// <summary>
    /// Specifies all states a connection can have.
    /// </summary>
    public enum ConnectionStatus
    {
        /// <summary>
        /// The current status of the connection is unknown.
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Connection ist offline.
        /// </summary>
        Offline,
        
        /// <summary>
        /// Connection is on the way to get online.
        /// </summary>
        Connecting,
        
        /// <summary>
        /// Connection is waiting for authentication.
        /// </summary>
        Authing,
        
        /// <summary>
        /// Connection is established and useable.
        /// </summary>
        Online
    }

    /// <summary>
    /// The StatusChangedEventArgs are used by the StatusChangedEventHandler.
    /// </summary>
    public class StatusChangedEventArgs
    {
        ///
        private ConnectionStatus oldStatus;
        private ConnectionStatus newStatus;
        
        /// <summary>
        /// Initializes a new instance of the StatusChangedEventArgs class.
        /// </summary>
        /// <param name="newStatus">The new status of the object.</param>
        public StatusChangedEventArgs(ConnectionStatus newStatus)
        {
            this.oldStatus = ConnectionStatus.Unknown;
            this.newStatus = newStatus;
        }
        
        /// <summary>
        /// Initializes a new instance of the StatusChangedEventArgs class.
        /// </summary>
        /// <param name="oldStatus">The status, the object was in before the event.</param>
        /// <param name="newStatus">The new status of the object.</param>
        public StatusChangedEventArgs(ConnectionStatus oldStatus, ConnectionStatus newStatus)
        {
            this.oldStatus = oldStatus;
            this.newStatus = newStatus;
        }
        
        /// <summary>
        /// Gets the new status of the object.
        /// </summary>
        /// <value>The new status.</value>
        public ConnectionStatus NewStatus
        {
            get { return newStatus; }
        }
        
        /// <summary>
        /// Gets the old status of the object.
        /// </summary>
        /// <value>The old status or ConnectionStatus.Unknown if there was no status specified.</value>
        public ConnectionStatus OldStatus
        {
            get { return oldStatus; }
        }
    }
}
