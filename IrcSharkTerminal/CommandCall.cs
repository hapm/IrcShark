// <copyright file="CammandCall.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the CommandCall class.</summary>

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
	using System.Collections;
	using System.Text.RegularExpressions;

	/// <summary>
	/// The CommandCall class is used to parse a command from a text line to give easy access to its parameters.
	/// </summary>
	public class CommandCall
	{
		/// <summary>
		/// Saves the command name.
		/// </summary>
		private string commandName;
		
		/// <summary>
		/// Saves the list of parameters.
		/// </summary>
		private string[] parameters;
		
		private static Regex cmdCallRegex = new Regex("([^ ]+)(?: +(\"(?:[^\\\\\"]|\\\\.)*\"|[^ ]*))*");
		
		private static Regex escapeReplace = new Regex(@"\\(.)");
		
		/// <summary>
		/// Initializes a new instance of the CommandCall class.
		/// </summary>
		/// <param name="line">The line to parse.</param>
		public CommandCall(string line)
		{
			Match result = cmdCallRegex.Match(line);
			if (!result.Success)
				throw new ArgumentException("The line couldn't be parsed to a command call", "line");
			commandName = result.Groups[1].Value;
			parameters = new string[result.Groups[2].Captures.Count];
			for (int i = 0; i < parameters.Length; i++)
			{
				Capture c = result.Groups[2].Captures[i];
				if (c.Value[0] == '"')
					parameters[i] = escapeReplace.Replace(c.Value.Substring(1, c.Value.Length - 2), "$1");
				else
					parameters[i] = c.Value;
			}
		}
		
		/// <summary>
		/// Gets the name of the command.
		/// </summary>
		public string CommandName
		{
			get { return commandName; }
		}
		
		/// <summary>
		/// Gets the list of parameters.
		/// </summary>
		/// <returns>
		/// The array of parameters.
		/// </returns>
		public string[] Parameters
		{
			get { return parameters; }
		}
	}
}
