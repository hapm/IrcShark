using System.Collections.Generic;
// <copyright file="ProtocolExtension.cs" company="IrcShark Team">
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
	using IrcShark.Chatting;
	using System.Xml;

	[Mono.Addins.TypeExtensionPoint]
	public interface IProtocolExtension {
		/// <summary>
		/// Gets the protocol, represented by this extension.
		/// </summary>
		/// <value>
		/// The instance for the supported protocol.
		/// </value>
		IProtocol Protocol { get; }
	
		/// <summary>
		/// Loads a network of the supported protocol from the given NetworkSettings.
		/// </summary>
		/// <param name="settings">The settings to load from.</param>
		/// <returns>The generated instance for the network.</returns>
		/// <exception cref="UnsupportedProtocolExteption">
		/// An UnsupportedProtocolExteption is thrown, if the given settings object is for
		/// another protocol.
		/// </exception>{
		INetwork LoadNetwork(NetworkSettings settings);

		/// <summary>
		/// Saves the given network to a new NetworkSettings instance.
		/// </summary>
		/// <param name="network">The network to save.</param>
		/// <returns>The setting instance holding all settings of the network.</returns>
		/// <exception cref="UnsupportedProtocolExteption">
		/// An UnsupportedProtocolExteption is thrown, if the given network object is for
		/// another protocol.
		/// </exception>
		NetworkSettings SaveNetwork(INetwork network);
		
		/// <summary>
		/// Saves the given network in the provided XmlWriter.
		/// </summary>
		/// <param name="network">The network to save.</param>
		/// <param name="writer">The writer to use.</param>
		void SerializeNetwork(INetwork network, XmlWriter writer);
		
		/// <summary>
		/// Serializes all networks in the given collection, that can be handled by the implementing extension.
		/// </summary>
		/// <param name="networks">The networks to serialize.</param>
		/// <param name="writer">The writer to serialze to.</param>
		void SerializeNetworks(IEnumerable<INetwork> networks, XmlWriter writer);
		
		/// <summary>
		/// Deserialze a list of networks in an XmlReader.
		/// </summary>
		/// <param name="reader">The reader to read the networks from.</param>
		/// <returns>A list of deserialized networks.</returns>
		/// <remarks>
		/// The reader must be at a networks-Tag start element, and the protocol attribute needs
		/// to match the protocol provided by the implementing extension.
		/// </remarks>
		IEnumerable<INetwork> DeserializeNetworks(XmlReader reader);
	}
}
