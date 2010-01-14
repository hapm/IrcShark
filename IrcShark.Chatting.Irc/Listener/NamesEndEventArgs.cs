// <copyright file="NamesEndEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the NamesEndEventArgs class.</summary>

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

    /// <summary>
    /// The NamesEndEventArgs class is the EventArgs class for the NamesListener.NamesEnd event.
    /// </summary>
    public class NamesEndEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Holds all names that belongs to the received names list.
        /// </summary>
        private string[] names;
        
        /// <summary>
        /// Saves the channel name.
        /// </summary>
        private string channelName;

        /// <summary>
        /// Initializes a new instance of the NamesEndEventArgs class.
        /// </summary>
        /// <param name="line">The line, that marks the links block end.</param>
        /// <param name="names">All names of the names list.</param>
        /// <param name="channelName">The name of the channel, the names list was received for.</param>
        public NamesEndEventArgs(IrcLine line, string[] names, string channelName) : base(line)
        {
            this.names = names;
            this.channelName = channelName;
        }

        /// <summary>
        /// Gets all names in the names list.
        /// </summary>
        /// <value>A string array of the names.</value>
        public string[] Names
        {
            get { return names; }
        }

        /// <summary>
        /// Gets the name of the channel, the request was send for.
        /// </summary>
        /// <value>The channel name as a string.</value>
        public string ChannelName
        {
            get { return channelName; }
        }
    }
}
