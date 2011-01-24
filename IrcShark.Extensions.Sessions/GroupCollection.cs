// <copyright file="GroupCollection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the GroupCollection class.</summary>

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
    using System.Security;
    using System.Security.Permissions;
    using IrcShark.Security;
    
    /// <summary>
    /// The GroupCollection class manages the list of groups in the SessionManagerExtension.
    /// </summary>
    public class GroupCollection : ICollection<Group>
    {
        /// <summary>
        /// Saves all groups with there names as keys.
        /// </summary>
        private Dictionary<string, Group> groups;
        
        /// <summary>
        /// Initializes a new instance of the GroupCollection class.
        /// </summary>
        public GroupCollection()
        {
        }
        
        /// <summary>
        /// Gets the number of registered groups in this collection.
        /// </summary>
        public int Count 
        {
            get 
            {
                return groups.Count;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether the collection is readonly or not.
        /// </summary>
        public bool IsReadOnly 
        {
            get 
            {
                RolePermission p = new RolePermission(new string[] { "IrcShark.UserManager" }, RolePermissionType.And);
                return !SecurityManager.IsGranted(p);
            }
        }
        
        /// <summary>
        /// Adds a new group to the collection.
        /// </summary>
        /// <param name="item">The group to add.</param>
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.UserManager")]
        public void Add(Group item)
        {
            if (groups.ContainsKey(item.Name))
            {
                throw new ArgumentException(string.Format("A group with the name {0} already exists", item.Name), "item");
            }
            
            groups.Add(item.Name, item);
        }
        
        /// <summary>
        /// Clears all groups from this collection.
        /// </summary>
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.UserManager")]
        public void Clear()
        {
            groups.Clear();
        }
        
        /// <summary>
        /// Checks if the given group is in the collection or not.
        /// </summary>
        /// <param name="item">The group to check for.</param>
        /// <returns>Its true, if the group is in the collection, false otherwise.</returns>
        public bool Contains(Group item)
        {
            return groups.ContainsValue(item);
        }
        
        /// <summary>
        /// Checks if the group with the given name is in the collection or not.
        /// </summary>
        /// <param name="name">The name of thegroup to check for.</param>
        /// <returns>Its true, if the group is in the collection, false otherwise.</returns>
        public bool Contains(string name)
        {
            return groups.ContainsKey(name);
        }
        
        public void CopyTo(Group[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.UserManager")]
        public bool Remove(Group item)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerator<Group> GetEnumerator()
        {
            return (IEnumerator<Group>)groups.Values.GetEnumerator();
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return groups.Values.GetEnumerator();
        }
    }
}
