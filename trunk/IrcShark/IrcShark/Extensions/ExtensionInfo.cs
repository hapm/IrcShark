// $Id$
// 
// Note:
// 
// Copyright (C) 2009 IrcShark Team
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
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IrcShark.Extensions
{
	[XmlRoot(ElementName = "extensions", DataType = "extensions", Namespace = "http://www.ircshark.net/2009/extensionmetadata.xsd", IsNullable = false)]
	[XmlSchemaProviderAttribute("GetSchema")]
	public class ExtensionInfo : IXmlSerializable
	{		
		private string name;
		private string author;
		private string description;
		private string classType;
		private Version version;
		private string[] dependencies;
		private bool trusted;
		
		/// <summary>
		/// creates a new ExtensionInfo instance
		/// </summary>
		public ExtensionInfo()
		{
			trusted = false;
		}
		
		/// <summary>
		/// the name of the extension
		/// </summary>
		public string Name {
			get { return name; }
		}
		
		/// <summary>
		/// the author who wrote the extension
		/// </summary>
		public string Author {
			get { return author; }
		}
		
		/// <summary>
		/// a short description text for the extension
		/// </summary>
		public string Description {
			get { return description; }
		}
		
		/// <summary>
		/// the full qualified type name of the class implementing the Extension
		/// </summary>
		public string Class {
			get { return classType; }
		}
		
		/// <summary>
		/// the version of this extension
		/// </summary>
		public Version Version {
			get { return version; }
		}
		
		public bool Trusted {
			get { return trusted; }
		}
		
		/// <summary>
		/// a list of all dependencies of this extension
		/// </summary>
		public string[] Dependencies 
		{
			get 
			{ 
				if (dependencies != null)
					return (string[])dependencies.Clone(); 
				return null;
			}
		}

		/// <summary>
		/// returns the schema for the xml representation of an ExtensionInfo
		/// </summary>
		/// <returns>
		/// A <see cref="XmlSchema"/>
		/// </returns>
		#region IXmlSerializable implementation
		public XmlSchema GetSchema ()
		{
			return XmlSchema.Read(XmlReader.Create("http://www.ircshark.net/2009/extensionmetadata.xsd"), null);
		}
		
		/// <summary>
		/// reads all data about an ExtensionInfo from the given XmlReader
		/// </summary>
		/// <param name="reader">
		/// the <see cref="System.Xml.XmlReader"/> to read the data from
		/// </param>
		public void ReadXml (System.Xml.XmlReader reader)
		{
			String name = null;
			String author = null;
			Version version = null;
			String classType = null;
			String[] dependencies = null;
			reader.Read();
			if (reader.NodeType != XmlNodeType.Element || reader.Name != "extension") {	
				throw new System.Runtime.Serialization.SerializationException("Couldn't load ExtensionInfo, extension tag missing");
			}
			while (reader.MoveToNextAttribute()) {
				switch (reader.LocalName) {
				case "name":
					name = reader.ReadContentAsString();
					break;
				case "version":
					version = new Version(reader.ReadContentAsString());
					break;
				}
			}
			while (reader.Read()) {
				switch (reader.NodeType) {
				case XmlNodeType.Attribute:
					break;
				case XmlNodeType.Element:
					switch (reader.Name) {
					case "class":
						classType = reader.ReadString();
						break;
					case "author":
						author = reader.ReadString();
						break;
					case "description":
						description = reader.ReadString();
						break;
					case "dependencies":
						List<String> deps = new List<String>();
						bool done = false;
						while (!done && reader.Read()) {
							Console.WriteLine(reader.NodeType.ToString()+": "+reader.Name);
							switch (reader.NodeType) {
							case XmlNodeType.Element:
								if (reader.Name.Equals("dependency"))
									deps.Add(reader.ReadString());
								break;
							case XmlNodeType.EndElement:
								if (reader.Name == "dependencies") {
									done = true;
								}
								break;
							}
						}
						dependencies = deps.ToArray();
						break;
					}
					break;
				}
			}
			this.name = name;
			this.author = author;
			this.version = version;
			this.classType = classType;
			this.dependencies = dependencies;
		}
		
		/// <summary>
		/// writes the current ExtensionInfo instance to the given XmlWriter
		/// </summary>
		/// <param name="writer">
		/// the XmlWriter to write to <see cref="System.Xml.XmlWriter"/>
		/// </param>
		public void WriteXml (System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement("extension");
			writer.WriteAttributeString("name", Name);
			writer.WriteAttributeString("version", Version.ToString());
			writer.WriteElementString("class", Class);
			if (!String.IsNullOrEmpty(description))
				writer.WriteElementString("description", Description);
			if (dependencies.Length > 0) 
			{
				writer.WriteStartAttribute("dependencies");
				foreach (string dep in dependencies) 
				{
					writer.WriteElementString("dependency", dep);
				}
				writer.WriteEndElement();				
			}
			writer.WriteEndElement();
		}
		#endregion

	}
}
