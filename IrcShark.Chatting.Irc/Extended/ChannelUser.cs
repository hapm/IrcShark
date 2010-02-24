// <copyright file="ChannelUser.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChannelUser class.</summary>

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
namespace IrcShark.Chatting.Irc.Extended
{
    using System;

    /// <summary>
    /// The ChannelUser is used by the <see cref="Channel" /> class to represent a user.
    /// </summary>
    public class ChannelUser : IIrcObject
    {
        /// <summary>
        /// Saves the channel, the user is in.
        /// </summary>
        private Channel channel;
        
        /// <summary>
        /// Saves the name of the user.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Initializes a new instance of the ChannelUser class.
        /// </summary>
        /// <param name="chan">The channel the user belongs to.</param>
        /// <param name="name">The name of the user.</param>
        public ChannelUser(Channel chan, string name)
        {
            this.channel = chan;
            this.name = name;
        }
        
        /// <summary>
        /// Gets the channel, the user is in.
        /// </summary>
        /// <value>The channel instance.</value>
        public Channel Channel
        {
            get { return channel; }
        }
        
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name as a string.</value>
        public string Name
        {
            get { return name; }
        }

        #region IIrcObject implementation
        /// <summary>
        /// Gets the irc connection, the ChannelUser belongs to.
        /// </summary>
        /// <value>The irc connection.</value>
        public IrcClient Client 
        {
            get { return channel.Client; }
        }
        #endregion
    }
}
