// <copyright file="StatusChangedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChatManagerExtension class.</summary>

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
namespace IrcShark
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using IrcShark.Extensions;

    /// <summary>
    /// The <see cref="EventArgs" /> for the StatusChanged event.
    /// </summary>
    public class StatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Holds the <see cref="ExtensionInfo" /> of this StatusChangeEventArgs.
        /// </summary>
        private ExtensionInfo extension;
        
        /// <summary>
        /// Holds the new <see cref="ExtensionStates" /> of this StatusChangeEventArgs.
        /// </summary>
        private ExtensionStates status;

        /// <summary>
        /// Initializes a new instance of the StatusChangedEventArgs class.
        /// </summary>
        /// <param name="ext">Information about the extension, what changed its status.</param>
        /// <param name="status">The new status of the extension.</param>
        public StatusChangedEventArgs(ExtensionInfo ext, ExtensionStates status)
        {
            this.extension = ext;
            this.status = status;
        }

        /// <summary>
        /// Gets the ExtensionInfo for the extension, what changed its status.
        /// </summary>
        /// <value>The ExtensionInfo for the extension, what changed its status.</value>
        public ExtensionInfo Extension
        {
            get { return extension; }
        }

        /// <summary>
        /// Gets the new status of the Extension.
        /// </summary>
        /// <value>The new statu of the Extension.</value>
        public ExtensionStates Status
        {
            get { return status; }
        }
    }
}