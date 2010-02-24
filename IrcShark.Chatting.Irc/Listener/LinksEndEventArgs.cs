// <copyright file="LinksEndEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the LinksEndEventArgs class.</summary>

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
namespace IrcShark.Chatting.Irc.Listener
{
    using System;

    /// <summary>
    /// The LinksEndEventArgs class is the EventArgs class for the LinksListener.LinksEnd event.
    /// </summary>
    public class LinksEndEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Holds all lines that belongs to the received links block.
        /// </summary>
        private IrcLine[] linksLines;

        /// <summary>
        /// Initializes a new instance of the LinksEndEventArgs class.
        /// </summary>
        /// <param name="line">The line, that marks the links block end.</param>
        /// <param name="linksLines">All lines of the links block.</param>
        public LinksEndEventArgs(IrcLine line, IrcLine[] linksLines) : base(line)
        {
            this.linksLines = linksLines;
        }

        /// <summary>
        /// Gets all lines that belong to the links block.
        /// </summary>
        /// <value>An array of IrcLines.</value>
        public IrcLine[] LinksLines
        {
            get { return linksLines; }
        }
    }
}
