﻿// <copyright file="MslDummyMethods.cs" company="IrcShark Team">
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

    public delegate string SimpleStringIdentifier();
    
    public delegate string MultiParamIdentifierDelegate(string[] parameters);
    
    /// <summary>
    /// Description of MslDummyMethods.
    /// </summary>
    public static class MslDummyMethods
    {
        public static string Me()
        {
            return "Unnamed";
        }
        
        public static string True()
        {
            return "$true";
        }
        
        public static string False()
        {
            return "$false";
        }
    }
}
