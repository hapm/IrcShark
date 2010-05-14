// <copyright file="LeftIdentifier.cs" company="IrcShark Team">
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

namespace IrcShark.Extensions.Scripting.Msl.Identifier
{
    using System;
    
    /// <summary>
    /// Description of LeftIdentifier.
    /// </summary>
    public class LeftIdentifier : MslIdentifier
    {
        public LeftIdentifier() : base("left")
        {
        }
        
        public override string Execute(Call c)
        {
            if (c.Parameters == null)
            {
                return string.Empty;
            }
            
            if (c.Parameters.Length > 2)
            {
                throw new ArgumentOutOfRangeException("Too many parameters: $left");
            }
            
            if (c.Parameters.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Too few parameters: $left");
            }
            
            int left = 0;
            string text = c.Parameters[0];
            int.TryParse(c.Parameters[1], out left);
            if (left == 0)
            {
                return string.Empty;
            }
            
            if (left > text.Length)
            {
                return text;
            }
            
            if (left < 0)
            {
                left = text.Length - left;
            }
            
            if (left < 0)
            {
                return string.Empty;
            }
            
            return text.Substring(0, left);
        }
    }
}
