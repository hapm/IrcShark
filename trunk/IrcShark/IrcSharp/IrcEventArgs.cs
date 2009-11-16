// $Id$
//
// Add description here
//
// Benutzer: markus
// Datum: 12.11.2009
// Zeit: 22:00 
//
// Note:
// 
// Copyright (C) 2009 IrcShark Team
//  
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
//
// Erstellt mit SharpDevelop.
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
        private IrcLine baseLine;

        /// <summary>
        /// Initializes a new instance of the IrcEventArgs class.
        /// </summary>
        /// <param name="Client">The client instance, the EventArgs belongs to.</param>
        public IrcEventArgs(IrcClient Client)
        {
            handled = false;
            client = Client;
        }

        /// <summary>
        /// Initializes a new isntance of the IrcEventArgs class.
        /// </summary>
        /// <param name="BaseLine">The line, the EventArgs where created from.</param>
        public IrcEventArgs(IrcLine BaseLine)
        {
            handled = false;
            client = BaseLine.Client;
            baseLine = BaseLine;
        }

        /// <summary>
        /// Gets or sets wether the event of the EventArgs is handled.
        /// </summary>
        public bool Handled
        {
            get { return handled; }
            set { handled = value; }
        }

        /// <summary>
        /// Gets the line, this EventArgs belong to.
        /// </summary>
        public IrcLine BaseLine
        {
            get { return baseLine; }
        }

        #region IIrcObject Member
        /// <summary>
        /// Gets the IrcClient the EventArgs where created for.
        /// </summary>
        public IrcClient Client
        {
            get { return client; }
        }

        #endregion
    }
}
