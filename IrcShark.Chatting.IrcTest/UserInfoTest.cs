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
using IrcShark.Chatting.Irc;

namespace IrcShark.Chatting.IrcTest
{
	/// <summary>
	/// test class for <see cref="IrcShark.Chatting.Irc.UserInfo"/>
	/// </summary>
	[TestFixture()]
	public class UserInfoTest : IIrcObjectTest
	{
		IrcClient client;
		
		[TestFixtureSetUp()]
		public void TestFixtureSetUp() 
		{
			client = new IrcClient();
		}
		
		[Test()]
		public void Constructor()
		{
			UserInfo info = new UserInfo(client, "nick!ident@host.de");
			Assert.IsNotNull(info);
			try 
			{
				info = new UserInfo(client, "blubl@bla@blubb");
				Assert.Fail("userinfo was created with @ in host");
			}
			catch (ArgumentException) {}
			try 
			{
				info = new UserInfo(client, "");
				Assert.Fail("userinfo was created with empty host");
			}
			catch (ArgumentException) {}
			try 
			{
				info = new UserInfo(client, "blu@bla!blubb");
				Assert.Fail("userinfo was created with bad host");
			}
			catch (ArgumentException) {}
			try 
			{
				info = new UserInfo(client, "!bla@blubb");
				Assert.Fail("userinfo was created without nickname");
			}
			catch (ArgumentException) {}
			try 
			{
				info = new UserInfo(client, "f!bla@");
				Assert.Fail("userinfo was created without host");
			}
			catch (ArgumentException) {}
			try 
			{
				info = new UserInfo(client, "bla!@blubb");
				Assert.Fail("userinfo was created without ident");
			}
			catch (ArgumentException) {}
		}
		
		[Test()]
		public void Constructor2()
		{
			UserInfo info = new UserInfo(new IrcLine(client, ":nick!ident@host.de CMD :test"));
			Assert.IsNotNull(info);
			try 
			{
				info = new UserInfo(new IrcLine(client, "CMD :test"));
				Assert.Fail("userinfo was created with an IrcLine what doesn't have a prefix");
			}
			catch (ArgumentException) {}
		}
		
		[Test()]
		public void NickName() 
		{
			UserInfo info = new UserInfo(client, "nick!ident@host.de");
			Assert.AreEqual("nick", info.NickName);
			info = new UserInfo(client, "me!foobar@you");
			Assert.AreEqual("me", info.NickName);
			info = new UserInfo(new IrcLine(client, ":nick!ident@host.de CMD :test"));
			Assert.AreEqual("nick", info.NickName);
			info = new UserInfo(new IrcLine(client, ":me!foobar@you CMD :test"));
			Assert.AreEqual("me", info.NickName);
		}
		
		[Test()]
		public void Ident() 
		{
			UserInfo info = new UserInfo(client, "nick!ident@host.de");
			Assert.AreEqual("ident", info.Ident);
			info = new UserInfo(client, "me!foobar@you");
			Assert.AreEqual("foobar", info.Ident);
			info = new UserInfo(new IrcLine(client, ":nick!ident@host.de CMD :test"));
			Assert.AreEqual("ident", info.Ident);
			info = new UserInfo(new IrcLine(client, ":me!foobar@you CMD :test"));
			Assert.AreEqual("foobar", info.Ident);
		}
		
		[Test()]
		public void Host() {
			UserInfo info = new UserInfo(client, "nick!ident@host.de");
			Assert.AreEqual("host.de", info.Host);
			info = new UserInfo(client, "me!foobar@you");
			Assert.AreEqual("you", info.Host);
			info = new UserInfo(new IrcLine(client, ":nick!ident@host.de CMD :test"));
			Assert.AreEqual("host.de", info.Host);
			info = new UserInfo(new IrcLine(client, ":me!foobar@you CMD :test"));
			Assert.AreEqual("you", info.Host);
		}
		
		[Test]
		public void BaseLine()
		{
			UserInfo info = new UserInfo(client, "nick!ident@host.de");
			Assert.IsNull(info.BaseLine);
			IrcLine line = new IrcLine(client, ":nick!ident@host.de CMD :test");
			info = new UserInfo(line);
			Assert.AreSame(line, info.BaseLine);
		}
		
		[Test()]
		public void ToStringTest() 
		{
			UserInfo info = new UserInfo(client, "nick!ident@host.de");
			Assert.AreEqual("nick!ident@host.de", info.ToString());
			info = new UserInfo(client, "me!foobar@you");
			Assert.AreEqual("me!foobar@you", info.ToString());
		}
		
		[Test()]
		public void Equals() 
		{
			UserInfo info1 = new UserInfo(client, "nick!ident@host.de");
			UserInfo info2 = new UserInfo(client, "nick!ident@host.de");
			Assert.IsTrue(info1.Equals(info2));
			Assert.IsTrue(info2.Equals(info1));
			info2 = new UserInfo(client, "foo!bar@you");
			Assert.IsFalse(info1.Equals(info2));
			Assert.IsFalse(info2.Equals(info1));
		}
		
		[Test()]
		public void GetHashCodeTest() 
		{
			UserInfo info = new UserInfo(client, "nick!ident@host.de");
			Assert.AreEqual("nick!ident@host.de".GetHashCode(), info.GetHashCode());
			info = new UserInfo(client, "foo!bar@me");
			Assert.AreEqual("foo!bar@me".GetHashCode(), info.GetHashCode());
		}

		#region IIrcObjectTest implementation
		public void Client ()
		{
			UserInfo info = new UserInfo(client, "nick!ident@host.de");
			Assert.AreSame(client, info.Client);
			info = new UserInfo(new IrcLine(client, ":nick!ident@host.de CMD :test"));
			Assert.AreSame(client, info.Client);
		}
		#endregion

	}
}
