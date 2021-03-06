// <copyright file="ExtensionException.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExtensionException class.</summary>

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
namespace IrcShark.Extensions
{
    using System;

    /// <summary>
    /// This exception is thrown when ever an extension causes an exception.
    /// </summary>
    [Serializable]
    public class ExtensionException : Exception
    {
        /// <summary>
        /// The identifier for the extension, what caused the exception.
        /// </summary>
        private ExtensionInfo info;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionException class for the given extension.
        /// </summary>
        /// <param name="info">
        /// The <see cref="ExtensionInfo"/> for the extension causing the exception.
        /// </param>
        public ExtensionException(ExtensionInfo info) : base(String.Format("Exception caused by extension {0} ", info.Name))
        {
            this.info = info;
        }
        
        /// <summary>
        /// Initializes a new instance of the ExtensionException class for the given extension with the given message.
        /// </summary>
        /// <param name="info">
        /// The <see cref="ExtensionInfo"/> for the extension causing the exception.
        /// </param>
        /// <param name="msg">
        /// The message of the exception.
        /// </param>
        public ExtensionException(ExtensionInfo info, string msg) : base(msg)
        {
            this.info = info;
        }
        
        /// <summary>
        /// Gets the identifier for the extension causing this exception.
        /// </summary>
        /// <value>The ExtensionInfo of the extension.</value>
        public ExtensionInfo Info
        {
            get { return info; }
        }
    }
}
