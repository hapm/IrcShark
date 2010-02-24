// <copyright file="ChannelListLine.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChannelListLine class.</summary>

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
namespace IrcShark.Chatting.Irc.Listener
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The ChannelListLine represents a line in a channel list as sent from an irc server.
    /// </summary>
    public class ChannelListLine : IrcLine
    {
        /// <summary>
        /// Saves the number of users inf the channel.
        /// </summary>
        private int userCount;
        
        /// <summary>
        /// Saves the set channel modes.
        /// </summary>
        private string modes;
        
        /// <summary>
        /// Saves the channel topic.
        /// </summary>
        private string topic;

        /// <summary>
        /// Initializes a new instance of the ChannelListLine class.
        /// </summary>
        /// <param name="line">The line, the ChannelListLine bases on.</param>
        public ChannelListLine(IrcLine line) : base(line)
        {
            if (line.Numeric != 332)
            {
                throw new ArgumentOutOfRangeException("line", "CHANNELLIST_RPL 322 expected");
            }
            
            if (Parameters.Length < 3)
            {
                throw new ArgumentOutOfRangeException("line", "Need a minimum of 3 parameters");
            }
            
            if (!int.TryParse(Parameters[2], out userCount))
            {
                throw new ArgumentOutOfRangeException("line", "Invalid user count, integer expected");
            }
            
            if (Parameters.Length > 3)
            {
                Regex modeTopicRegex = new Regex(@"(?:\[\+([^ \]]*)] )?(.*)");
                Match m = modeTopicRegex.Match(Parameters[3]);
                if (m.Success)
                {
                    modes = m.Groups[1].Value;
                    topic = m.Groups[2].Value;
                }
                else
                {
                    modes = string.Empty;
                    topic = string.Empty;
                }
            }
            else
            {
                modes = string.Empty;
                topic = string.Empty;
            }
        }

        /// <summary>
        /// Gets the name of the channel.
        /// </summary>
        /// <value>The name as a string.</value>
        public string ChannelName
        {
            get { return Parameters[1]; }
        }
        
        /// <summary>
        /// Gets the topic of the channel.
        /// </summary>
        /// <value>The topic as a string.</value>
        public string Topic 
        {
            get { return topic; }
        }
        
        /// <summary>
        /// Gets the modes, set for the channel.
        /// </summary>
        /// <value>The modes as a string.</value>
        public string Modes
        {
            get { return modes; }
        }

        /// <summary>
        /// Gets the number of users in the channel.
        /// </summary>
        /// <value>The number of users.</value>
        public int UserCount
        {
            get { return userCount; }
        }
    }
}
