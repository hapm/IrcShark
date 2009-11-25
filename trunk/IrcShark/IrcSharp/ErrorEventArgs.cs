// <copyright file="ErrorEventArgs.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ErrorEventArgs class.</summary>

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
    /// The ErrorEventArgs belongs to the <see cref="ErrorEventHandler" /> and the <see cref="IrcClient.Error" /> event.
    /// </summary>
    public class ErrorEventArgs : IrcEventArgs
    {
        /// <summary>
        /// Saves the inner exception that caused the error, if any.
        /// </summary>
        private Exception innerException;
        
        /// <summary>
        /// Saves the error message.
        /// </summary>
        private string message;

        /// <summary>
        /// Initializes a new instance of the ErrorEventArgs class.
        /// </summary>
        /// <param name="client">The client, the error happens on.</param>
        /// <param name="msg">The related error message.</param>
        public ErrorEventArgs(IrcClient client, string msg) : base(client)
        {
            message = msg;
        }

        /// <summary>
        /// Initializes a new instance of the ErrorEventArgs class.
        /// </summary>
        /// <param name="client">The client, the error happens on.</param>
        /// <param name="msg">The related error message.</param>
        /// <param name="exception">The inner exception, that caused the error.</param>
        public ErrorEventArgs(IrcClient client, string msg, Exception exception) : base(client)
        {
            innerException = exception;
            message = msg;
        }

        /// <summary>
        /// Gets the message related to the error.
        /// </summary>
        /// <value>The error message.</value>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// Gets the exception causing the error.
        /// </summary>
        /// <value>The exception instance or null if there was no exception.</value>
        public Exception InnerException
        {
            get { return innerException; }
        }
    }
}
