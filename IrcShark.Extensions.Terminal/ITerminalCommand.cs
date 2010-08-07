// <copyright file="TerminalCommand.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the TerminalCommand class.</summary>

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
	using System.Collections.Generic;
	using System.Text;
	
	using Mono.Addins;
	
	/// <summary>
	/// Used to extend the commands displayed on the terminal.
	/// </summary>
	[TypeExtensionPoint(ExtensionAttributeType=typeof(TerminalCommandAttribute))]
	public interface ITerminalCommand
	{
		/// <summary>
		/// Gets the command name to use to execute thi command.
		/// </summary>
		/// <value>The name of the command.</value>
		string CommandName { get; }
		
		/// <summary>
		/// The TerminalExtension instance of the terminal, this command is shown on.
		/// </summary>
		/// <value>The TerminalExtension instance.</value>
		TerminalExtension Terminal { get; }
		
		/// <summary>
		/// Gets a value indicating whether the command is currently useable or not.
		/// </summary>
		/// <value>Its true if the command can be executed at the moment, false otherwise.</value>
		bool Active { get; }
		
	    /// <summary>
	    /// Autocompletes the parameters of this command.
	    /// </summary>
	    /// <param name="call">The call to complete.</param>
	    /// <param name="paramIndex">The index of the parameter to complete.</param>
	    /// <returns>The possible completitions.</returns>
		string[] AutoComplete(CommandCall call, int paramIndex);
		
		/// <summary>
		/// Executes the command with the given parameters.
		/// </summary>
		/// <param name="paramList">A list of specified parameters.</param>
		void Execute(params string[] paramList);
		
		/// <summary>
		/// Initializes the command by providing the TerminalExtension instance.
		/// </summary>
		/// <param name="terminal">The TerminalExtension instance.</param>
		void Init(TerminalExtension terminal);
	}
}
