// <copyright file="ChannelListEndEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChannelListEndEventArgs class.</summary>

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
    /// The ChannelListEndEventArgs class is the EventArgs class for the ChannelListListener.ChannelListEnd event.
    /// </summary>
    public class ChannelListEndEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves all received channellist lines.
        /// </summary>
        private IrcLine[] channelListLines;

        /// <summary>
        /// Initializes a new instance of the ChannelListEndEventArgs class.
        /// </summary>
        /// <param name="line">The line, that marks the channel list block end.</param>
        /// <param name="channellistLines">All lines of the channel list block.</param>
        public ChannelListEndEventArgs(IrcLine line, IrcLine[] channellistLines) : base(line)
        {
            channelListLines = channellistLines;
        }

        /// <summary>
        /// Gets all lines that belong to the channel list block.
        /// </summary>
        /// <value>An array of ChannelListLines.</value>
        public IrcLine[] ChannelListLines
        {
            get { return channelListLines; }
        }
    }
}
