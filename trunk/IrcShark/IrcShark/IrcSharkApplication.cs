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
		
		private Settings settings;
		
		/// <summary>
		/// The constructor of this class. If you create a new instance of IrcSharkApplication, you
		/// create a new instance of IrcShark it self.
		/// </summary>
		[IrcSharkAdministrationPermission(SecurityAction.Demand, Unrestricted = true)]
		public IrcSharkApplication() 
		{
			InitLogging();
			log.Log(new LogMessage(Logger.CoreChannel, 1, "Starting IrcShark, hold the line..."));
			LoadSettings();
			
			InitExtensionManager();
			SaveSettings();
			log.Log(new LogMessage(Logger.CoreChannel, 10, "Shutting down ... bye bye"));
			log.Dispose();
		}
		
		/// <summary>
		/// Loads the settings from a file or creates the default settings.
		/// </summary>
		private void LoadSettings()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Settings));
			FileInfo settingfile = new FileInfo("ircshark.config");
			
			// Loads the file if it exists
			if (settingfile.Exists)
			{
				FileStream file = null;
				try
				{
					file = settingfile.OpenRead();
					settings = serializer.Deserialize(file) as Settings;
					file.Close();
					log.Log(new LogMessage(Logger.CoreChannel, 2, "Successfully loaded settings file"));
				}
				catch (Exception ex)
				{
					log.Log(new LogMessage(Logger.CoreChannel, 301, LogLevel.Error, "Couldn't load settings file: " + ex.ToString()));
					if (file == null)
					{
						if (file.CanRead)
							file.Close();
					}
				}
			}
			else
			{
				log.Log(new LogMessage(Logger.CoreChannel, 201, LogLevel.Warning, "The settingsfile doesn't exist, creating the default one"));
			}
			
			// Creates the default settings if the settingsfile couldn't be loaded
			if (settings == null)
			{
				settings = new Settings();
				settings.ExtensionDirectorys.Add(System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "Extensions"));
				settings.SettingDirectorys.Add(System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "Settings"));
				LogHandlerSetting logSetting = new LogHandlerSetting("IrcShark.ConsoleLogHandler", "iwe");
				settings.LogSettings.Add(logSetting);
				logSetting = new LogHandlerSetting("IrcShark.FileLogHandler", "we");
				logSetting.Target = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "default.log");
				settings.LogSettings.Add(logSetting);
			}
		}
		
		private void SaveSettings()
		{
			try
			{
				FileStream settingsFile = new FileStream("IrcShark.config", FileMode.Create);
				XmlSerializer serializer = new XmlSerializer(typeof(Settings));
				serializer.Serialize(settingsFile, settings);
				settingsFile.Close();
				log.Log(new LogMessage(Logger.CoreChannel, 5, "Settings file successfully saved"));
			}
			catch (Exception ex)
			{
				log.Log(new LogMessage("IrcShark.Core", 302, LogLevel.Error, "Couldn't save the settings file: " + ex.ToString()));
			}
		}
		
		private void InitLogging()
		{
			File.AppendAllText("log.log", "--- New session ---\n"); // TODO: Read logfile-path from settings
			log = new Logger(this);
			log.LoggedMessage += DefaultConsoleLogger;
			log.LoggedMessage += DefaultFileLogger;
		}
		
		private void InitExtensionManager()
		{
			log.Log(new LogMessage(Logger.CoreChannel, 3, "Initialising extension manager..."));
			extensions = new ExtensionManager(this);
		}

		/// <summary>
		/// The default console logger
		/// </summary>
		/// <param name="logger">The logger, what raised the event.</param>
		/// <param name="msg">The message to log</param>
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
		
		/// <summary>
		/// The default file logger
		/// </summary>
		/// <param name="logger">The logger, what raised the event.</param>
		/// <param name="msg">The message to log</param>
		void DefaultFileLogger (object logger, LogMessage msg)
		{
			string format = "[{0}][{1}][{2}] {3}\r\n";			
			string logMsg = string.Format(format, msg.Time, msg.Channel, msg.Level.ToString(), msg.Message);
			File.AppendAllText("log.log", logMsg); // TODO: Read logfile-path from settings
		}
		
		/// <summary>
		/// Gets a list of all directorys used for settings lookup
		/// </summary>
		public DirectoryCollection SettingsDirectorys
		{
			get { return new DirectoryCollection(settingsDirectorys); }
		}
		
		/// <summary>
		/// Gets a list of all directorys used for extension lookup
		/// </summary>
		public DirectoryCollection ExtensionsDirectorys
		{
			get { return new DirectoryCollection(extensionsDirectorys); }
		}
		
		/// <summary>
		/// Gets the ExtensionManager of this IrcShark instance
		/// </summary>
		public ExtensionManager Extensions 
		{
			get { return extensions; }
		}

		/// <summary>
		/// Gets the <see cref="Logger" /> for this IrcShark instance
		/// </summary>
		public Logger Log 
		{
			get { return log; }
		}
	}
}