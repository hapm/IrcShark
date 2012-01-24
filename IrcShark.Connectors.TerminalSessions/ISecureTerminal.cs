// <copyright file="TerminalExtension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcShark.Connectors.TerminalSessions
{
	using System;
	using IrcShark.Extensions.Sessions;
	using IrcShark.Extensions.Terminal;

	/// <summary>
	/// Descriptes an ISecureTerminal, that requires authentification before enabling the access to terminal commands.
	/// </summary>
	public interface ISecureTerminal : ITerminal
	{
		/// <summary>
		/// Gets the session, that is used by this secure terminal.
		/// </summary>
		Session Session { get; }
	}
}
