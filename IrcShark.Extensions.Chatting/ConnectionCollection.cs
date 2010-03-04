// <copyright file="ConnectionCollection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ConnectionCollection class.</summary>

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
    using System.Collections.Generic;
    using IrcShark.Chatting;

    /// <summary>
    /// The ConnectionCollection class holds a collection of connections.
    /// </summary>
    public class ConnectionCollection : ICollection<IConnection>
    {
        /// <summary>
        /// Saves the instance to the underlying list instance.
        /// </summary>
        private List<IConnection> connections;
        
        /// <summary>
        /// This event is fired when a new connection was added.
        /// </summary>
        public event ConnectionEventHandler AddedConnection;
        
        /// <summary>
        /// This event is fired when a connection was removed.
        /// </summary>
        public event ConnectionEventHandler RemovedConnection;
        
        /// <summary>
        /// Initializes a new instance of the ConnectionCollection class.
        /// </summary>
        public ConnectionCollection()
        {
            connections = new List<IConnection>();
        }
        
        /// <summary>
        /// Gets the connection at the given index.
        /// </summary>
        /// <value>The connection instance.</value>
        public IConnection this[int index]
        {
            get { return connections[index]; }
        }
        
        /// <summary>
        /// Gets the number of connections in this collection.
        /// </summary>
        /// <value>The number of connections.</value>
        public int Count
        {
            get { return connections.Count; }
        }
        
        /// <summary>
        /// Gets a value indicating whether this collection is readonly or not.
        /// </summary>
        /// <value>
        /// Always false as this type isn't read only.
        /// </value>
        public bool IsReadOnly 
        {
            get { return false; }
        }
        
        /// <summary>
        /// Adds a new connection to the collection.
        /// </summary>
        /// <param name="item">The connection to add.</param>
        public void Add(IConnection item)
        {
            connections.Add(item);
            OnAddedConnection(item);
        }
        
        /// <summary>
        /// Clears all connections.
        /// </summary>
        public void Clear()
        {
            connections.Clear();
        }
        
        /// <summary>
        /// Checks if the given connection is already in the collection.
        /// </summary>
        /// <param name="item">The connection to check.</param>
        /// <returns>Its true if the connection was already added, false otherwise.</returns>
        public bool Contains(IConnection item)
        {
            return connections.Contains(item);
        }
        
        /// <summary>
        /// Copys all connections to an array at the given index.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The array index to start from.</param>
        public void CopyTo(IConnection[] array, int arrayIndex)
        {
            connections.CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// Removes the give connection from the collection.
        /// </summary>
        /// <param name="item">The conenction to remove.</param>
        /// <returns>Its true if the connection was successfully removed, false otherwise.</returns>
        public bool Remove(IConnection item)
        {
            bool result = connections.Remove(item);
            if (result)
            {
                OnRemovedConnection(item);
            }
            
            return result;
        }
        
        /// <summary>
        /// Gets a generic enumerator for the collection.
        /// </summary>
        /// <returns>The enumerator instance.</returns>
        public IEnumerator<IConnection> GetEnumerator()
        {
            return ((IEnumerable<IConnection>)connections).GetEnumerator();
        }
        
        /// <summary>
        /// Gets an enumerator for the collection.
        /// </summary>
        /// <returns>The enumerator instance.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return connections.GetEnumerator();
        }
        
        /// <summary>
        /// Fires the AddedConnection event.
        /// </summary>
        /// <param name="con">The connection that was added.</param>
        protected void OnAddedConnection(IConnection con)
        {
            if (AddedConnection != null)
            {
                ConnectionEventArgs args = new ConnectionEventArgs(con);
                AddedConnection(this, args);
            }
        }
        
        /// <summary>
        /// Fires the RemovedConnection event.
        /// </summary>
        /// <param name="con">The connection that was removed.</param>
        protected void OnRemovedConnection(IConnection con)
        {
            if (RemovedConnection != null)
            {
                ConnectionEventArgs args = new ConnectionEventArgs(con);
                RemovedConnection(this, args);
            }
        }
    }
}
