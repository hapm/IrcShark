// <copyright file="ConsoleTerminal.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ConsoleTerminal class.</summary>

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
namespace IrcShark.Extensions.Terminal
{
	using System;

	/// <summary>
	/// This class allows the TerminalExtension to use the local Console as
	/// as a terminal for a user.
	/// </summary>
	public class ConsoleTerminal
	{
        
        /// <summary>
        /// Saves a value indicating whether the last written console line has 
        /// a linebreak at the end or not.
        /// </summary>
        /// <remarks>
        /// This value is needed in <see cref="CleanInputLine" /> to know if
        /// the linebreak should be cleared or not.
        /// </remarks>
        private bool newLine;
        
        /// <summary>
        /// Saves the length of the lastly written line.
        /// </summary>
        /// <remarks>
        /// This value is needed for the Write method to be able to determine where
        /// new written text should start.
        /// </remarks>
        private int lastLineLength;
        
        /// <summary>
        /// Saves the currently selected foreground color.
        /// </summary>
        private ConsoleColor fgColor;
        
        /// <summary>
        /// Initializes a new instances of the ConsoleTerminal class.
        /// </summary>
		public ConsoleTerminal()
		{
		}
	}
}
