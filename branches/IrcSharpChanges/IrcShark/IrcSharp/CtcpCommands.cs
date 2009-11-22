// <copyright file="CtcpCommands.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the CtcpCommands enum.</summary>

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
namespace IrcSharp 
{
    /// <summary>
    /// A listing of all well known ctcp commands.
    /// </summary>
    public enum CtcpCommands
    {
        /// <summary>
        /// The ctcp action command.
        /// </summary>
        Action,
        
        /// <summary>
        /// The ctcp version command.
        /// </summary>
        Version,
        
        /// <summary>
        /// An unknown ctcp command.
        /// </summary>
        Unkown,
        
        /// <summary>
        /// None means, the message is no ctcp message.
        /// </summary>
        None
    }
}