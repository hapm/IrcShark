// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
//  
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

using System;
using System.Collections.Generic;
using IrcShark.Extensions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IrcShark
{
	/// <summary>
	/// This class loads and saves the IrcShark configuration from and to a given xml file
	/// </summary>
	[XmlRoot("configuration")]
	public class IrcSharkSettings : IXmlSerializable
	{
		/// <summary>
		/// saves all settings directorys of this configuration
		/// </summary>
		private DirectoryCollection settingDirectorys;
		
		/// <summary>
		/// saves all extension directories of this configuration
		/// </summary>
		private DirectoryCollection extensionDirectorys;
		
		/// <summary>
		/// saves a list of all ExtensionInfo instances for the extensions to load, when using this configuration
		/// </summary>
		private List<ExtensionInfo> loadedExtensions;
		
		/// <summary>
		/// creates a new configuration instance
		/// </summary>
		public IrcSharkSettings ()
		{
			settingDirectorys = new DirectoryCollection();
			extensionDirectorys = new DirectoryCollection();
		}
		
		[XmlElement("settingdirs")]
		public DirectoryCollection SettingDirectorys {
			get { return settingDirectorys; }
		}
		
		[XmlElement("extensiondirs")]
		public DirectoryCollection ExtensionDirectorys {
			get { return extensionDirectorys; }
		}

		#region IXmlSerializable implementation
		XmlSchema IXmlSerializable.GetSchema ()
		{
			return XmlSchema.Read(XmlReader.Create("http://www.ircshark.net/2009/extensionmetadata.xsd"), null);
		}
		
		void IXmlSerializable.ReadXml (XmlReader reader)
		{
		}
		
		void IXmlSerializable.WriteXml (XmlWriter writer)
		{
		}
		#endregion

	}
}
