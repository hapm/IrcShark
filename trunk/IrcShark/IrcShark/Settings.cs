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
        /// saves all libarys of this configuration
        /// </summary>
        private string libraryDirectory;

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
		private ExtensionInfoCollection loadedExtensions;
		
		private LogHandlerSettingCollection logSettings;		
		
		/// <summary>
		/// creates a new configuration instance
		/// </summary>
		public Settings ()
		{
			settingDirectorys = new DirectoryCollection();
			extensionDirectorys = new DirectoryCollection();
			loadedExtensions = new ExtensionInfoCollection();
			logSettings = new LogHandlerSettingCollection();
		}

        /// <summary>
        /// Gets or set the directory for the librarys
        /// </summary>
        [XmlElement("librarydir")]
        public string LibraryDirectory
        {
            get { return libraryDirectory; }
            set { libraryDirectory = value; }
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
		
		/// <summary>
		/// A collection of all <see cref="LogHandlerSetting" />s in this settings
		/// </summary>
		public LogHandlerSettingCollection LogSettings
		{
			get { return logSettings; }
		}
		
		/// <summary>
		/// Gets the configured extensions, what should be auto loaded
		/// </summary>
		public ExtensionInfoCollection LoadedExtensions
		{
			get { return loadedExtensions; }
		}

		#region IXmlSerializable implementation
		public XmlSchema GetSchema ()
		{
			return XmlSchema.Read(XmlReader.Create("http://www.ircshark.net/2009/settings.xsd"), null);
		}
		
		public void ReadXml (XmlReader reader)
		{
			reader.Read();
			while (true)
			{
				switch(reader.NodeType)
				{
				case XmlNodeType.Element:
					switch (reader.Name)
					{
					case "configuration":
						ReadConfiguration(reader);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case XmlNodeType.EndElement:
					reader.Read();
					return;
				default:
					if (!reader.Read())
						return;
					break;
				}
			}
		}
		
		private void ReadConfiguration(XmlReader reader)
		{
			reader.Read();
			while (true)
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
					reader.Read();
					return;
				default:
					if (!reader.Read())
						return;
					break;
				}
			}			
		}
		
		private static void ReadDirectoryList(XmlReader reader, DirectoryCollection dirs)
		{
			reader.Read();
			while (true)
			{
				switch(reader.NodeType)
				{
				case XmlNodeType.Element:
					switch (reader.Name)
					{
						case "directory":
							dirs.Add(reader.ReadString());
							reader.Read();
							break;
						default:
							reader.Skip();
							break;
					}
					break;
				case XmlNodeType.EndElement:
					reader.Read();
					return;
				default:
					if (!reader.Read())
						return;
					break;
				}
			}			
		}
		
		private void ReadLoggingSettings(XmlReader reader)
		{
			LogHandlerSetting logHandler;
			reader.Read();
			while (true)
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
					case XmlNodeType.EndElement:
						reader.Read();
						return;
					default:
						if (!reader.Read())
							return;
						break;
				}
			}
		}

        private void ReadLibraryDirectory(XmlReader reader)
        {
             libraryDirectory = reader.ReadString();
             reader.Read();
        }
		
		private void ReadLoadedExtensions(XmlReader reader)
		{
			reader.Read();
			while(true)
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						switch (reader.Name)
						{
							case "extension":
								try
								{
									ExtensionInfo info = new ExtensionInfo();
									info.ReadXml(reader);
									loadedExtensions.Add(info);
								}
								catch (Exception ex)
								{
									throw new ConfigurationException("couldn't load extension info", ex);
								}
								break;
							default:
								reader.Skip();
								break;
						}
						break;
					case XmlNodeType.EndElement:
						reader.Read();
						return;
					default:
						if (!reader.Read())
							return;
						break;
				}
			}
		}
		
		public void WriteXml (XmlWriter writer)
		{
			writer.WriteAttributeString("xmlns", "http://www.ircshark.net/2009/settings");
			writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteAttributeString("xsi:schemaLocation", "http://www.ircshark.net/2009/settings http://www.ircshark.net/2009/settings.xsd");
			writer.WriteStartElement("configuration");
			WriteDirectoryList(writer, "settingdirs", settingDirectorys);
			WriteDirectoryList(writer, "extensiondirs", extensionDirectorys);
            writer.WriteElementString("librarydirs", libraryDirectory);
			WriteLoadedExtensions(writer, loadedExtensions.ToArray());
			WriteLoggingSettings(writer, logSettings.ToArray());
			writer.WriteEndElement();
		}
		
		private static void WriteDirectoryList(XmlWriter writer, string tag, DirectoryCollection dirs)
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
		
		private static void WriteLoadedExtensions(XmlWriter writer, ExtensionInfo[] loaded)
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
