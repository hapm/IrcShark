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
	[XmlRoot(Namespace = "http://www.ircshark.net/2009/settings", ElementName = "ircshark")]
	public class Settings : IXmlSerializable
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
		
		private LogHandlerSettingCollection logSettings;
		
		/// <summary>
		/// creates a new configuration instance
		/// </summary>
		public Settings ()
		{
			settingDirectorys = new DirectoryCollection();
			extensionDirectorys = new DirectoryCollection();
			loadedExtensions = new List<ExtensionInfo>();
			logSettings = new LogHandlerSettingCollection();
		}
		
		/// <summary>
		/// Gets a list of all directories searched for extension settings
		/// </summary>
		[XmlElement("settingdirs")]
		public DirectoryCollection SettingDirectorys
		{
			get { return settingDirectorys; }
		}
		
		/// <summary>
		/// Gets a list of all directories searched for extensions
		/// </summary>
		[XmlElement("extensiondirs")]
		public DirectoryCollection ExtensionDirectorys {
			get { return extensionDirectorys; }
		}
		
		public LogHandlerSettingCollection LogSettings
		{
			get { return logSettings; }
		}

		#region IXmlSerializable implementation
		XmlSchema IXmlSerializable.GetSchema ()
		{
			return XmlSchema.Read(XmlReader.Create("http://www.ircshark.net/2009/settings.xsd"), null);
		}
		
		void IXmlSerializable.ReadXml (XmlReader reader)
		{
			while (reader.Read())
			{
				switch(reader.NodeType)
				{
				case XmlNodeType.Element:
					switch (reader.Name)
					{
					case "configuration":
						ReadConfiguration(reader);
						break;
					}
					break;
				}
			}
		}
		
		private void ReadConfiguration(XmlReader reader)
		{
			while (reader.Read())
			{
				switch(reader.NodeType)
				{
				case XmlNodeType.Element:
					switch (reader.Name)
					{
					case "settingdirs":
						ReadDirectoryList(reader, settingDirectorys);
						break;
					case "extensiondirs":
						ReadDirectoryList(reader, extensionDirectorys);
						break;
					case "loaded":
						ReadLoadedExtensions(reader);
						break;
					case "logging":
						ReadLoggingSettings(reader);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case XmlNodeType.EndElement:
					return;
				}
			}			
		}
		
		private void ReadDirectoryList(XmlReader reader, DirectoryCollection dirs)
		{
			while (reader.Read())
			{
				switch(reader.NodeType)
				{
				case XmlNodeType.Element:
					switch (reader.Name)
					{
					case "directory":
						dirs.Add(reader.ReadElementContentAsString());
						break;
					}
					break;
				case XmlNodeType.EndElement:
					return;
				}
			}			
		}
		
		private void ReadLoggingSettings(XmlReader reader)
		{
			LogHandlerSetting logHandler;
			while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						if (reader.Name == "loghandler")
						{
							logHandler = new LogHandlerSetting("");
							logHandler.ReadXml(reader);
							LogSettings.Add(logHandler);
						}
						else
							reader.Skip();
						break;
				}
			}
		}
		
		private void ReadLoadedExtensions(XmlReader reader)
		{
			while(true)
			{
				try
				{
					ExtensionInfo info = new ExtensionInfo();
					info.ReadXml(reader);
					loadedExtensions.Add(info);
				}
				catch (Exception ex)
				{
					if (reader.NodeType == XmlNodeType.EndElement)
						return;
					throw new ConfigurationException("couldn't load extension info", ex);
				}
			}
		}
		
		void IXmlSerializable.WriteXml (XmlWriter writer)
		{
			writer.WriteAttributeString("xmlns", "http://www.ircshark.net/2009/settings");
			writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteAttributeString("xsi:schemaLocation", "http://www.ircshark.net/2009/settings http://www.ircshark.net/2009/settings.xsd");
			writer.WriteStartElement("configuration");
			WriteDirectoryList(writer, "settingdirs", settingDirectorys);
			WriteDirectoryList(writer, "extensiondirs", extensionDirectorys);
			WriteLoadedExtensions(writer, loadedExtensions.ToArray());
			WriteLoggingSettings(writer, logSettings.ToArray());
			writer.WriteEndElement();
		}
		
		private void WriteDirectoryList(XmlWriter writer, string tag, DirectoryCollection dirs)
		{
			if (dirs.Count > 0)
			{
				writer.WriteStartElement(tag);
				foreach (string dir in dirs)
				{
					writer.WriteElementString("directory", dir);
				}
				writer.WriteEndElement();
			}			
		}
		
		private void WriteLoadedExtensions(XmlWriter writer, ExtensionInfo[] loaded)
		{
			if (loaded.Length > 0)
			{
				writer.WriteStartElement("loaded");
				foreach (ExtensionInfo ext in loaded)
				{
					ext.WriteXml(writer);
				}
				writer.WriteEndElement();				
			}
		}
		
		private void WriteLoggingSettings(XmlWriter writer, LogHandlerSetting[] settings)
		{
			if (logSettings.Count != 0) 
			{
				writer.WriteStartElement("logging");
				foreach (LogHandlerSetting setting in settings)
				{
					writer.WriteStartElement("loghandler");
					setting.WriteXml(writer);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		#endregion

	}
}
