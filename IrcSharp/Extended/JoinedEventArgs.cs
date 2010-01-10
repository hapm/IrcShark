// <copyright file="JoinedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the JoinedEventArgs class.</summary>

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
namespace IrcSharp.Extended
{
    using System;

    /// <summary>
    /// The JoinedEventArgs belongs to the <see cref="ChannelManager.JoinedEventHandler" /> 
    /// and the <see cref="ChannelManager.Joined" /> event.
    /// </summary>
    public class JoinedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the channel, that was joined.
        /// </summary>
        private Channel channel;

        /// <summary>
        /// Initializes a new instance of the JoinedEventArgs class.
        /// </summary>
        /// <param name="chan">The instance of the Channel, that was joined.</param>
        public JoinedEventArgs(Channel chan) : base(chan.Client)
        {
            channel = chan;
        }

        /// <summary>
        /// Gets the instance of the joined channel.
        /// </summary>
        /// <value>The Channel that was joined.</value>
        public Channel Channel
        {
            get { return channel; }
        }
    }
}
