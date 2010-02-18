// <copyright file="NetworkSettings.cs" company="IrcShark Team">
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
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Saves all configurations for chatting network.
    /// </summary>
    [XmlRoot("network")]
    public class NetworkSettings
    {
        /// <summary>
        /// Saves the name of the network.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves the protocol of the network.
        /// </summary>
        private string protocol;
        
        /// <summary>
        /// Saves protocol specific parameters.
        /// </summary>
        private ParameterCollection parameters;
        
        /// <summary>
        /// Saves the list of servers for this network.
        /// </summary>
        private List<ServerSettings> servers;
            
        /// <summary>
        /// Initializes a new instance of the NetworkSettings class.
        /// </summary>
        public NetworkSettings()
        {
            parameters = new ParameterCollection();
            servers = new List<ServerSettings>();
        }
        
        /// <summary>
        /// Gets or sets the name of the protocol used by this network.
        /// </summary>
        /// <value>The name of the protocol.</value>
        [XmlAttribute("protocol")]
        public string Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }
        
        /// <summary>
        /// Gets or sets the name of the network.
        /// </summary>
        /// <value>The name of the network.</value>
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        /// <summary>
        /// Gets or sets a collection of protocol dependent parameters for this network.
        /// </summary>
        /// <value>A ParameterCollection with all parameter names and values.</value>
        [XmlElement("params")]
        public ParameterCollection Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
        
        /// <summary>
        /// Gets or sets the list of servers belonging to this network.
        /// </summary>
        /// <value>A list of servers belonging to this network.</value>
        [XmlElement("servers")]
        public List<ServerSettings> Servers
        {
            get { return servers; }
            set { servers = value; }
        }
    }
}
