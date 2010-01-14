// <copyright file="ModeArt.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ModeArt enum.</summary>

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
namespace IrcShark.Chatting.Irc
{
    /// <summary>
    /// The ModeArt describes if a mode can be set to a channel or to a user.
    /// </summary>
    /// <remarks>
    /// IRC allows to set modes to channels an users, but you can't set the same mode
    /// on a user and on a channel.
    /// </remarks>
    public enum ModeArt
    {
        /// <summary>
        /// The mode can be applied to users.
        /// </summary>
        User,
        
        /// <summary>
        /// The mode can be applied to channels.
        /// </summary>
        Channel
    }
}
