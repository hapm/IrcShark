// <copyright file="NoticeReceivedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the NoticeReceivedEventArgs class.</summary>

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
namespace IrcSharp
{
    using System;

    /// <summary>
    /// The NoticeReceivedEventArgs belongs to the <see cref="NoticeReceivedEventHandler" /> and the <see cref="IrcClient.NoticeReceived" /> event.
    /// </summary>
    public class NoticeReceivedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the NoticeReceivedEventArgs class.
        /// </summary>
        /// <param name="line">The line creating thenotice event.</param>
        public NoticeReceivedEventArgs(IrcLine line) : base(line)
        {
        }

        /// <summary>
        /// Gets the sender of the notice.
        /// </summary>
        /// <value>The name of the sender as a string.</value>
        public string Sender
        {
            get { return Line.Prefix; }
        }

        /// <summary>
        /// Gets the receiver of the notice message.
        /// </summary>
        /// <value>The name of the destination.</value>
        public string Destination
        {
            get { return Line.Parameters[0]; }
        }

        /// <summary>
        /// Gets the message sent as a notice.
        /// </summary>
        /// <value>The message as a string.</value>
        public string Message
        {
            get { return Line.Parameters[1]; }
        }
    }
}
