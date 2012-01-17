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
namespace IrcShark.Extensions.Terminal
{
    using System;
    
    /// <summary>
    /// The AutoCompleteHandler delegate is used by the AutoComplete property.
    /// </summary>
	public delegate Completion AutoCompleteHandler(string text, int pos);

    /// <summary>
    /// Represents a terminal where information can be presented to and 
    /// received from a user.
    /// </summary>
    /// <remarks>
    /// This for the interface to the different terminal types like the
    /// local console or a ssl network terminal.
    /// </remarks>
	[TypeExtensionPoint(ExtensionAttributeType=typeof(IrcShark.Extensions.Terminal.TerminalAttribute))]
    public interface ITerminal
    {
        /// <summary>
        /// Gets or sets the foregroundcolor of the drawn text.
        /// </summary>
        /// <value>A ConsoleColor value indicating the current foreground color.</value>
        ConsoleColor ForegroundColor { get; set; }
        
        /// <summary>
        /// This handler should be called, when autocompletition is wanted.
        /// </summary>
        AutoCompleteHandler AutoCompleteEvent { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether the terminal is currently reading
        /// a command.
        /// </summary>
        bool IsReading { get; }
     	
        /// <summary>
        /// Opens the terminal for reading and writing.
        /// </summary>
    	void Open(ExtensionContext context);   
    	
    	/// <summary>
    	/// Closes the terminal.
    	/// </summary>
    	void Close();
    	
        /// <summary>
        /// Writes the given text to the terminal.
        /// </summary>
        /// <param name="text">The text to write.</param>
        void Write(string text);
        
        /// <summary>
        /// Writes the given text to the terminal.
        /// </summary>
        /// <param name="format">The format of the text to write.</param>
        /// <param name="arg">The args to place in the format.</param>
        void Write(string format, params object[] arg);
        
        /// <summary>
        /// Writes a complete line and appends a linebreak at the end.
        /// </summary>
        /// <param name="line">The line to write.</param>
        void WriteLine(string line);
        
        /// <summary>
        /// Writes a complete formated line and appends a linebreak at the end.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="arg">The objects to use when formating the line.</param>
        void WriteLine(string format, params object[] arg);
        
        /// <summary>
        /// Writes a linebreak to the terminal.
        /// </summary>
        void WriteLine();
        
        /// <summary>
        /// Resets the foreground and background color of the terminal.
        /// </summary>
        void ResetColor();
        
        /// <summary>
        /// Reads a command from the terminal.
        /// </summary>
        /// <returns>
        /// The CommandCall instance for the command or null, if the user din't type a command.
        /// </returns>
        CommandCall ReadCommand();
        
        /// <summary>
        /// Stops to read a command from the terminal if it is reading at the moment.
        /// </summary>
        void StopReading();
    }
}
