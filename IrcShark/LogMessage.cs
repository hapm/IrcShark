// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
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

namespace IrcShark
{
    using System;

    /// <summary>
    /// Represents a message to log.
    /// </summary>
    public class LogMessage
    {
        private int identifier;
        private string channel;
        private string message;
        private LogLevel level;
        private DateTime time;

        /// <summary>
        /// Initializes a new instance of the LogMessage class with the given text on the given channel.
        /// </summary>
        /// <param name="channel">
        /// The name of the channel the message was logged to.
        /// </param>
        /// <param name="id">
        /// An identfication number for this message unique for the log channel.
        /// </param>
        /// <param name="msg">
        /// The message belonging to this log entry.
        /// </param>
        public LogMessage(string channel, int id, string msg)
        {
            this.channel = channel;
            this.level = LogLevel.Information;
            this.message = msg;
            this.time = DateTime.Now;
            this.identifier = id;
        }
        
        /// <summary>
        /// Initializes a new instance of the LogMessage class with the given text and level on the given channel.
        /// </summary>
        /// <param name="channel">
        /// The name of the channel the message was logged to.
        /// </param>
        /// <param name="id">
        /// An identfication number for this message unique for the log channel.
        /// </param>
        /// <param name="level">
        /// The <see cref="LogLevel"/> of this LogMessage.
        /// </param>
        /// <param name="msg">
        /// The message belonging to this log entry.
        /// </param>
        public LogMessage(string channel, int id, LogLevel level, string msg)
        {
            this.channel = channel;
            this.level = level;
            this.message = msg;
            this.time = DateTime.Now;
            this.identifier = id;
        }
        
        /// <summary>
        /// Initializes a new instance of the LogMessage class with the given text and level on the given channel.
        /// </summary>
        /// <param name="channel">
        /// The name of the channel the message was logged to.
        /// </param>
        /// <param name="id">
        /// An identfication number for this message unique for the log channel.
        /// </param>
        /// <param name="level">
        /// The <see cref="LogLevel"/> of this LogMessage.
        /// </param>
        /// <param name="msg">
        /// The message format string belonging to this log entry.
        /// </param>
        /// <param name="args">
        /// The arguments to insert into the format string.
        /// </param>
        public LogMessage(string channel, int id, LogLevel level, string msg, params object[] args)
        {
            this.channel = channel;
            this.level = level;
            this.message = String.Format(msg, args);
            this.time = DateTime.Now;
            this.identifier = id;
        }
                
        /// <summary>
        /// Gets the channels this message was logged on.
        /// </summary>
        public string Channel
        {
            get { return channel; }
        }
        
        /// <summary>
        /// Gets the LogLevel of this message.
        /// </summary>
        public LogLevel Level
        {
            get { return level; }
        }
        
        /// <summary>
        /// Gets the message text.
        /// </summary>
        public string Message 
        {
            get { return message; }
        }
        
        /// <summary>
        /// Gets the time when this message was created.
        /// </summary>
        public DateTime Time
        {
            get { return time; }
        }
        
        /// <summary>
        /// Gets the identifieng number of this messages.
        /// </summary>
        public int Id 
        {
            get { return identifier; }
        }
    }
}
