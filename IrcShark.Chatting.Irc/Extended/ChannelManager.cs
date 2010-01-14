// <copyright file="ChannelManager.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChannelManager class.</summary>

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
    using System.Collections.Generic;

    /// <summary>
    /// The ChannelManager manages all channels the current IrcClient is in.
    /// </summary>
    /// <remarks>
    /// The ChannelManager needs to be applied to the IrcClient instance
    /// before any channel is joined to avoid bad behavior.
    /// </remarks>
    [Serializable]
    public class ChannelManager : Dictionary<string, Channel>, IIrcObject
    {
        /// <summary>
        /// The client, this ChannelManager belongs to.
        /// </summary>
        private IrcClient client;

        /// <summary>
        /// Initializes a new instance of the ChannelManager class.
        /// </summary>
        /// <param name="client">The client, the manager runs for.</param>
        public ChannelManager(IrcClient client)
        {
            this.client = client;
            Client.JoinReceived += new IrcClient.JoinReceivedEventHandler(Client_JoinReceived);
        }
        
        /// <summary>
        /// The handler for the ChannelManager.Joined event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void JoinedEventHandler(object sender, JoinedEventArgs e);
        
        /// <summary>
        /// The handler for the ChannelManager.Parted event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void PartedEventHandler(object sender, LeftEventArgs e);

        /// <summary>
        /// The Joined event is raised when the handled connection joins a new channel.
        /// </summary>
        public event JoinedEventHandler Joined;

        /// <summary>
        /// The Parted event is raised when the handled connection parts a new channel.
        /// </summary>
        public event PartedEventHandler Parted;

        #region IIrcObject Member
        /// <summary>
        /// Gets the IrcClient, this ChannelManager belongs to.
        /// </summary>
        /// <value>
        /// The <see cref="IrcClient"/> this ChannelManager belongs to.
        /// </value>
        public IrcClient Client
        {
            get { return client; }
        }
        #endregion

        /// <summary>
        /// Gets multible channels by their name.
        /// </summary>
        /// <param name="channelNames">A list of channelnames to get Channels for.</param>
        /// <returns>An array of all found Channel instances.</returns>
        public Channel[] ChannelsByList(string[] channelNames)
        {
            List<Channel> result = new List<Channel>();
            foreach (string ch in channelNames)
            {
                if (ContainsKey(ch)) 
                    result.Add(this[ch]);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Handles the <see cref="IrcClient.JoinReceived" /> event.
        /// </summary>
        /// <param name="sender">The sender of the join event.</param>
        /// <param name="e">The event args.</param>
        private void Client_JoinReceived(object sender, JoinReceivedEventArgs e)
        {
            if (e.User.NickName == Client.Self.NickName)
            {
                if (ContainsKey(e.ChannelName))
                    return;
                Channel newChannel = new Channel(e);
                newChannel.Joined += new Channel.JoinedEventHandler(Channel_Joined);
                newChannel.Left += new Channel.LeftEventHandler(Channel_Left);
                Add(newChannel.Name, newChannel);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the <see cref="Channel.Joined" /> event.
        /// </summary>
        /// <param name="sender">The sender of the joined event.</param>
        /// <param name="e">The event args.</param>
        private void Channel_Joined(object sender, JoinedEventArgs e)
        {
            if (!ContainsKey(e.Channel.Name))
            {
                Add(e.Channel.Name, e.Channel);
            }
            if (Joined != null) 
                Joined(this, e);
        }

        /// <summary>
        /// Handles the <see cref="Channel.Left" /> event.
        /// </summary>
        /// <param name="sender">The sender of the parted event.</param>
        /// <param name="e">The event args.</param>
        private void Channel_Left(object sender, LeftEventArgs e)
        {
            if (!ContainsKey(e.Channel.Name)) 
                return;
            if (Parted != null) 
                Parted(this, e);
            Remove(e.Channel.Name);
            e.Channel.Dispose();
        }
    }
}
