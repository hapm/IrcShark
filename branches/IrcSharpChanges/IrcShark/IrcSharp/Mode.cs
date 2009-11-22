// <copyright file="Mode.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Mode class.</summary>

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
    /// The Mode class represents one mode set in an irc mode line.
    /// </summary>
    public class Mode
    {
        /// <summary>
        /// Saves the changed flag.
        /// </summary>
        private FlagDefinition flag;
        
        /// <summary>
        /// Saves the mode parameter if any.
        /// </summary>
        private string parameter;
        
        /// <summary>
        /// Saves if the flag was set or unset.
        /// </summary>
        private FlagArt art;

        /// <summary>
        /// Initializes a new instance of the Mode class.
        /// </summary>
        /// <param name="flag">The flag, what is changed.</param>
        /// <param name="art">The way, the flag is changed.</param>
        public Mode(FlagDefinition flag, FlagArt art)
        {
            this.flag = flag;
            this.art = art;
        }

        /// <summary>
        /// Initializes a new instance of the Mode class.
        /// </summary>
        /// <param name="flag">The flag, what is changed.</param>
        /// <param name="art">The way, the flag is changed.</param>
        /// <param name="parameter">The parameter for the flag.</param>
        public Mode(FlagDefinition flag, FlagArt art, string parameter)
        {
            this.flag = flag;
            this.art = art;
            this.parameter = parameter;
        }

        /// <summary>
        /// Gets the definition of the changed flag.
        /// </summary>
        /// <value>The FlagDefinition instance for the flag.</value>
        public FlagDefinition Flag
        {
            get { return flag; }
        }

        /// <summary>
        /// Gets the parameter of the flag change.
        /// </summary>
        /// <value>The flag parameter or null if the flag doesn't need a parameter.</value>
        public string Parameter
        {
            get { return parameter; }
        }

        /// <summary>
        /// Gets the way the flag is changed.
        /// </summary>
        /// <value>The FlagArt.</value>
        public FlagArt Art
        {
            get { return art; }
        }
    }
}
