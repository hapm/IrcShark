// <copyright file="UserJoinEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the UserJoinEventArgs class.</summary>

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
namespace IrcShark.Chatting.Irc.Extended
{
    using System;

    /// <summary>
    /// The UserJoinEventArgs are used by the <see cref="Channel.UserJoin" /> event.
    /// </summary>
    public class UserJoinEventArgs : JoinReceivedEventArgs
    {
        /// <summary>
        /// Saves the user who joined.
        /// </summary>
        private ChannelUser user;

        /// <summary>
        /// Initializes a new instance of the UserJoinEventArgs class.
        /// </summary>
        /// <param name="user">The user that joined.</param>
        /// <param name="baseArgs">The event args for the base join event.</param>
        public UserJoinEventArgs(ChannelUser user, JoinReceivedEventArgs baseArgs)
            : base(baseArgs.Line)
        {
            this.user = user;
        }

        /// <summary>
        /// Gets the user, who joined the channel.
        /// </summary>
        /// <value>The ChannelUser instance for the joined user.</value>
        public ChannelUser ChannelUser
        {
            get { return user; }
        }
    }
}
