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
using IrcShark;
using NUnit.Framework;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace IrcSharkTest
{


	[TestFixture()]
	public class IrcSharkSettingsTest
	{

		[Test()]
		public void XmlSerialization()
		{
			IrcSharkSettings settings = new IrcSharkSettings();
			XmlSerializer serializer = new XmlSerializer(typeof(IrcSharkSettings));
			StringBuilder sb = new StringBuilder();
			StringWriter writer = new StringWriter(sb);
			serializer.Serialize(writer, settings);
			Console.Write(sb.ToString());
		}
	}
}
