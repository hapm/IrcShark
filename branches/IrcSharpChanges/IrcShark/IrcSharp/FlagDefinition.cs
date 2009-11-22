// <copyright file="FlagDefinition.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the FlagDefinition class.</summary>

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
    /// Represents a definition for a flag what can be set to a user or channel.
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
        /// Saves the character representation of the flag.
        /// </summary>
        private char character;
        
        /// <summary>
        /// Saves in which mode commands this flag can be used.
        /// </summary>
        private ModeArt appliesTo;
        
        /// <summary>
        /// Saves if parameters are needed when setting the flag.
        /// </summary>
        private FlagParameter setParameter;
        
        /// <summary>
        /// Saves if parameters are needed when unsetting the flag.
        /// </summary>
        private FlagParameter unsetParameter;
        
        /// <summary>
        /// Initializes a new instance of the FlagDefinition class with the given flag character and mode art.
        /// </summary>
        /// <param name="flag">
        /// The <see cref="System.Char"/> what represents this flag.
        /// </param>
        /// <param name="art">
        /// The <see cref="ModeArt"/>, this flag can be applied to.
        /// </param>
        public FlagDefinition(char flag, ModeArt art)
        {
            character = flag;
            appliesTo = art;
        }
        
        /// <summary>
        /// Initializes a new instance of the FlagDefinition class with the given flag character, mode art and parameter definition.
        /// </summary>
        /// <param name="flag">
        /// The <see cref="System.Char"/> what represents this flag.
        /// </param>
        /// <param name="art">
        /// The <see cref="ModeArt"/>, this flag can be applied to.
        /// </param>
        /// <param name="parameters">
        /// The <see cref="FlagParameter"/> used when the flag is set or unset.
        /// </param>
        public FlagDefinition(char flag, ModeArt art, FlagParameter parameters)
        {
            character = flag;
            appliesTo = art;
            setParameter = parameters;
            unsetParameter = parameters;
        }
        
        /// <summary>
        /// Initializes a new instance of the FlagDefinition class with the given flag character and mode art.
        /// </summary>
        /// <param name="flag">
        /// A <see cref="System.Char"/>.
        /// </param>
        /// <param name="art">
        /// A <see cref="ModeArt"/>.
        /// </param>
        /// <param name="setParams">
        /// The <see cref="FlagParameter"/> used when the flag is set.
        /// </param>
        /// <param name="unsetParams">
        /// The <see cref="FlagParameter"/> used when the flag is unset.
        /// </param>
        public FlagDefinition(char flag, ModeArt art, FlagParameter setParams, FlagParameter unsetParams)
        {
            character = flag;
            appliesTo = art;
            setParameter = setParams;
            unsetParameter = unsetParams;
        }
        
        /// <summary>
        /// Gets the character representing this flag.
        /// </summary>
        /// <value>
        /// The character to use for this flag.
        /// </value>
        public char Character
        {
            get { return character; }
        }
        
        /// <summary>
        /// Gets the type of target, this flag applies to.
        /// </summary>
        /// <value>
        /// The type of the traget, this flag applies to.
        /// </value>
        public ModeArt AppliesTo
        {
            get { return appliesTo; }
        }
        
        /// <summary>
        /// Gets if parameters are needed for settings this flag or not.
        /// </summary>
        /// <value>
        /// None if there is no parameter.
        /// Optional if the parameter can be given but doesn't need to.
        /// Required if there needs to be a parameter.
        /// </value>
        public FlagParameter SetParameter
        {
            get { return setParameter; }
        }
        
        /// <summary>
        /// Gets if parameters are needed for unsettings this flag or not.
        /// </summary>
        /// <value>
        /// None if there is no parameter.
        /// Optional if the parameter can be given but doesn't need to.
        /// Required if there needs to be a parameter.
        /// </value>
        public FlagParameter UnsetParameter
        {
            get { return unsetParameter; }
        }
        
        /// <summary>
        /// Tests if this flag needs a parameter or not .
        /// </summary>
        /// <param name="art">
        /// The <see cref="FlagArt"/> to check.
        /// </param>
        /// <returns>
        /// True if the flag needs parameter for the given <see cref="FlagArt"/>
        /// false otherwise.
        /// </returns>
        /// <remarks>
        /// Because some flags only require a parameter, when they are set you have to use the art parameter to
        /// get the result for setting or unsetting the flag.
        /// </remarks>
        public bool NeedsParameter(FlagArt art) 
        {
            switch (art) 
            {
            case FlagArt.Set:
                return SetParameter == FlagParameter.Required;
            case FlagArt.Unset:
                return UnsetParameter == FlagParameter.Required;
            }
            return false;
        }
        
        /// <summary>
        /// Checks if the given parameter is valide for the given flag.
        /// </summary>
        /// <param name="art">The flag art to check for.</param>
        /// <param name="text">The parameter text to check.</param>
        /// <returns>If the parameter is valid true is returned, false otherwise.</returns>
        public bool IsParameter(FlagArt art, string text) 
        {
            if (art == FlagArt.Set && SetParameter == FlagParameter.None)
                return false;
            else if (art == FlagArt.Unset && UnsetParameter == FlagParameter.None)
                return false;
            
            return true;

            // TODO: disabled until ParameterCheck is needed.
            // if (ParameterCheck == null)
            //    return true;
            // return ParameterCheck.IsMatch(Parameter); 
        }
    }
}
