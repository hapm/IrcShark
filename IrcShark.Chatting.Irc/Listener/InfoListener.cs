// <copyright file="InfoListener.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the InfoListener class.</summary>

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
    /// The InfoListener reads a complete info block from an IrcClient.
    /// </summary>
    public class InfoListener : IIrcObject
    {
        /// <summary>
        /// Saves the client instance, what is listened on.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves all received lines for an info block.
        /// </summary>
        private List<IrcLine> infoLines;
        
        /// <summary>
        /// Saves the current state of the listener.
        /// </summary>
        private bool isReading;

        /// <summary>
        /// Initializes a new instance of the InfoListener class.
        /// </summary>
        /// <param name="client">The client to listen on.</param>
        public InfoListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new IrcClient.LineReceivedEventHandler(HandleLine);
            infoLines = new List<IrcLine>();
        }
        
        /// <summary>
        /// The EventHandler for the <see cref="InfoBegin" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void InfoBeginEventHandler(object sender, InfoBeginEventArgs e);
        
        /// <summary>
        /// The EventHandler for the <see cref="InfoEnd" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void InfoEndEventHandler(object sender, InfoEndEventArgs e);
        
        /// <summary>
        /// This event is raised, when the listener receives an info block begin.
        /// </summary>
        public event InfoBeginEventHandler InfoBegin;
        
        /// <summary>
        /// This event is raised, when the listener receives an info block end.
        /// </summary>
        public event InfoEndEventHandler InfoEnd;

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
        /// Gets all lines belonging to the last received info block.
        /// </summary>
        /// <value>An array of lines.</value>
        public IrcLine[] InfoLines
        {
            get { return infoLines.ToArray(); }
        }

        /// <summary>
        /// Gets a value indicating whether the listener currently receives a new info block.
        /// </summary>
        /// <value>Its true, if the listener currently reads an info block, false otherwise.</value>
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
                case 371:
                    infoLines.Add(e.Line);
                    if (!IsReading)
                    {
                        isReading = true;
                        if (InfoBegin != null)
                        {
                            InfoBegin(this, new InfoBeginEventArgs(e.Line));
                        }
                    }
                    
                    break;
                    
                case 374:
                    infoLines.Add(e.Line);
                    if (InfoEnd != null)
                    {
                        InfoEnd(this, new InfoEndEventArgs(e.Line, InfoLines));
                    }
                    
                    isReading = false;
                    break;
            }
        }
    }
}