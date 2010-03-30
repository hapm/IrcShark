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
        /// Gets the beginning of a string.
        /// </summary>
        /// <param name="text">The text to get the first characters from.</param>
        /// <param name="left">The number of characters retrieved starting from the left.</param>
        /// <returns>A string containing the given number of characters, starting from the left of the given text.</returns>
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
        /// Gets the end of a string.
        /// </summary>
        /// <param name="text">The text to get the last characters from.</param>
        /// <param name="left">The number of characters retrieved starting from the right.</param>
        /// <returns>A string containing the given number of characters, starting from the right of the given text.</returns>
        public static string Right(string text, int left)
        {
            return null;
        }
        
        /// <summary>
        /// Checks if the given string only contains uppercase characters.
        /// </summary>
        /// <param name="text">The string to text.</param>
        /// <returns>Its true if the text only contains uppercase characters, else false.</returns>
        public static bool IsUpper(string text)
        {
            return false;
        }
        
        /// <summary>
        /// Checks if the given string only contains lowercase characters.
        /// </summary>
        /// <param name="text">The string to text.</param>
        /// <returns>Its true if the text only contains lowercase characters, else false.</returns>
        public static bool IsLower(string text)
        {
            return false;
        }
        
        /// <summary>
        /// Determines the length of the given string.
        /// </summary>
        /// <param name="text">The text to get the length from.</param>
        /// <returns></returns>
        public static int Len(string text)
        {
            return text.Length;
        }
        
        /// <summary>
        /// Gets the given string but replacing all upper characters with there lower part.
        /// </summary>
        /// <param name="text">The text to convert to lower case.</param>
        /// <returns>The given text with lowercase characters only.</returns>
        public static string Lower(string text)
        {
            return text;
        }
        
        /// <summary>
        /// Gets the given string but replacing all lower characters with there upper part.
        /// </summary>
        /// <param name="text">The text to convert to upper case.</param>
        /// <returns>The given text with uppercase characters only.</returns>
        public static string Upper(string text)
        {
            return text;
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
            if (string.IsNullOrEmpty(character)) {
                return string.Empty;
            } else {
                return ((int)character[0]).ToString();
            }
        }
        
        /// <summary>
        /// Gets the string for the given ASCII code.
        /// </summary>
        /// <param name="asciiCode">The ASCII code to convert.</param>
        /// <returns>The character for the given ASCII code as a string.</returns>
        public static string Chr(int asciiCode)
        {
            return ((char)asciiCode).ToString();
        }
    }
}
