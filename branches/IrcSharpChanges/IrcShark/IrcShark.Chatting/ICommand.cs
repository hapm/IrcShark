// $Id$
//
// Add description here
//
// Benutzer: markus
// Datum: 16.11.2009
// Zeit: 23:50 
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
    /// Describes any chat command, that was received by an IConnection.
    /// </summary>
    /// <value>
    /// The protocol dependant name of the command.
    /// </value>
    public interface ICommand
    {
        /// <summary>
        /// Gets th protocol dependant name of the command.
        /// </summary>
        /// <value>The name of the command as a string.</value>
        string Name { get; }
    }
}
