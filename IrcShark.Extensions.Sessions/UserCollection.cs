// <copyright file="UserCollection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the UserCollection class.</summary>

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
    using IrcShark.Security;
    using System.Security;
    using System.Security.Permissions;
    
    /// <summary>
    /// Description of UserCollection.
    /// </summary>
    public class UserCollection : ICollection<User>
    {
        /// <summary>
        /// Saves the internal user list.
        /// </summary>
        Dictionary<string, User> list;
        
        /// <summary>
        /// Initializes a new instance of the UserCollection class.
        /// </summary>
        public UserCollection()
        {
            list = new Dictionary<string, User>();
        }
        
        public int Count 
        {
            get 
            {
                return list.Count;
            }
        }
        
        public bool IsReadOnly 
        {
            get 
            {
                RolePermission p = new RolePermission(new string[] { "IrcShark.UserManager" }, RolePermissionType.And);
                return !SecurityManager.IsGranted(p);
            }
        }
        
        /// <summary>
        /// Adds the given User to the UserCollection.
        /// </summary>
        /// <param name="item">The user to add.</param>
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.UserManager")]
        public void Add(User item)
        {
            if (list.ContainsKey(item.Name)) 
            {
                throw new ArgumentException("The user with the name " + item.Name + " already exists.");
            }
            
            list.Add(item.Name, item);
        }
        
        public void Clear()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// checks if the given User instance is in this collection.
        /// </summary>
        /// <param name="item">The instance of the user to check for.</param>
        /// <returns>true, if the user exists in this collection, false otherwise.</returns>
        public bool Contains(User item)
        {
            return list.ContainsValue(item);
        }
        
        /// <summary>
        /// checks if the given username is in this collection.
        /// </summary>
        /// <param name="item">The name of the user to check for.</param>
        /// <returns>true, if the user exists in this collection, false otherwise.</returns>
        public bool Contains(string name)
        {
            return list.ContainsKey(name);
        }
        
        public void CopyTo(User[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Removes a given user from this collection.
        /// </summary>
        /// <param name="item">The user to remove.</param>
        /// <returns>true if the user was removed, false otherwise.</returns>
        [RolePermission(SecurityAction.Demand, Roles="IrcShark.UserManager")]
        public bool Remove(User item)
        {
            if (!list.ContainsValue(item))
                return false;
            
            return list.Remove(item.Name);
        }
        
        public IEnumerator<User> GetEnumerator()
        {
            return (IEnumerator<User>)list.Values.GetEnumerator();
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.Values.GetEnumerator();
        }
    }
}
