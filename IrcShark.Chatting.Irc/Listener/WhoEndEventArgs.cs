// <copyright file="WhoEndEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the WhoEndEventArgs class.</summary>

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
namespace IrcShark.Chatting.Irc.Listener
{
    using System;

    /// <summary>
    /// The WhoEndEventArgs class is the EventArgs class for the WhoListener.WhoEnd event.
    /// </summary>
    public class WhoEndEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Holds all lines that belongs to the received who reply.
        /// </summary>
        private WhoLine[] whoLines;

        /// <summary>
        /// Initializes a new instance of the WhoEndEventArgs class.
        /// </summary>
        /// <param name="line">The line, that marks the who reply end.</param>
        /// <param name="whoLines">All lines of the who reply.</param>
        public WhoEndEventArgs(IrcLine line, WhoLine[] whoLines) : base(line)
        {
            this.whoLines = whoLines;
        }

        /// <summary>
        /// Gets all lines that belong to the who reply.
        /// </summary>
        /// <value>An array of IrcLines.</value>
        public WhoLine[] WhoLines
        {
            get { return whoLines; }
        }
    }
}
