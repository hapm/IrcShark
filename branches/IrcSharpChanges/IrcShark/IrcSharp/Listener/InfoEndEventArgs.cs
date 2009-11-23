// <copyright file="InfoEndEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the InfoEndEventArgs class.</summary>

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
namespace IrcSharp.Listener
{
    using System;

    /// <summary>
    /// The InfoEndEventArgs class is the EventArgs class for the InfoListener.InfoEnd event.
    /// </summary>
    public class InfoEndEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Holds all lines that belongs to the received info block.
        /// </summary>
        private IrcLine[] infoLines;

        /// <summary>
        /// Initializes a new instance of the InfoEndEventArgs class.
        /// </summary>
        /// <param name="line">The line, that marks the info block end.</param>
        /// <param name="infoLines">All lines of the info block.</param>
        public InfoEndEventArgs(IrcLine line, IrcLine[] infoLines) : base(line)
        {
            this.infoLines = infoLines;
        }

        /// <summary>
        /// Gets all lines that belong to the info block.
        /// </summary>
        /// <value>An array of IrcLines.</value>
        public IrcLine[] InfoLines
        {
            get { return infoLines; }
        }
    }
}
