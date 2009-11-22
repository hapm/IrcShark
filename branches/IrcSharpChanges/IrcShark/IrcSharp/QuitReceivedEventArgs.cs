// <copyright file="QuitReceivedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the QuitReceivedEventArgs class.</summary>

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
    /// The QuitReceivedEventArgs belongs to the <see cref="QuitReceivedEventHandler" /> and the <see cref="IrcClient.QuitReceived" /> event.
    /// </summary>
    public class QuitReceivedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the instance of the user, who quited.
        /// </summary>
        private UserInfo user;
        
        /// <summary>
        /// Saves the quit message.
        /// </summary>
        private string quitMessage;

        /// <summary>
        /// Initializes a new instance of the QuitReceivedEventArgs class.
        /// </summary>
        /// <param name="line">The line containing the quit command.</param>
        public QuitReceivedEventArgs(IrcLine line) : base(line)
        {
            user = new UserInfo(line);
            
            if (line.Parameters.Length > 0)
                quitMessage = line.Parameters[0];
            else
                quitMessage = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the QuitReceivedEventArgs class.
        /// </summary>
        /// <param name="user">The user, that quited.</param>
        /// <param name="message">The message the user sent when quitting.</param>
        public QuitReceivedEventArgs(UserInfo user, string message) : base(user.Client)
        {
            this.user = user;
            this.quitMessage = message;
        }

        /// <summary>
        /// Gets the UserInfo of the user who quitted.
        /// </summary>
        /// <value>The UserInfo instance.</value>
        public UserInfo User
        {
            get { return user; }
        }

        /// <summary>
        /// Gets the emssage the user added to the quit.
        /// </summary>
        /// <value>The message as a string.</value>
        public string QuitMessage
        {
            get { return quitMessage; }
        }
    }
}
