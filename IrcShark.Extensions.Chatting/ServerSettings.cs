// <copyright file="ServerSettings.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Chatting
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Saves all settings for a server of a network.
    /// </summary>
    public class ServerSettings
    {
        /// <summary>
        /// Saves the name of the server.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves the address of the server.
        /// </summary>
        private string address;
        
        /// <summary>
        /// Protocol dependent parameters.
        /// </summary>
        private ParameterCollection parameters;
        
        /// <summary>
        /// Initializes a new instance of the ServerSettings class.
        /// </summary>
        public ServerSettings()
        {
            parameters = new ParameterCollection();
        }
        
        /// <summary>
        /// Gets or sets the address of the server.
        /// </summary>
        /// <value>The address of the server.</value>
        [XmlAttribute("address")]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        
        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        /// <summary>
        /// Gets or sets a collection of protocol dependent parameters for this server.
        /// </summary>
        /// <value>A ParameterCollection with all parameter names and values.</value>
        public ParameterCollection Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
    }
}
