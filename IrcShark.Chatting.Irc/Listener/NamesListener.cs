// <copyright file="NamesListener.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the NamesListener class.</summary>

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
    /// This listener allows you to listen for a names reply.
    /// </summary>
    /// <remarks>The reply will be captured to the end, and you will be informed when the end is reached.</remarks>
    public class NamesListener : IIrcObject
    {
        /// <summary>
        /// Saves the client instance, what is listened on.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves all received names for a names block.
        /// </summary>
        private List<string> names;
        
        /// <summary>
        /// Saves the current state of the listener.
        /// </summary>
        private bool isReading;

        /// <summary>
        /// Saves the name of the channel, the names reply is send for.
        /// </summary>
        private string channelName;

        /// <summary>
        /// Initializes a new instance of the NamesListener class.
        /// </summary>
        /// <param name="client">The client to listen on.</param>
        public NamesListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new IrcClient.LineReceivedEventHandler(HandleLine);
            names = new List<string>();
        }
        
        /// <summary>
        /// The EventHandler for the <see cref="NamesBegin" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void NamesBeginEventHandler(object sender, NamesBeginEventArgs e);
        
        /// <summary>
        /// The EventHandler for the <see cref="NamesEnd" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void NamesEndEventHandler(object sender, NamesEndEventArgs e);

        /// <summary>
        /// This event is raised, when the listener receives a names list begin.
        /// </summary>
        public event NamesBeginEventHandler NamesBegin;
        
        /// <summary>
        /// This event is raised, when the listener receives a names list end.
        /// </summary>
        public event NamesEndEventHandler NamesEnd;

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
        /// Gets all names belonging to the last received names list.
        /// </summary>
        /// <value>An array of strings.</value>
        public string[] Names
        {
            get { return names.ToArray(); }
        }

        /// <summary>
        /// Gets a value indicating whether the listener currently receives a new names list.
        /// </summary>
        /// <value>Its true, if the listener currently reads a names list, false otherwise.</value>
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
            if (!e.Line.IsNumeric)
            {
                return;
            }
            
            switch (e.Line.Numeric)
            {
                case 353:
                    channelName = e.Line.Parameters[2];

                    foreach (string s in e.Line.Parameters[3].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        names.Add(s);
                    }

                    if (!IsReading)
                    {
                        isReading = true;
                        if (NamesBegin != null)
                        {
                            NamesBegin(this, new NamesBeginEventArgs(e.Line));
                        }
                    }
                    
                    break;
                    
                case 366:
                    if (NamesEnd != null)
                    {
                        NamesEnd(this, new NamesEndEventArgs(e.Line, Names, channelName));
                    }
                    
                    isReading = false;
                    break;
            }
        }
    }
}
