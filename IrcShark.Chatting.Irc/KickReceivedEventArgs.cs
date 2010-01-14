// <copyright file="KickReceivedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the KickReceivedEventArgs class.</summary>

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
    using System;

    /// <summary>
    /// Description of KickReceivedEventArgs.
    /// </summary>
    public class KickReceivedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the info of the user who kicked the other.
        /// </summary>
        private UserInfo kicker;
        
        /// <summary>
        /// Saves the nickname of the user who was kicked.
        /// </summary>
        private string kickedName;
        
        /// <summary>
        /// Saves the name of the channel where the kick happened.
        /// </summary>
        private string channelName;
        
        /// <summary>
        /// Saves the reason message given for this kick.
        /// </summary>
        private string reason;

        /// <summary>
        /// Initializes a new instance of the KickReceivedEventArgs class.
        /// </summary>
        /// <param name="line">The line with the kick command.</param>
        public KickReceivedEventArgs(IrcLine line) : base(line)
        {
            kicker = new UserInfo(line);
            kickedName = line.Parameters[1];
            channelName = line.Parameters[0];
            reason = line.Parameters[2];
        }

        /// <summary>
        /// Gets the info about the user who kicked the other.
        /// </summary>
        /// <value>The UserInfo about the kicker.</value>
        public UserInfo Kicker
        {
            get { return kicker; }
        }

        /// <summary>
        /// Gets the nickname of the kicked user.
        /// </summary>
        /// <value>The name as a string.</value>
        public string KickedName
        {
            get { return kickedName; }
        }

        /// <summary>
        /// Gets the name of the channel where the kick happend.
        /// </summary>
        /// <value>The name as a string.</value>
        public string ChannelName
        {
            get { return channelName; }
        }

        /// <summary>
        /// Gets the reason specified by the kicker.
        /// </summary>
        /// <value>The reason as a string.</value>
        public string Reason
        {
            get { return reason; }
        }
    }
}
