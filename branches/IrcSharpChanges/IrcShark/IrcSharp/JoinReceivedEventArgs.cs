// <copyright file="JoinReceivedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the JoinReceivedEventArgs class.</summary>

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
    using System;

    /// <summary>
    /// The JoinReceivedEventArgs belongs to the <see cref="JoinReceivedEventHandler" /> and the <see cref="IrcClient.JoinReceived" /> event.
    /// </summary>
    public class JoinReceivedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the name of the channel, the user joined.
        /// </summary>
        private string channelName;
        
        /// <summary>
        /// Saves an instance of the user, who joined.
        /// </summary>
        private UserInfo user;

        /// <summary>
        /// Initializes a new instance of the JoinReceivedEventArgs class.
        /// </summary>
        /// <param name="line">The line to create the event args from.</param>
        public JoinReceivedEventArgs(IrcLine line) : base(line)
        {
            user = new UserInfo(line);
            channelName = line.Parameters[0];
        }

        /// <summary>
        /// Initializes a new instance of the JoinReceivedEventArgs class.
        /// </summary>
        /// <param name="channelName">The name of the joined channel.</param>
        /// <param name="joinedUser">The UserInfo for the joined user.</param>
        public JoinReceivedEventArgs(string channelName, UserInfo joinedUser) : base(joinedUser.Client)
        {
            this.channelName = channelName;
            user = joinedUser;
        }

        /// <summary>
        /// Gets the name of the channel, the user joined.
        /// </summary>
        /// <value>
        /// The roomname as a string.
        /// </value>
        public string ChannelName
        {
            get { return channelName; }
        }

        /// <summary>
        /// Gets the user, who joined.
        /// </summary>
        /// <value>
        /// Information about the user as a UserInfo.
        /// </value>
        public UserInfo User
        {
            get { return user; }
        }
    }
}
