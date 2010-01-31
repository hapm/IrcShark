// <copyright file="ITerminal.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ITerminal interface.</summary>

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
namespace IrcSharkTerminal
{
	using System;

	/// <summary>
	/// Represents a terminal where information can be presented to and 
	/// received from a user.
	/// </summary>
	/// <remarks>
	/// This for the interface to the different terminal types like the
	/// local console or a ssl network terminal.
	/// </remarks>
	public interface ITerminal
	{
		/// <summary>
        /// Writes a complete line and appends a linebreak at the end.
        /// </summary>
        /// <param name="line">The line to write.</param>
        public void WriteLine(string line);
        
        /// <summary>
        /// Resets the foreground and background color of the terminal.
        /// </summary>
        public void ResetColor();
        
        /// <summary>
        /// Writes a complete formated line and appends a linebreak at the end.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="arg">The objects to use when formating the line.</param>
        public void WriteLine(string format, params object[] arg);
        
        /// <summary>
        /// Writes a linebreak to the terminal.
        /// </summary>
        public void WriteLine();
        
        /// <summary>
        /// Gets or sets the foregroundcolor of the drawn text.
        /// </summary>
        /// <value>A ConsoleColor value indicating the current foreground color.</value>
        public ConsoleColor ForegroundColor { get; set; }
	}
}
