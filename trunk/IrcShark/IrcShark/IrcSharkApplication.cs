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
using System.Collections.Generic;
using System.Security.Permissions;
using IrcShark.Policy;
using IrcShark.Extensions;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace IrcShark
{
	/// <summary>
	/// The main class of an IrcShark instance. You can get all references you want to have if 
	/// you have an instance of this class and the needed permissions to access the fields.
	/// </summary>
    /// <author>Alpha</author>
    /// <since version="0.1"/>
	public class IrcSharkApplication
	{		
		/// <summary>
		/// Saves the ExtensionManager instance for this IrcShark instance  
		/// </summary>
		private ExtensionManager extensions;
		
		/// <summary>
		/// saves the extension directorys for this IrcShark instance
		/// </summary>
		private List<string> extensionsDirectorys;
		
		/// <summary>
		/// saves the settings directorys for this IrcShark instance 
		/// </summary>
		private List<string> settingsDirectorys;
		
		/// <summary>
		/// the Logger instance belonging to this IrcSharkApplication
		/// </summary>
		private Logger log;
		
		/// <summary>
		/// The contructor of this class. If you create a new instance of IrcSharkApplication, you
		/// create a new instance of IrcShark it self.
		/// </summary>
		[IrcSharkAdministrationPermission(SecurityAction.Demand, Unrestricted = true)]
		public IrcSharkApplication() 
		{
			String xml = "<extension version=\"1.0\" name=\"My displayed name\">" + "<class>the full qualified name of the class implementing the extension</class>" + "<author>Someone</author>" + "<dependencies>" + "<dependency>a fullname to the extension</dependency>" + "<dependency>a second extension</dependency>" + "</dependencies>" + "</extension>";
			ExtensionInfo info = new ExtensionInfo();
			log = new Logger();
			info.ReadXml(XmlReader.Create(new System.IO.StringReader(xml)));
			
			extensions = new ExtensionManager(this);
			extensionsDirectorys = new List<string>();
			extensionsDirectorys.Add(System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "Extensions"));

			settingsDirectorys = new List<string>();
			settingsDirectorys.Add(System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "Settings"));
			IrcSharkSettings settings = new IrcSharkSettings();
			XmlSerializer serializer = new XmlSerializer(typeof(IrcSharkSettings));
			StringBuilder sb = new StringBuilder();
			StringWriter writer = new StringWriter(sb);
			serializer.Serialize(writer, settings);
			Console.Write(sb.ToString());
		}
		
		/// <summary>
		/// A list of all directorys used for settings lookup
		/// </summary>
		public DirectoryCollection SettingsDirectorys
		{
			get { return new DirectoryCollection(settingsDirectorys); }
		}
		
		/// <summary>
		/// A list of all directorys used for extension lookup
		/// </summary>
		public DirectoryCollection ExtensionsDirectorys
		{
			get { return new DirectoryCollection(extensionsDirectorys); }
		}
		
		public ExtensionManager Extensions {
			get { return extensions; }
		}

		public Logger Log {
			get { return log; }
		}
	}
}