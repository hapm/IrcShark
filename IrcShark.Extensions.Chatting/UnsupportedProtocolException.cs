// <copyright file="UnsupportedProtocolException.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcShark.Extensions.Chatting
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The UnsupportedProtocolException is thrown if a ProtocolExtension gets objects not supported by its protocol.
    /// </summary>
    public class UnsupportedProtocolException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the UnsupportedProtocolException class.
        /// </summary>
        public UnsupportedProtocolException() : base("The protocol of the given object is not supported.")
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the UnsupportedProtocolException class.
        /// </summary>
        /// <param name="message">The message to use with the exception.</param>
        public UnsupportedProtocolException(string message) : base(message)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the UnsupportedProtocolException class.
        /// </summary>
        /// <param name="message">The message to use with the exception.</param>
        /// <param name="innerException">The inner exception causing this exception.</param>
        public UnsupportedProtocolException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the UnsupportedProtocolException class.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        public UnsupportedProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
