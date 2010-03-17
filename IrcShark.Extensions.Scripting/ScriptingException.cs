// <copyright file="ScriptingException.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ScriptingException class.</summary>

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
namespace IrcShark.Extensions.Scripting
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A base class ScriptingException for all exceptions caused when executing a script.
    /// </summary>
    [Serializable]
    public class ScriptingException : Exception
    {
        public ScriptingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ScriptingException class.
        /// </summary>
        public ScriptingException() : base()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ScriptingException class.
        /// </summary>
        /// <param name="message">The message for this exception.</param>
        public ScriptingException(string message) : base(message)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ScriptingException class.
        /// </summary>
        /// <param name="message">The message for this exception.</param>
        /// <param name="innerException">The inner exception causing this exception.</param>
        public ScriptingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
