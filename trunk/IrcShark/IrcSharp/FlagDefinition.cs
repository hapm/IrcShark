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
		private char character;
		private ModeArt appliesTo;
		private FlagParameter setParameter;
		private FlagParameter unsetParameter;
		
		/// <summary>
		/// Creates a new FlagDefinition with the given flag character and mode art
		/// </summary>
		/// <param name="flag">
		/// the <see cref="System.Char"/> what represents this flag
		/// </param>
		/// <param name="art">
		/// the <see cref="ModeArt"/>, this flag can be applied to
		/// </param>
		public FlagDefinition(char flag, ModeArt art)
		{
			character = flag;
			appliesTo = art;
		}
		
		/// <summary>
		/// Creates a new FlagDefinition with the given flag character, mode art and parameter definition
		/// </summary>
		/// <param name="flag">
		/// the <see cref="System.Char"/> what represents this flag
		/// </param>
		/// <param name="art">
		/// the <see cref="ModeArt"/>, this flag can be applied to
		/// </param>
		/// <param name="parameters">
		/// the <see cref="FlagParameter"/> used when the flag is set or unset
		/// </param>
		public FlagDefinition(char flag, ModeArt art, FlagParameter parameters)
		{
			character = flag;
			appliesTo = art;
			setParameter = parameters;
			unsetParameter = parameters;
		}
		
		/// <summary>
		/// Creates a new FlagDefinition with the given flag character and mode art
		/// </summary>
		/// <param name="flag">
		/// A <see cref="System.Char"/>
		/// </param>
		/// <param name="art">
		/// A <see cref="ModeArt"/>
		/// </param>
		/// <param name="setParams">
		/// the <see cref="FlagParameter"/> used when the flag is set
		/// </param>
		/// <param name="unsetParams">
		/// the <see cref="FlagParameter"/> used when the flag is unset
		/// </param>
		public FlagDefinition(char flag, ModeArt art, FlagParameter setParams, FlagParameter unsetParams)
		{
			character = flag;
			appliesTo = art;
			setParameter = setParams;
			unsetParameter = unsetParams;
		}
		
		/// <value>
		/// the character to use for this flag
		/// </value>
		public char Character
		{
			get { return character; }
		}
		
		/// <value>
		/// the type of the traget this flag applies to
		/// </value>
		public ModeArt AppliesTo
		{
			get { return appliesTo; }
		}
		
		/// <value>
		/// None if there is no parameter
		/// Optional if the parameter can be given but doesn't need to
		/// Required if there needs to be a parameter
		/// </value>		
		public FlagParameter SetParameter
		{
			get { return setParameter; }
		}
		
		/// <value>
		/// None if there is no parameter
		/// Optional if the parameter can be given but doesn't need to
		/// Required if there needs to be a parameter
		/// </value>		
		public FlagParameter UnsetParameter
		{
			get { return unsetParameter; }
		}
		
		/// <summary>
		/// Tests if this flag needs a parameter or not 
		/// </summary>
		/// <param name="art">
		/// the <see cref="FlagArt"/> to check
		/// </param>
		/// <returns>
		/// true if the flag needs parameter for the given <see cref="FlagArt"/>
		/// false otherwise
		/// </returns>
		/// <remarks>
		/// Because some flags only require a parameter, when they are set you have to use the art parameter to
		/// get the result for setting or unsetting the flag
		/// </remarks>
		public bool NeedsParameter(FlagArt art) 
		{
			switch (art) {
			case FlagArt.Set:
				return SetParameter == FlagParameter.Required;
			case FlagArt.Unset:
				return UnsetParameter == FlagParameter.Required;
			}
			return false;
		}
	}
}
