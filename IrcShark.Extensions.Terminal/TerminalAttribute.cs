// <copyright file="TerminalAttribute.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Session class.</summary>

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
namespace IrcShark.Extensions.Terminal
{
	using System;
	using Mono.Addins;

	/// <summary>
	/// The TerminalAttribute class is used to describe an ITerminal implementation for the IrcShark Terminal addin.
	/// </summary>
	public class TerminalAttribute : CustomExtensionAttribute
	{
        /// <summary>
        /// Saves the name of the terminal implementation.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves the identifier used to reference this terminal implementation.
        /// </summary>
        private string identifier;
        
        /// <summary>
        /// Saves the value indicating whether the terminal supports multible instances or not.
        /// </summary>
        private bool multiInstanceSupport;
        
        /// <summary>
        /// Initializes a new instance of the TerminalAttribute class.
        /// </summary>
        /// <param name="identifier">
        /// The identifier used to identify this terminal terminal implementation.
        /// </param>
        public TerminalAttribute([NodeAttribute("Identifier")]string identifier)
        {
        	Identifier = identifier;
            Name = identifier;
        }
        
        /// <summary>
        /// Initializes a new instance of the TerminalAttribute class.
        /// </summary>
        /// <param name="identifier">
        /// The identifier used to identify this terminal terminal implementation.
        /// </param>
        /// <param name="name">
        /// The name of the terminal implementation.
        /// </param>
        public TerminalAttribute([NodeAttribute("Identifier")]string identifier, [NodeAttribute("Name")]string name)
        {
        	Identifier = identifier;
            Name = name;
        }
        
        /// <summary>
        /// Initializes a new instance of the TerminalAttribute class.
        /// </summary>
        /// <param name="identifier">
        /// The identifier used to identify this terminal terminal implementation.
        /// </param>
        /// <param name="name">
        /// The name of the terminal implementation.
        /// </param>
        public TerminalAttribute([NodeAttribute("Identifier")]string identifier, [NodeAttribute("Name")]string name, [NodeAttribute("MultiInstanceSupport")]bool multiInstanceSupport)
        {
        	Identifier = identifier;
            Name = name;
            MultiInstanceSupport = multiInstanceSupport;
        }
        
        
        /// <summary>
        /// Gets or sets the name of the terminal implementation.
        /// </summary>
        /// <value>
        /// The name of the terminal implementation as a string.
        /// </value>
        [NodeAttribute]
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }
        
        
        /// <summary>
        /// Gets or sets the name of the terminal implementation.
        /// </summary>
        /// <value>
        /// The name of the terminal implementation as a string.
        /// </value>
        [NodeAttribute]
        public string Identifier {
            get {
                return identifier;
            }
            set {
                identifier = value;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the terminal implementation supports multible instances or not.
        /// </summary>
        /// <value>
        /// Its true, if there can be more than one instance of the terminal. Its false if only one instance is allowed.
        /// </value>
        [NodeAttribute]
        public bool MultiInstanceSupport
        {
        	get 
        	{
        		return multiInstanceSupport;
        	}
        	set 
        	{
        		multiInstanceSupport = value;
        	}
        }
	}
}
