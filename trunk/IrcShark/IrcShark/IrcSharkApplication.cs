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
			//String xml = "<extension version=\"1.0\" name=\"My displayed name\">" + "<class>the full qualified name of the class implementing the extension</class>" + "<author>Someone</author>" + "<dependencies>" + "<dependency>a fullname to the extension</dependency>" + "<dependency>a second extension</dependency>" + "</dependencies>" + "</extension>";
			//ExtensionInfo info = new ExtensionInfo();
			//info.ReadXml(XmlReader.Create(new System.IO.StringReader(xml)));
			
			log = new Logger(this);
			log.LoggedMessage += DefaultConsoleLogger;
			log.LoggedMessage += DefaultFileLogger;
			log.Log(new LogMessage(Logger.CoreChannel, 1, "Starting IrcShark, hold the line..."));
			log.Log(new LogMessage(Logger.CoreChannel, 2, "Trying to load settings file"));
			IrcSharkSettings settings = new IrcSharkSettings();
			
			log.Log(new LogMessage(Logger.CoreChannel, 3, "Initialising extension manager..."));
			extensions = new ExtensionManager(this);
			settings.ExtensionDirectorys.Add(System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "Extensions"));
			settings.SettingDirectorys.Add(System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "Settings"));
			XmlSerializer serializer = new XmlSerializer(typeof(IrcSharkSettings));
			StringBuilder sb = new StringBuilder();
			StringWriter writer = new StringWriter(sb);
			serializer.Serialize(writer, settings);
			Console.Write(sb.ToString());
		}

		void DefaultConsoleLogger (object logger, LogMessage msg)
		{
			string format = "[{0}][{1}][{2}] {3}";
			switch (msg.Level)
			{
			case LogLevel.Debug:
				Console.ForegroundColor = ConsoleColor.Gray;
				break;
			case LogLevel.Warning:
				Console.ForegroundColor = ConsoleColor.Yellow;
				break;
			case LogLevel.Error:
				Console.ForegroundColor = ConsoleColor.Red;
				break;
			}
			Console.WriteLine(format, msg.Time, msg.Channel, msg.Level.ToString(), msg.Message);
			Console.ResetColor();
		}
		
		void DefaultFileLogger (object logger, LogMessage msg)
		{
			string format = "[{0}][{1}][{2}] {3}\r\n";
			
			string logMsg = string.Format(format, msg.Time, msg.Channel, msg.Level.ToString(), msg.Message);
			File.AppendAllText("log.log", logMsg); // TODO: Read logfile-path from settings
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
		
		public ExtensionManager Extensions 
		{
			get { return extensions; }
		}

		public Logger Log 
		{
			get { return log; }
		}
	}
}