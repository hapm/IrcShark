// <copyright file="WebUiExtension.cs" company="IrcShark Team">
// Copyright (C) 2010 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the TerminalExtension class.</summary>

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
using System.Runtime.InteropServices;

using IrcShark;
using Kayak;
using Kayak.Framework;

namespace IrcShark.Extensions.WebUi
{
	/// <summary>
	/// Description of WebUiExtension.
	/// </summary>
    [Extension(Name="WebUI", Id="IrcShark.Extensions.WebUi.WebUiExtension")]
	public class WebUiExtension : Extension
	{
		private KayakServer server = new KayakServer();
		
		public WebUiExtension()
		{
		}
		
		public override void Start(ExtensionContext context)
		{
			Context = context;
			server.UseFramework();
			server.Start();
			Context.Log.Info(Logger.CoreChannel, 1000, "WebUi server started on " + server.EndPoint);
		}
		
		public override void Stop()
		{
			server.Stop();
			Context.Log.Info(Logger.CoreChannel, 1001, "WebUi server stoped");
		}
	}
}
