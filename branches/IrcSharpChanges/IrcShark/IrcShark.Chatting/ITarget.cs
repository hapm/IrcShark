// $Id$
//
// Add description here
//
// Benutzer: markus
// Datum: 17.11.2009
// Zeit: 00:23 
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
//
// Erstellt mit SharpDevelop.
namespace IrcShark.Chatting
{
    using System;

    /// <summary>
    /// Specifies any target that can be addressed in a chat protocol.
    /// </summary>
    public interface ITarget
    {
        /// <summary>
        /// Gets the name of the target.
        /// </summary>
        /// <value>The name of the target as a string.</value>
        string Name { get; }
    }
}
