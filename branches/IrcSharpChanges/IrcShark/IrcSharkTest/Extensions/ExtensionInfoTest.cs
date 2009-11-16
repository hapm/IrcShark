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
using NUnit.Framework;
using IrcShark.Extensions;
using System.Xml;

namespace IrcSharkTest.Extensions
{	
	[TestFixture()]
	public class ExtensionInfoTest
	{		
		[Test()]
		public void Constructor()
		{
			ExtensionInfo info = new ExtensionInfo();
			Assert.IsNotNull(info);
		}
		
		[Test()]
		public void ReadXml ()
		{
			String xml = "<extension name=\"test\" version=\"1.0.0\"><class></class></extension>";
			ExtensionInfo info = new ExtensionInfo ();
			info.ReadXml (XmlReader.Create (new System.IO.StringReader (xml)));
			Assert.AreEqual ("test", info.Name);
			Assert.AreEqual (new Version ("1.0.0"), info.Version);
			Assert.IsNull (info.Author);
			Assert.IsNull (info.Description);
			Assert.IsNull (info.Dependencies);
			Assert.IsEmpty (info.Class);
			xml = "<extension version=\"1.0\" name=\"My displayed name\"><class>the full qualified name of the class implementing the extension</class><author>Someone</author><dependencies><dependency>a fullname to the extension</dependency><dependency>a second extension</dependency></dependencies></extension>";
			Console.WriteLine(xml);
			info = new ExtensionInfo ();
			info.ReadXml (XmlReader.Create (new System.IO.StringReader (xml)));
			Assert.AreEqual ("My displayed name", info.Name);
			Assert.AreEqual (new Version ("1.0"), info.Version);
			Assert.IsNull (info.Description);
			Assert.AreEqual ("a fullname to the extension", info.Dependencies[0]);
			Assert.AreEqual ("a second extension", info.Dependencies[1]);
			Assert.AreEqual (info.Class, "the full qualified name of the class implementing the extension");
		}
	}
}
