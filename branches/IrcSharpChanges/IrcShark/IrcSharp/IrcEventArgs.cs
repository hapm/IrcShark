// <copyright file="IrcEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the IrcEventArgs class.</summary>

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
namespace IrcSharp 
{
    using System;

    /// <summary>
    /// This cöass represents the EventArgs for irc protocol event.
    /// </summary>
    public class IrcEventArgs : EventArgs, IIrcObject
    {
        /// <summary>
        /// Saves, if the event was handled or not.
        /// </summary>
        private bool handled;
        
        /// <summary>
        /// Saves the instance of the IrcClient, this IrcEventArgs belong to.
        /// </summary>
        private IrcClient client;
        
        /// <summary>
        /// Saves the IrcLine the EventArgs instance was created from.
        /// </summary>
        private IrcLine line;

        /// <summary>
        /// Initializes a new instance of the IrcEventArgs class.
        /// </summary>
        /// <param name="client">The client instance, the EventArgs belongs to.</param>
        public IrcEventArgs(IrcClient client)
        {
            handled = false;
            this.client = client;
        }

        /// <summary>
        /// Initializes a new instance of the IrcEventArgs class.
        /// </summary>
        /// <param name="line">The line, the EventArgs where created from.</param>
        public IrcEventArgs(IrcLine line)
        {
            handled = false;
            client = line.Client;
            this.line = line;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the event of the EventArgs is handled.
        /// </summary>
        /// <value>Its true when the event was handled, false otherwise.</value>
        public bool Handled
        {
            get { return handled; }
            set { handled = value; }
        }

        /// <summary>
        /// Gets the line, this EventArgs belong to.
        /// </summary>
        /// <value>The line that caused the event.</value>
        public IrcLine Line
        {
            get { return line; }
        }

        #region IIrcObject Member
        /// <summary>
        /// Gets the IrcClient the EventArgs where created for.
        /// </summary>
        /// <value>The client the event was raised from.</value>
        public IrcClient Client
        {
            get { return client; }
        }

        #endregion
    }
}
