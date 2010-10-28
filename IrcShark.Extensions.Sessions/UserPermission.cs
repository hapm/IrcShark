// <copyright file="UserPermission.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the UserPermission class.</summary>

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
    using System.Security;

    /// <summary>
    /// Describes a UserPermission.
    /// </summary>
    public abstract class UserPermission : IPermission
    {
        /// <summary>
        /// Creates
        /// </summary>
        public UserPermission()
        {
        }
        
        public IPermission Intersect(IPermission target)
        {
            throw new NotImplementedException();
        }
        
        public IPermission Union(IPermission target)
        {
            throw new NotImplementedException();
        }
        
        public bool IsSubsetOf(IPermission target)
        {
            throw new NotImplementedException();
        }
        
        public void Demand()
        {
            throw new NotImplementedException();
        }
        
        public IPermission Copy()
        {
            throw new NotImplementedException();
        }
        
        public SecurityElement ToXml()
        {
            throw new NotImplementedException();
        }
        
        public void FromXml(SecurityElement e)
        {
            throw new NotImplementedException();
        }
    }
}
