// $Id$
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

using System;

namespace IrcSharp
{	
	/// <summary>
	/// This exception is thrown by an <see cref="IrcLine"/> if the raw format was not correct
	/// </summary>
	public class InvalidLineFormatException : Exception
	{
        string line;

		/// <summary>
		/// creates a new instance of this exception
		/// </summary>
		/// <param name="line">
		/// the raw string, what couldn't be parsed as a raw irc line
		/// </param>
        public InvalidLineFormatException(string line) : base(String.Format("Couldn't parse the raw line \"{0}\"", line))
        {
            this.line = line;
        }
		
		/// <summary>
		/// creates a new instance of this exception with the given message
		/// </summary>
		/// <param name="msg">
		/// the message of this exception
		/// </param>
		/// <param name="line">
		/// the part of or a complete raw string, what couldn't be parsed as a raw irc line
		/// </param>
        public InvalidLineFormatException(string msg, string line) : base(msg)
        {
            this.line = line;
        }

		/// <value>
		/// the raw incorrect <see cref="System.String"/>
		/// </value>
        public string Line
        {
            get { return line; }
        }
	}
}
