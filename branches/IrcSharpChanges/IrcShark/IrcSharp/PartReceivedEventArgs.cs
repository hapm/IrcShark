// <copyright file="PartReceivedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the PartReceivedEventArgs class.</summary>

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
    /// The PartReceivedEventArgs belongs to the <see cref="PartReceivedEventHandler" /> and the <see cref="IrcClient.PartReceived" /> event.
    /// </summary>
    public class PartReceivedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the name of the parted channel.
        /// </summary>
        private string channelName;
        
        /// <summary>
        /// Saves the message for the part.
        /// </summary>
        private string partMessage;
        
        /// <summary>
        /// Saves the UserInfo instance for the user, who parted.
        /// </summary>
        private UserInfo user;

        /// <summary>
        /// Initializes a new instance of the PartReceivedEventArgs class.
        /// </summary>
        /// <param name="line">The line to create the event args from.</param>
        public PartReceivedEventArgs(IrcLine line) : base(line)
        {
            user = new UserInfo(line);
            channelName = line.Parameters[0];
            
            if (line.Parameters.Length > 1)
                partMessage = line.Parameters[1];
        }

        /// <summary>
        /// Initializes a new instance of the PartReceivedEventArgs class.
        /// </summary>
        /// <param name="channelName">The name of the channel.</param>
        /// <param name="partedUser">The user, who parted.</param>
        public PartReceivedEventArgs(string channelName, UserInfo partedUser) : base(partedUser.Client)
        {
            this.channelName = channelName;
            user = partedUser;
        }

        /// <summary>
        /// Gets the name of the parted channel.
        /// </summary>
        /// <value>
        /// The channelname as a string.
        /// </value>
        public string ChannelName
        {
            get { return channelName; }
        }

        /// <summary>
        /// Gets the part message send by the parted user.
        /// </summary>
        /// <value>The part message as a string.</value>
        public string PartMessage
        {
            get { return partMessage; }
        }

        /// <summary>
        /// Gets the instance of the UserInfo of the user, who parted.
        /// </summary>
        /// <value>The UserInfo instance.</value>
        public UserInfo User
        {
            get { return user; }
        }
    }
}
