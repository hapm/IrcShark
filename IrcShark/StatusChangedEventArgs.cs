// <copyright file="StatusChangedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the StatusChangedEventArgs class.</summary>

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
        private string addinId;
        
        /// <summary>
        /// Holds the new <see cref="ExtensionStates" /> of this StatusChangeEventArgs.
        /// </summary>
        private ExtensionStates status;

        /// <summary>
        /// Initializes a new instance of the StatusChangedEventArgs class.
        /// </summary>
        /// <param name="addinId">The id of the addin, what changed its status.</param>
        /// <param name="status">The new status of the extension.</param>
        public StatusChangedEventArgs(string addinId, ExtensionStates status)
        {
            this.addinId = addinId;
            this.status = status;
        }

        /// <summary>
        /// Gets the id for the extension, what changed its status.
        /// </summary>
        /// <value>The addinid as a string for the extension, what changed its status.</value>
        public string AddIn
        {
            get { return addinId; }
        }

        /// <summary>
        /// Gets the new status of the Extension.
        /// </summary>
        /// <value>The new status of the Extension.</value>
        public ExtensionStates Status
        {
            get { return status; }
        }
    }
}