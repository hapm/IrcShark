// <copyright file="Dependency.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Dependency class.</summary>

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

namespace IrcShark.Extensions
{
    /// <summary>
    /// Represents the dependency needed for an extension to work properly.
    /// </summary>
    [Serializable]
    public class Dependency
    {
        /// <summary>
        /// Saves the guid of the depended extension.
        /// </summary>
        private Guid guid;
        
        /// <summary>
        /// Initializes a new instance of the Dependency class.
        /// </summary>
        /// <param name="guid">The guid of the extension to depend on.</param>
        public Dependency(Guid guid)
        {
            this.guid = guid;
        }
        
        /// <summary>
        /// Gets the guid of the extension of this dependency.
        /// </summary>
        public Guid Guid
        {
            get { return guid; }
        }
    }
}
