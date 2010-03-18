// <copyright file="MslStringMethods.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the MslStringMethods class.</summary>

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
namespace IrcShark.Extensions.Scripting.Msl
{
    using System;
    
    /// <summary>
    /// The MslStringMethods class contains methods used from msl to manipulate strings.
    /// </summary>
    public static class MslStringMethods
    {
        /// <summary>
        /// Delegate used for registering the Left method.
        /// </summary>
        public delegate string LeftDelegate(string text, int left);
        
        /// <summary>
        /// Gets the beginning of a string
        /// </summary>
        /// <param name="text">The text to get the first characters from.</param>
        /// <param name="left">The number of characters retrieved starting from the left.</param>
        /// <returns>A string containing the given numer of characters, starting from the left of the given text.</returns>
        public static string Left(string text, int left)
        {
            if (left == 0)
            {
                return "";
            }
            
            if (left > text.Length)
            {
                return text;
            }
            
            if (left < 0)
            {
                left = text.Length + left;
            }
            
            if (left < 0)
            {
                return "";
            }
            
            return text.Substring(0, left);
        }
        
        /// <summary>
        /// Concats the given array of strings in one string.
        /// </summary>
        /// <param name="texts">The strings to concat.</param>
        /// <returns>A string containing all strings in the order they are saved in the text array.</returns>
        public static string Concat(string[] texts)
        {
            return string.Concat(texts);
        }
        
        /// <summary>
        /// Gets the integer value for the given character.
        /// </summary>
        /// <param name="character">The character to convert.</param>
        /// <returns>The number for the given character as a string.</returns>
        /// <remarks>
        /// If the character string contains more then one character, the first character is converted.
        /// If the character string is null or empty, string.Empty is returned.
        /// </remarks>
        public static string Asc(string character)
        {
            return string.Empty;
        }
    }
}
