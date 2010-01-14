// <copyright file="UserLeaveEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the UserLeaveEventArgs class.</summary>

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
namespace IrcSharp.Extended
{
    using System;

    /// <summary>
    /// The UserLeaveReason enum represents the reason why a user have left a channel.
    /// </summary>
    public enum UserLeaveReason
    {
        /// <summary>
        /// The user have parted the channel on a normal way.
        /// </summary>
        Parted,
        
        /// <summary>
        /// The user have fully disconnected from the server.
        /// </summary>
        Quit,
        
        /// <summary>
        /// The user has been forcefully removed from the channel.
        /// </summary>
        Kicked        
    }
    
    /// <summary>
    /// The UserLeaveEventArgs are used by the <see cref="IrcSharp.Extended.Channel.UserLeave" /> event.
    /// </summary>
    public class UserLeaveEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the user that left the channel.
        /// </summary>
        private ChannelUser user;
        
        /// <summary>
        /// Saves the reason why the user have left.
        /// </summary>
        private UserLeaveReason reason;
        
        /// <summary>
        /// Saves the leaving message.
        /// </summary>
        private string msg;

        /// <summary>
        /// Initializes a new instance of the UserLeaveEventArgs class.
        /// </summary>
        /// <param name="user">The user, that have left the channel.</param>
        /// <param name="msg">The last message belonging to the leaving reason.</param>
        /// <param name="reason">The reason why the user have left.</param>
        public UserLeaveEventArgs(ChannelUser user, string msg, UserLeaveReason reason) : base(user.Client)
        {
            this.user = user;
            this.reason = reason;
            this.msg = msg;
        }

        /// <summary>
        /// Gets the Channel, the user has parted from.
        /// </summary>
        /// <value>The channel instance.</value>
        public Channel Channel
        {
            get { return user.Channel; }
        }

        /// <summary>
        /// Gets the user that left the channel.
        /// </summary>
        /// <value>The ChannelUser instance.</value>
        public ChannelUser ChannelUser
        {
            get { return user; }
        }
        
        /// <summary>
        /// Gets the message, that belongs to the leaving reason.
        /// </summary>
        /// <value>The message as a string.</value>
        public string Message
        {
            get { return msg; }
        }

        /// <summary>
        /// Gets the reason why the user left the channel.
        /// </summary>
        /// <value>The reason why the user left.</value>
        public UserLeaveReason Reason
        {
            get { return reason; }
        }
    }
}
