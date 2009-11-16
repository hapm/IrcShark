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
    using System.Runtime.Serialization;
    
    /// <summary>
    /// A ConfigurationException is thrown when there was an error in the configuration of IrcShark.
    /// </summary>
    [Serializable]
    public class ConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ConfigurationException class.
        /// </summary>
        public ConfigurationException() : base()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ConfigurationException class.
        /// </summary>
        /// <param name="message">The message of this exception.</param>
        public ConfigurationException(string message) : base(message) 
        { 
        }
        
        /// <summary>
        /// Initializes a new instance of the ConfigurationException class.
        /// </summary>
        /// <param name="message">The message of this exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public ConfigurationException(string message, Exception innerException) : base(message, innerException) 
        { 
        }
        
        /// <summary>
        /// Initializes a new instance of the ConfigurationException class.
        /// </summary>
        /// <param name="info">The SerializationInfo.</param>
        /// <param name="context">The StreamingContext.</param>
        protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) 
        { 
        }
    }
}
