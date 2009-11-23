// <copyright file="InvalidLineFormatException.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the InvalidLineFormatException class.</summary>

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
    /// This exception is thrown by an <see cref="IrcLine"/> if the raw format was not correct.
    /// </summary>
    [Serializable]
    public class InvalidLineFormatException : Exception
    {
        /// <summary>
        /// Saves the text line, what was tried to parse.
        /// </summary>
        private string line;

        /// <summary>
        /// Initializes a new instance of the InvalidLineFormatException class.
        /// </summary>
        /// <param name="line">
        /// The raw string, what couldn't be parsed as a raw irc line.
        /// </param>
        public InvalidLineFormatException(string line) : base(string.Format("Couldn't parse the raw line \"{0}\"", line))
        {
            this.line = line;
        }
        
        /// <summary>
        /// Initializes a new instance of the InvalidLineFormatException class with the given message.
        /// </summary>
        /// <param name="msg">
        /// The message of this exception.
        /// </param>
        /// <param name="line">
        /// The part of or a complete raw string, what couldn't be parsed as a raw irc line.
        /// </param>
        public InvalidLineFormatException(string msg, string line) : base(msg)
        {
            this.line = line;
        }

        /// <summary>
        /// Gets the raw text line.
        /// </summary>
        /// <value>
        /// The raw incorrect <see cref="System.String"/>.
        /// </value>
        public string Line
        {
            get { return line; }
        }
    }
}
