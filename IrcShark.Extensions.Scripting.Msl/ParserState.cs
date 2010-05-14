// <copyright file="ParserState.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Scripting.Msl
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Description of ParserState.
    /// </summary>
    public class ParserState : IEquatable<ParserState>
    {
        public static bool operator ==(ParserState left, ParserState right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(ParserState left, ParserState right)
        {
            return !left.Equals(right);
        }
        
        public List<string> localVarNames = new List<string>();
        
        #region Equals and GetHashCode implementation
        // The code in this region is useful if you want to use this structure in collections.
        // If you don't need it, you can just remove the region and the ": IEquatable<ParserState>" declaration.
        public override bool Equals(object obj)
        {
            if (obj is ParserState)
            {
                return Equals((ParserState)obj); // use Equals method below
            }
            else
            {
                return false;
            }
        }
        
        public bool Equals(ParserState other)
        {
            // add comparisions for all members here
            return this.localVarNames == other.localVarNames;
        }
        
        public override int GetHashCode()
        {
            // combine the hash codes of all members here (e.g. with XOR operator ^)
            return localVarNames.GetHashCode();
        }
        #endregion
    }
}
