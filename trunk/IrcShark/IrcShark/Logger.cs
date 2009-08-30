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

namespace IrcShark
{
	public delegate void LoggedMessageEventHandler(object logger, LogMessage msg);
	
	/// <summary>
	/// The Logger class is the entrypoint to the logging system of IrcShark.
	/// </summary>
	/// <remarks>
	/// Logging a message is quite easy: simply call the Log method with the give <see cref="IrcShark.LogMessage"/>.
	/// The message is then send to all log writers by fireing the LoggedMessage event.
	/// </remarks>
	public class Logger
	{
		public const String CoreChannel = "Core";
		
		/// <summary>
		/// Holds the instance of the IrcSharkApplication, this Logger is used for
		/// </summary>
		private IrcSharkApplication application;
		
		/// <summary>
		/// The LoggedMessage event is fired when anyone logs a new message. Feel
		/// free to register your own event handler here to get all log messages of the system.
		/// </summary>
		public event LoggedMessageEventHandler LoggedMessage;
		
		/// <summary>
		/// Initialises a new instance of the Logger class
		/// </summary>
		/// <param name="app">
		/// The  <see cref="IrcSharkApplication"/>, this Logger logs messages for
		/// </param>
		public Logger (IrcSharkApplication app)
		{
			application = app;
		}
		
		/// <summary>
		/// Logs a new message to the logging system.
		/// </summary>
		/// <param name="msg">
		/// The <see cref="LogMessage"/> to log
		/// </param>
		public void Log(LogMessage msg)
		{
			if (LoggedMessage != null)
				LoggedMessage(this, msg);
		}
	}
}
