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
using IrcSharp;

namespace IrcSharpTest
{
	/// <summary>
	/// a test class for <see cref="IrcSharp.IrcStandardDefinition"/>
	/// </summary>
	[TestFixture()]
	public class IrcStandardDefinitionTest
	{
		private IrcClient client1;
		
		[TestFixtureSetUp()]
		public void TestFixtureSetUp() 
		{
			client1 = new IrcClient();
		}
		
		[Test()]
		public void Constructor()
		{
			IrcStandardDefinition standard = new IrcStandardDefinition(client1);
			Assert.IsNotNull(standard);
			Assert.IsInstanceOfType(typeof(IIrcObject), standard);
			Assert.AreSame(client1, standard.Client);
		}
	}
}
