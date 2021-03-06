﻿// <copyright file="InfoBeginEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the InfoBeginEventArgs class.</summary>

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
namespace IrcShark.Chatting.Irc.Listener
{
    using System;

    /// <summary>
    /// The InfoBeginEventArgs class is the EventArgs class for the InfoListener.InfoBegin event.
    /// </summary>
    public class InfoBeginEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the InfoBeginEventArgs class.
        /// </summary>
        /// <param name="line">The line, what raised the event.</param>
        public InfoBeginEventArgs(IrcLine line) : base(line)
        {
        }
    }
}
