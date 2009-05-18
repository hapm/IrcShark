// $Id$
// 
// Note:
// 
// Copyright (C) 2009 IrcShark Team
//  
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

using System;

namespace IrcSharp
{
	/// <summary>
	/// Represents a definition for a flag what can be set to a user or channel
	/// </summary>
	/// <remarks>
	/// Flag definitions are not hardcoded in the IrcSharp library. Following the internet draft
	/// IRC RPL_ISUPPORT Numeric Definition (draft-brocklesby-irc-isupport-03) written by E. Brocklesby
	/// the server can define custom flags. This class represents one of this definitions made by the server.
	/// See <see cref="IrcSharp.IrcStandardDefinition"/> for more information about the ISUPPORT reply.
	/// </remarks>
	public class FlagDefinition
	{
		/// <summary>
		/// Creates a new FlagDefinition with the given flag character and mode art
		/// </summary>
		/// <param name="flag">
		/// A <see cref="System.Char"/>
		/// </param>
		/// <param name="art">
		/// A <see cref="ModeArt"/>
		/// </param>
		public FlagDefinition(char flag, ModeArt art)
		{
		}
	}
}
