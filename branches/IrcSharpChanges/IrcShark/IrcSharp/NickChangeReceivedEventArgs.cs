// <copyright file="NickChangeReceivedEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the NickChangeReceivedEventArgs class.</summary>

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
    /// The NickChangeReceivedEventArgs belongs to the <see cref="NickchangeReceivedEventHandler" /> and the <see cref="IrcClient.NickChangeReceived" /> event.
    /// </summary>
    public class NickChangeReceivedEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the new nick of the user.
        /// </summary>
        private string newNickName;
        
        /// <summary>
        /// Saves the user information.
        /// </summary>
        private UserInfo user;

        /// <summary>
        /// Initializes a new instance of the NickChangeReceivedEventArgs class.
        /// </summary>
        /// <param name="line">The line, that holds the renaming command.</param>
        public NickChangeReceivedEventArgs(IrcLine line) : base(line)
        {
            user = new UserInfo(line);
            newNickName = line.Parameters[0];
        }

        /// <summary>
        /// Gets the UserInfo for the user who changed its nick.
        /// </summary>
        /// <value>The UserInfo instance.</value>
        public UserInfo User 
        {
            get { return user; }
        }

        /// <summary>
        /// Gets the new name of the user.
        /// </summary>
        /// <value>The new name as a string.</value>
        public string NewNickName
        {
            get { return newNickName; }
        }
    }
}
