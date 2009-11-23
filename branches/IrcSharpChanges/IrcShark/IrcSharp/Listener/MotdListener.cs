// <copyright file="MotdListener.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the MotdListener class.</summary>

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
namespace IrcSharp.Listener
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This listener allows you to listen for the server motd.
    /// </summary>
    public class MotdListener : IIrcObject
    {
        /// <summary>
        /// Saves the client instance, what is listened on.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves all received lines for a motd.
        /// </summary>
        private List<IrcLine> motdLines;
        
        /// <summary>
        /// Saves the current state of the listener.
        /// </summary>
        private bool isReading;

        /// <summary>
        /// Initializes a new instance of the MotdListener class.
        /// </summary>
        /// <param name="client">The client to listen on.</param>
        public MotdListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new IrcClient.LineReceivedEventHandler(HandleLine);
            motdLines = new List<IrcLine>();
        }
        
        /// <summary>
        /// The EventHandler for the <see cref="MotdBegin" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void MotdBeginEventHandler(object sender, MotdBeginEventArgs e);
        
        /// <summary>
        /// The EventHandler for the <see cref="MotdEnd" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void MotdEndEventHandler(object sender, MotdEndEventArgs e);

        /// <summary>
        /// This event is raised, when the listener receives a motd begin.
        /// </summary>
        public event MotdBeginEventHandler MotdBegin;
        
        /// <summary>
        /// This event is raised, when the listener receives a motd end.
        /// </summary>
        public event MotdEndEventHandler MotdEnd;

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
        /// Gets all lines belonging to the last received motd.
        /// </summary>
        /// <value>An array of lines.</value>
        public IrcLine[] MotdLines
        {
            get { return motdLines.ToArray(); }
        }

        /// <summary>
        /// Gets a value indicating whether the listener currently receives a new motd.
        /// </summary>
        /// <value>Its true, if the listener currently reads a motd, false otherwise.</value>
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
            if (!IsReading && e.Line.Numeric != 375) return;
            switch (e.Line.Numeric)
            {
                case 375:
                    isReading = true;
                    motdLines.Clear();
                    motdLines.Add(e.Line);
                    if (MotdBegin != null)
                        MotdBegin(this, new MotdBeginEventArgs(e.Line));
                    break;
                    
                case 372:
                    motdLines.Add(e.Line);
                    break;
                    
                case 376:
                    motdLines.Add(e.Line);
                    if (MotdEnd != null)
                        MotdEnd(this, new MotdEndEventArgs(e.Line, MotdLines));
                    isReading = false;
                    break;
            }
        }
    }
}
