// $Id$
// 
// Add description here
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
using IrcShark;
using System.Diagnostics;

namespace IrcSharkStarter
{
	/// <summary>
	/// The main class of the IrcSharkStarter starts a single instance of IrcShark.
	/// </summary>
	public class Starter
	{
		private static IrcSharkApplication ircShark;
		
		public static IrcSharkApplication IrcShark
		{
			get { return ircShark; }
		}
		
		public static void Main(string[] args)
		{
			Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);;
			ircShark = new IrcSharkApplication();
			ircShark.Run();
		}

		static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			ircShark.Dispose();
		}
	}
}