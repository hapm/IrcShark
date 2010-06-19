// <copyright file="Call.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Call class.</summary>

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
    /// Description of Call.
    /// </summary>
    public class Call
    {
        private string[] parameters;
        private bool isIdentifier;
        private string methodName;
        private string property;
        
        public Call(string name, string[] parameters, bool ident, string property)
        {
            this.parameters = parameters;
            this.isIdentifier = ident;
            this.methodName = name;
            this.property = property;
        }
        
        public string[] Parameters
        {
            get { return parameters; }
        }
        
        public bool IsIdentifier
        {
            get { return isIdentifier; }
        }
        
        public string MethodName
        {
            get { return methodName; }
        }
        
        public string Property
        {
            get { return property; }
        }
    }
}
