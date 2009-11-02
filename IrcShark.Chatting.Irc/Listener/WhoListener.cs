// <copyright file="WhoListener.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the WhoListener class.</summary>

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
    /// This listener allows you to listen for a who reply.
    /// </summary>
    public class WhoListener : IIrcObject
    {
        /// <summary>
        /// Saves the client instance, what is listened on.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves all received lines for a who reply.
        /// </summary>
        private List<WhoLine> whoLines;
        
        /// <summary>
        /// Saves the current state of the listener.
        /// </summary>
        private bool isReading;

        /// <summary>
        /// Initializes a new instance of the WhoListener class.
        /// </summary>
        /// <param name="client">The client to listen on.</param>
        public WhoListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new IrcClient.LineReceivedEventHandler(HandleLine);
            whoLines = new List<WhoLine>();
        }
        
        /// <summary>
        /// The EventHandler for the <see cref="WhoBegin" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void WhoBeginEventHandler(object sender, WhoBeginEventArgs e);
        
        /// <summary>
        /// The EventHandler for the <see cref="WhoEnd" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void WhoEndEventHandler(object sender, WhoEndEventArgs e);

        /// <summary>
        /// This event is raised, when the listener receives a who reply begin.
        /// </summary>
        public event WhoBeginEventHandler WhoBegin;
        
        /// <summary>
        /// This event is raised, when the listener receives a who reply end.
        /// </summary>
        public event WhoEndEventHandler WhoEnd;
        
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
        /// Gets all lines belonging to the last received who reply.
        /// </summary>
        /// <value>An array of lines.</value>
        public WhoLine[] WhoLines
        {
            get { return whoLines.ToArray(); }
        }

        /// <summary>
        /// Gets a value indicating whether the listener currently receives a new who reply.
        /// </summary>
        /// <value>Its true, if the listener currently reads a who reply, false otherwise.</value>
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
                case 352:
                    whoLines.Add(new WhoLine(e.Line));
                    if (!IsReading)
                    {
                        isReading = true;
                        if (WhoBegin != null)
                        {
                            WhoBegin(this, new WhoBeginEventArgs(e.Line));
                        }
                    }
                    
                    break;
                    
                case 315:
                    if (WhoEnd != null)
                    {
                        WhoEnd(this, new WhoEndEventArgs(e.Line, WhoLines));
                    }
                    
                    isReading = false;
                    break;
            }
        }
    }
}
