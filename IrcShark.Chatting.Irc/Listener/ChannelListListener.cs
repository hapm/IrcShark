// <copyright file="ChannelListListener.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChannelListListener class.</summary>

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
namespace IrcShark.Chatting.Irc.Listener
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This listener allows you to listen for a channel list, requested by a LIST command.
    /// </summary>
    public class ChannelListListener : IIrcObject
    {
        /// <summary>
        /// Saves the client instance, what is listened on.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves all received lines for a channel list block.
        /// </summary>
        private List<ChannelListLine> channelListLines;
        
        /// <summary>
        /// Saves the current state of the listener.
        /// </summary>
        private bool isReading;

        /// <summary>
        /// Initializes a new instance of the ChannelListListener class.
        /// </summary>
        /// <param name="client">The client to listen on.</param>
        public ChannelListListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new IrcClient.LineReceivedEventHandler(HandleLine);
            channelListLines = new List<ChannelListLine>();
        }
        
        /// <summary>
        /// The EventHandler for the <see cref="ChannelListBegin" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void ChannelListBeginEventHandler(object sender, ChannelListBeginEventArgs e);
        
        /// <summary>
        /// The EventHandler for the <see cref="ChannelListEnd" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void ChannelListEndEventHandler(object sender, ChannelListEndEventArgs e);
        
        /// <summary>
        /// This event is raised, when the listener receives a channel list block begin.
        /// </summary>
        public event ChannelListBeginEventHandler ChannelListBegin;
        
        /// <summary>
        /// This event is raised, when the listener receives a channel list block end.
        /// </summary>
        public event ChannelListEndEventHandler ChannelListEnd;

        /// <summary>
        /// Gets the <see cref="IrcClient"/>, this object is associated to.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public IrcClient Client
        {
            get { return client; }
        }

        /// <summary>
        /// Gets all lines belonging to the last received channel list block.
        /// </summary>
        /// <value>An array of lines.</value>
        public ChannelListLine[] ChannelListLines
        {
            get { return channelListLines.ToArray(); }
        }

        /// <summary>
        /// Gets a value indicating whether the listener currently receives a new channel list block.
        /// </summary>
        /// <value>Its true, if the listener currently reads a channel list block, false otherwise.</value>
        public bool IsReading
        {
            get { return isReading; }
        }

        /// <summary>
        /// Handles a received line from the server.
        /// </summary>
        /// <param name="sender">The client, the message was received from.</param>
        /// <param name="e">The event argument, holding the received line.</param>
        private void HandleLine(object sender, LineReceivedEventArgs e)
        {
            if (!e.Line.IsNumeric) return;
            if (!IsReading && e.Line.Numeric != 321) return;
            switch (e.Line.Numeric)
            {
                case 321:
                    isReading = true;
                    if (ChannelListBegin != null)
                        ChannelListBegin(this, new ChannelListBeginEventArgs(e.Line));
                    break;
                case 322:
                    channelListLines.Add(new ChannelListLine(e.Line));
                    break;
                case 323:
                    if (ChannelListEnd != null)
                        ChannelListEnd(this, new ChannelListEndEventArgs(e.Line, ChannelListLines));
                    isReading = false;
                    break;
            }
        }
    }
}
