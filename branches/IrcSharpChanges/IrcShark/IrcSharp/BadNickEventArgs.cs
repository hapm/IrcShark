// <copyright file="BadNickEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the BadNickEventArgs class.</summary>

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
    /// The EventArgs for the BadNick event.
    /// </summary>
    public class BadNickEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves if the event was fired when logging in.
        /// </summary>
        private bool isLogin;

        /// <summary>
        /// Initializes a new instance of the BadNickEventArgs class.
        /// </summary>
        /// <param name="baseLine">The line, what created the bad nick event.</param>
        /// <param name="inLogin">Determines if the event was fired in the login state.</param>
        public BadNickEventArgs(IrcLine baseLine, bool inLogin) : base(baseLine)
        {
            isLogin = inLogin;
        }

        /// <summary>
        /// Gets a value indicating whether the connection is logging in at the moment.
        /// </summary>
        /// <remarks>The value of IsLogin is true if the nickname, you wanted to connect with, wasn't excepted by the server and therefor, the login isn't complete.</remarks>
        /// <value>True, if the bad nick was send when the connection is about to log in, else false.</value>
        public bool IsLogin
        {
            get { return isLogin; }
        }
    }
}
