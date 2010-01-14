// <copyright file="Channel.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Channel class.</summary>

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
    using System.Collections.ObjectModel;
    using IrcShark.Chatting;

    /// <summary>
    /// Represents a channel of an irc network.
    /// </summary>
    public class Channel : IRoom, IIrcObject, IDisposable
    {
        /// <summary>
        /// Saves the client, this channel belongs to.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves the name of the room.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves the read only collection of all users in the channel.
        /// </summary>
        private ChannelUserCollection users;
        
        /// <summary>
        /// Saves a list of all users in the channel.
        /// </summary>
        private List<ChannelUser> userList;
        
        /// <summary>
        /// Initializes a new instance of the Channel class.
        /// </summary>
        /// <param name="client">The client this channel belongs to.</param>
        /// <param name="name">The name of the channel.</param>
        public Channel(IrcClient client, string name)
        {
            this.client = client;
            this.name = name;
            this.userList = new List<ChannelUser>();
            this.users = new ChannelUserCollection(this, userList);
            Client.JoinReceived += new IrcClient.JoinReceivedEventHandler(Client_JoinReceived);
            Client.PartReceived += new IrcClient.PartReceivedEventHandler(Client_PartReceived);
            Client.QuitReceived += new IrcClient.QuitReceivedEventHandler(Client_QuitReceived);
            Client.KickReceived += new IrcClient.KickReceivedEventHandler(Client_KickReceived);
        }
        
        /// <summary>
        /// Initializes a new instance of the Channel class.
        /// </summary>
        /// <param name="e">The arguments that where received as a join.</param>
        public Channel(JoinReceivedEventArgs e)
        {
            this.client = e.Client;
            this.name = e.ChannelName;
            this.userList = new List<ChannelUser>();
            this.users = new ChannelUserCollection(this, userList);
            Client.JoinReceived += new IrcClient.JoinReceivedEventHandler(Client_JoinReceived);
            Client.PartReceived += new IrcClient.PartReceivedEventHandler(Client_PartReceived);
            Client.QuitReceived += new IrcClient.QuitReceivedEventHandler(Client_QuitReceived);
            Client.KickReceived += new IrcClient.KickReceivedEventHandler(Client_KickReceived);
        }
        
        /// <summary>
        /// The event handler for the <see cref="UserJoin" /> event.
        /// </summary>
        /// <param name="sender">The sender of the UserJoin event.</param>
        /// <param name="e">The arguments for the event.</param>
        public delegate void UserJoinEventHandler(object sender, UserJoinEventArgs e);
        
        /// <summary>
        /// The event handler for the <see cref="UserLeave" /> event.
        /// </summary>
        /// <param name="sender">The sender of the UserLeave event.</param>
        /// <param name="e">The arguments for the event.</param>
        public delegate void UserLeaveEventHandler(object sender, UserLeaveEventArgs e);
        
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
        public delegate void LeftEventHandler(object sender, LeftEventArgs e);
        
        /// <summary>
        /// This event is raised when a user joins the channel.
        /// </summary>
        public event UserJoinEventHandler UserJoin;
        
        /// <summary>
        /// This event is raised when a user leaves a channel.
        /// </summary>
        public event UserLeaveEventHandler UserLeave;

        /// <summary>
        /// The Joined event is raised when the handled connection joins a new channel.
        /// </summary>
        public event JoinedEventHandler Joined;

        /// <summary>
        /// The Parted event is raised when the handled connection parts a new channel.
        /// </summary>
        public event LeftEventHandler Left;
        
        /// <summary>
        /// Gets the IrcClient, this Channel belongs to.
        /// </summary>
        /// <value>
        /// The <see cref="IrcClient"/> this Channel belongs to.
        /// </value>
        public IrcClient Client
        {
            get { return client; }
        }
        
        /// <summary>
        /// Gets the name of the room.
        /// </summary>
        /// <value>The name as a string.</value>
        public string Name 
        {
            get { return name; }
        }
        
        /// <summary>
        /// Gets all users in the channel.
        /// </summary>
        /// <value>A readonly collection of all users.</value>
        public ChannelUserCollection Users
        {
            get { return users; }
        }

        /// <summary>
        /// Disposes the Channel.
        /// </summary>
        public void Dispose()
        {
            UserJoin = null;
            UserLeave = null;
        }

        /// <summary>
        /// Handles the <see cref="IrcClient.JoinReceived" /> event.
        /// </summary>
        /// <param name="sender">The sender of the join event.</param>
        /// <param name="e">The event args.</param>
        private void Client_JoinReceived(object sender, JoinReceivedEventArgs e)
        {
            if (e.ChannelName != Name)
                return;
            ChannelUser newUser = new ChannelUser(this, e.User.NickName);
            userList.Add(newUser);
            if (e.User.Equals(Client.Self))
                OnJoined(new JoinedEventArgs(this));
            else
                OnUserJoin(new UserJoinEventArgs(newUser, e));
        }

        /// <summary>
        /// Handels the <see cref="IrcClient.PartReceived" /> event.
        /// </summary>
        /// <param name="sender">The sender of the part event.</param>
        /// <param name="e">The event args.</param>
        private void Client_PartReceived(object sender, PartReceivedEventArgs e)
        {
            if (e.ChannelName != Name) 
                return;
            if (e.User.Equals(Client.Self))
                OnLeft(new LeftEventArgs(this));
            else
            {
                foreach (ChannelUser user in userList) 
                {
                    if (user.Name == e.User.NickName)
                    {
                        userList.Remove(user);
                        e.Handled = true;
                        OnUserLeave(new UserLeaveEventArgs(user, e.PartMessage, UserLeaveReason.Parted));
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Handels the <see cref="IrcClient.QuitReceived" /> event.
        /// </summary>
        /// <param name="sender">The sender of the quit event.</param>
        /// <param name="e">The event args.</param>
        private void Client_QuitReceived(object sender, QuitReceivedEventArgs e)
        {
            if (e.User.Equals(Client.Self))
                OnLeft(new LeftEventArgs(this));
            else
            {
                foreach (ChannelUser user in userList) 
                {
                    if (user.Name == e.User.NickName)
                    {
                        userList.Remove(user);
                        e.Handled = true;
                        OnUserLeave(new UserLeaveEventArgs(user, e.QuitMessage, UserLeaveReason.Quit));
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Handels the <see cref="IrcClient.KickReceived" /> event.
        /// </summary>
        /// <param name="sender">The sender of the kick event.</param>
        /// <param name="e">The event args.</param>
        private void Client_KickReceived(object sender, KickReceivedEventArgs e)
        {
            if (e.ChannelName != Name) 
                return;
            if (e.KickedName == Client.Self.NickName)
                OnLeft(new LeftEventArgs(this));
            else
            {
                foreach (ChannelUser user in userList) 
                {
                    if (user.Name == e.KickedName)
                    {
                        userList.Remove(user);
                        e.Handled = true;
                        OnUserLeave(new UserLeaveEventArgs(user, e.Reason, UserLeaveReason.Kicked));
                        return;
                    }
                }
            }
        }
        
        /// <summary>
        /// Raises the Joined event.
        /// </summary>
        /// <param name="args">The arguments to use for the event.</param>
        private void OnJoined(JoinedEventArgs args)
        {
            if (Joined != null)
                Joined(this, args);
        }
        
        /// <summary>
        /// Raises the Left event.
        /// </summary>
        /// <param name="args">The arguments to use for the event.</param>
        private void OnLeft(LeftEventArgs args)
        {
            if (Left != null)
                Left(this, args);
        }
        
        /// <summary>
        /// Raises the UserJoin event.
        /// </summary>
        /// <param name="args">The arguments to use for the event.</param>
        private void OnUserJoin(UserJoinEventArgs args)
        {
            if (UserJoin != null)
                UserJoin(this, args);            
        }
        
        /// <summary>
        /// Raises the UserLeave event.
        /// </summary>
        /// <param name="args">The arguments to use for the event.</param>
        private void OnUserLeave(UserLeaveEventArgs args)
        {
            if (UserLeave != null)
                UserLeave(this, args);
        }
        
        /// <summary>
        /// Represents a collection of Users.
        /// </summary>
        public class ChannelUserCollection : ReadOnlyCollection<ChannelUser>
        {
            /// <summary>
            /// Saves the channel, this collection belongs to.
            /// </summary>
            private Channel channel;
            
            /// <summary>
            /// Initializes a new instance of the ChannelUserCollection class.
            /// </summary>
            /// <param name="chan">The channel, the collection belongs to.</param>
            /// <param name="users">The list of users.</param>
            public ChannelUserCollection(Channel chan, IList<ChannelUser> users) : base(users) 
            {
                channel = chan;
            }
            
            /// <summary>
            /// Gets the channel this collection belongs to.
            /// </summary>
            /// <value>The channel instance.</value>
            public Channel Channel
            {
                get { return channel; }
            }
        }
    }
}
