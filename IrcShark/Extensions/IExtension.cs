// <copyright file="Extension.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Extension class.</summary>

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
namespace IrcShark.Extensions
{
	using System;
	using Mono.Addins;
	
	[TypeExtensionPoint(ExtensionAttributeType=typeof(ExtensionAttribute))]
	public interface IExtension : IExtensionObject
	{
		/// <summary>
		/// Gets the context of this Extension.
		/// </summary>
		/// <value>The context.</value>
		ExtensionContext Context { get; }
		
		/// <summary>
		/// Gets the id of the extension from attributes.
		/// </summary>
		/// <value>The id of the extension.</value>
		string Id { get; }
		
		/// <summary>
		/// Gets the display name of the extension from attributes.
		/// </summary>
		/// <value>The name of the extension.</value>
		string Name { get; }
		
		/// <summary>
		/// Starts the extension after the initialisation of IrcShark.
		/// </summary>
		/// <param name="context">
		/// The context the extension runs in.
		/// </param>
		void Start(ExtensionContext context);
		
		/// <summary>
		/// Stops the extension before IrcShark quits or the extension is unloaded.
		/// </summary>
		void Stop();
	}
}
