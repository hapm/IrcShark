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
	public class IrcStandardDefinitionTest : IIrcObjectTest
	{
		private IrcClient client;
		
		[TestFixtureSetUp()]
		public void TestFixtureSetUp() 
		{
			client = new IrcClient();
		}
		
		[Test()]
		public void Constructor()
		{
			IrcStandardDefinition standard = new IrcStandardDefinition(client);
			char[] tempChar = standard.ChannelPrefixes;
			
			Assert.AreEqual("rfc1459", standard.Version);
			
			Assert.AreEqual(2, tempChar.Length);
			Assert.AreEqual('#', tempChar[0]);		
			Assert.AreEqual('&', tempChar[1]);
			tempChar = standard.UserPrefixes;
			Assert.AreEqual(2, tempChar.Length);
			Assert.AreEqual('@', tempChar[0]);		
			Assert.AreEqual('+', tempChar[1]);
			
		}

		#region IIrcObjectTest implementation
		[Test]
		public void Client ()
		{
			IrcStandardDefinition standard = new IrcStandardDefinition(client);
			Assert.IsInstanceOf(typeof(IIrcObject), standard);
			Assert.AreSame(client, standard.Client);		
		}
		#endregion

	}
}
