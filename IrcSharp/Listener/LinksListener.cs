// <copyright file="LinksListener.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the link listener class.</summary>

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
    /// This listener allows you to listen for a link reply.
    /// </summary>
    /// <remarks>The reply will be captured to the end, and you will be informed when the end is reached.</remarks>
    public class LinksListener : IIrcObject
    {
        /// <summary>
        /// Saves the client instance, what is listened on.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves all received lines for a link block.
        /// </summary>
        private List<IrcLine> linksLines;
        
        /// <summary>
        /// Saves the current state of the listener.
        /// </summary>
        private bool isReading;

        /// <summary>
        /// Initializes a new instance of the LinksListener class.
        /// </summary>
        /// <param name="client">The client to listen on.</param>
        public LinksListener(IrcClient client)
        {
            this.client = client;
            client.LineReceived += new IrcClient.LineReceivedEventHandler(HandleLine);
            linksLines = new List<IrcLine>();
        }
        
        /// <summary>
        /// The EventHandler for the <see cref="LinksBegin" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void LinksBeginEventHandler(object sender, LinksBeginEventArgs e);
        
        /// <summary>
        /// The EventHandler for the <see cref="LinksEnd" /> event.
        /// </summary>
        /// <param name="sender">The IrcClient instance, that fired the event.</param>
        /// <param name="e">The event args for this event.</param>
        public delegate void LinksEndEventHandler(object sender, LinksEndEventArgs e);
        
        /// <summary>
        /// This event is raised, when the listener receives a link block begin.
        /// </summary>
        public event LinksBeginEventHandler LinksBegin;
        
        /// <summary>
        /// This event is raised, when the listener receives a link block end.
        /// </summary>
        public event LinksEndEventHandler LinksEnd;

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
        /// Gets all lines belonging to the last received links block.
        /// </summary>
        /// <value>An array of lines.</value>
        public IrcLine[] LinksLines
        {
            get { return linksLines.ToArray(); }
        }

        /// <summary>
        /// Gets a value indicating whether the listener currently receives a new links block.
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
            if (!e.Line.IsNumeric) return;
            switch (e.Line.Numeric)
            {
                case 364:
                    linksLines.Add(e.Line);
                    if (!IsReading)
                    {
                        isReading = true;
                        if (LinksBegin != null)
                            LinksBegin(this, new LinksBeginEventArgs(e.Line));
                    }
                    break;
                    
                case 365:
                    linksLines.Add(e.Line);
                    if (LinksEnd != null)
                        LinksEnd(this, new LinksEndEventArgs(e.Line, LinksLines));
                    isReading = false;
                    break;
            }
        }
    }
}
