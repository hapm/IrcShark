// <copyright file="IrcLineTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the tests for the IrcLine class.</summary>

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
namespace IrcShark.Chatting.IrcTest
{
    using System;
    using IrcShark.Chatting.Irc;
    using NUnit.Framework;
    
    /// <summary>
    /// A test class for <see cref="IrcShark.Chatting.Irc.IrcLine"/>.
    /// </summary>
    [TestFixture()]
    public class IrcLineTest : IIrcObjectTest
    {
        private IrcClient client;
        private string line1;
        private string line2;
        private string numeric1;
        private string numeric2;
        private string wrongline;
        
        [TestFixtureSetUp()]
        public void TestFixtureSetUp() 
        {
            client = new IrcClient();
            line1 = ":address PRIVMSG dest :message to send";
            line2 = "NOTICE another_dest bla :another message";
            numeric1 = ":address 300 dest :some info";
            numeric2 = ":addr 305 destination foo :bar foo";
            wrongline = " ";
        }
        
        [Test()]
        public void Constructor()
        {
            IrcLine line = new IrcLine(client, line1);
             Assert.IsNotNull(line);
            try 
            {
                line = new IrcLine(client, wrongline);
                Assert.Fail("an incorrect raw line was parsed correctly");
            }
            catch (InvalidLineFormatException)
            {
            }
            
             Assert.IsNotNull(line);
        }
        
        [Test()]
        public void Constructor2()
        {
            IrcLine line = new IrcLine(client, null, "PRIVMSG", new string[] { "dest", "some test text" });
             Assert.IsNotNull(line);
            line = new IrcLine(client, null, "PING", null);
             Assert.IsNotNull(line);
            try 
            {
                line = new IrcLine(client, "bla bla", "PRIVMSG", new string[] { "dest", "some test text" });
                Assert.Fail("prefix with space was accpeted");
            }
            catch (InvalidLineFormatException)
            {
            }
            
            try 
            {
                line = new IrcLine(client, "bla", "dum dum", new string[] { "dest", "some test text" });
                Assert.Fail("command with space was accpeted");
            }
            catch (InvalidLineFormatException)
            {
            }
            
            try 
            {
                line = new IrcLine(client, "bla", "PRIVMSG", new string[] { "dest name", "some test text" });
                Assert.Fail("parameters with space where accpeted");
            }
            catch (InvalidLineFormatException)
            {
            }
            
            Assert.IsNotNull(line);
        }
        
        [Test()]
        public void Constructor3() 
        {
            IrcLine line = new IrcLine(client, line1);
            IrcLine l2 = new IrcLine(line);
            Assert.AreEqual("address", l2.Prefix);
            Assert.AreEqual("PRIVMSG", l2.Command);
            Assert.AreEqual("dest", l2.Parameters[0]);
            Assert.AreEqual("message to send", l2.Parameters[1]);    
            line = new IrcLine(client, line2);
            l2 = new IrcLine(line);
            Assert.IsNull(l2.Prefix);
            Assert.AreEqual("NOTICE", l2.Command);
            Assert.AreEqual("another_dest", l2.Parameters[0]);
            Assert.AreEqual("bla", l2.Parameters[1]);
            Assert.AreEqual("another message", l2.Parameters[2]);
        }
        
        [Test()]
        public void Prefix() 
        {
            IrcLine line = new IrcLine(client, line1); 
            Assert.AreEqual("address", line.Prefix);
            line = new IrcLine(client, line2); 
            Assert.IsNull(line.Prefix);
        }
        
        [Test()]
        public void Command() 
        {
            IrcLine line = new IrcLine(client, line1); 
            Assert.AreEqual("PRIVMSG", line.Command);
            line = new IrcLine(client, line2); 
            Assert.AreEqual("NOTICE", line.Command);
        }
        
        [Test()]
        public void Numeric() 
        {
            IrcLine line = new IrcLine(client, line1); 
            Assert.AreEqual(0, line.Numeric);
            line = new IrcLine(client, numeric1); 
            Assert.AreEqual(300, line.Numeric);
            line = new IrcLine(client, numeric2); 
            Assert.AreEqual(305, line.Numeric);
        }
        
        [Test()]
        public void IsNumeric() 
        {
            IrcLine line = new IrcLine(client, line1); 
            Assert.IsFalse(line.IsNumeric);
            line = new IrcLine(client, numeric1); 
            Assert.IsTrue(line.IsNumeric);
            line = new IrcLine(client, numeric2); 
            Assert.IsTrue(line.IsNumeric);
        }
        
        [Test()]
        public void Parameters() 
        {
            IrcLine line = new IrcLine(client, line1); 
            Assert.AreEqual(2, line.Parameters.Length);
            Assert.AreEqual("dest", line.Parameters[0]);
            Assert.AreEqual("message to send", line.Parameters[1]);
            line = new IrcLine(client, numeric2); 
            Assert.AreEqual(3, line.Parameters.Length);
            Assert.AreEqual("destination", line.Parameters[0]);
            Assert.AreEqual("foo", line.Parameters[1]);
            Assert.AreEqual("bar foo", line.Parameters[2]);
            line = new IrcLine(client, ":test PING"); 
            Assert.IsNull(line.Parameters);
        }
        
        [Test()]
        public void Equals() 
        {
            IrcLine l1 = new IrcLine(client, line1);
            IrcLine l2 = new IrcLine(client, line1);
            Assert.IsTrue(l1.Equals(l2));
            l2 = new IrcLine(client, line2);
            Assert.IsFalse(l1.Equals(l2));
        }
        
        [Test()]
        public void ToStringTest() 
        {
            IrcLine line = new IrcLine(client, line1);
            Assert.AreEqual(line1, line.ToString());
            line = new IrcLine(client, line2);
            Assert.AreEqual(line2, line.ToString());
            line = new IrcLine(client, "PING test");
            Assert.AreEqual("PING test", line.ToString());
        }

        #region IIrcObjectTest implementation
        public void Client()
        {
            IrcLine line = new IrcLine(client, line1);
            Assert.IsInstanceOf(typeof(IIrcObject), line);
            Assert.AreSame(client, line.Client);
        }
        #endregion
    }
}
