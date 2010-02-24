// <copyright file="User.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the User class.</summary>

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
    using System.Collections.Generic;

    /// <summary>
    /// An instance of the User class represents a user on the IRC network.
    /// </summary>
    /// <remarks>
    /// A User instance gives more information about a user, than a UserInfo instance.
    /// You can check see, in what channels the user is, if you are in the channel too.
    /// </remarks>
    public class User : UserInfo
    {
        /// <summary>
        /// Saves a list of all current channels.
        /// </summary>
        private List<string> channels;
        
        /// <summary>
        /// Saves if the user is currently away.
        /// </summary>
        private bool isAway;
        
        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        /// <param name="info">The UserInfo of the user to watch.</param>
        /// <param name="channels">The channels the user is currently in.</param>
        public User(UserInfo info, string[] channels) : base(info)
        {
            isAway = false;
            this.channels = new List<string>();
            this.channels.AddRange(channels);
            Client.JoinReceived += new IrcClient.JoinReceivedEventHandler(Client_JoinReceived);
        }
        
        /// <summary>
        /// Gets a list of all known channels, the user is in.
        /// </summary>
        /// <value>An array with the channel names as a string.</value>
        public string[] Channels
        {
            get { return channels.ToArray(); }
        }
        
        /// <summary>
        /// Gets a value indicating whether the user is away at the moment or not.
        /// </summary>
        /// <value>Its true if the user is away, false otherwise.</value>
        public bool Away
        {
            get { return isAway; }
        }
        
        /// <summary>
        /// Checks if the user is in the given channel.
        /// </summary>
        /// <param name="channelName">The name of the channel to check.</param>
        /// <returns>Returns true if the user is in the given channel, false otherwise.</returns>
        public bool IsIn(string channelName)
        {
            return channels.Contains(channelName);
        }

        /// <summary>
        /// Handles received joins and add the user to channels, if it has joined a new one.
        /// </summary>
        /// <param name="sender">The client what raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Client_JoinReceived(object sender, JoinReceivedEventArgs e)
        {
            if (e.User.Equals(this)) 
            {
                channels.Add(e.ChannelName);
            }
        }
    }
}
