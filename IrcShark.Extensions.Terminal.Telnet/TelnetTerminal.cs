using IrcShark.Extensions.Sessions;
// <copyright file="AssemblyInfo.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Terminal.Telnet
{
	using System;
	using System.Collections.Generic;
	using IrcShark.Extensions.Terminal;
	using IrcShark.Connectors.TerminalSessions;

	/// <summary>
	/// Implements a terminal based on a telnet connection from a user.
	/// </summary>
	[Terminal("telnet")]
	public class TelnetTerminal : ISecureTerminal
	{
		/// <summary>
		/// Saves the instance of the ExtensionContext for this terminal.
		/// </summary>
		private ExtensionContext context;
		
		/// <summary>
		/// Saves the instance of the SessionManagementExtension.
		/// </summary>
		private SessionManagementExtension sessions;
		
		public Session Session {
			get {
				throw new NotImplementedException();
			}
		}
		
		public ConsoleColor ForegroundColor {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public AutoCompleteHandler AutoCompleteEvent {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public bool IsReading {
			get {
				throw new NotImplementedException();
			}
		}
    	
		public int DisplayWidth {
			get {
				return 80;
			}
		}
    	
		public int DisplayHeight {
			get {
				return 25;
			}
		}
		
		public void Open(ExtensionContext context)
		{
			this.context = context;
			this.sessions = context.Application.Extensions["IrcShark.Extensions.Sessions"] as SessionManagementExtension;
			if (sessions == null) 
				throw new InvalidOperationException("Sessions extension is missing, couldn't open telnet terminal");
			
			
		}
		
		public void Close()
		{
			throw new NotImplementedException();
		}
		
		public void Write(string text)
		{
			throw new NotImplementedException();
		}
		
		public void Write(string format, params object[] arg)
		{
			throw new NotImplementedException();
		}
		
		public void WriteLine(string line)
		{
			throw new NotImplementedException();
		}
		
		public void WriteLine(string format, params object[] arg)
		{
			throw new NotImplementedException();
		}
		
		public void WriteLine()
		{
			throw new NotImplementedException();
		}
		
		public void ResetColor()
		{
			throw new NotImplementedException();
		}
		
		public CommandCall ReadCommand()
		{
			throw new NotImplementedException();
		}
		
		public void StopReading()
		{
			throw new NotImplementedException();
		}
	}
}