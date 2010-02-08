// <copyright file="IrcNetworkTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains all tests of the IrcNetwork class.</summary>

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
    /// All tests for the IrcNetwork class.
    /// </summary>
    [TestFixture]
    public class IrcNetworkTest
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Test]
        public void Constructor()
        {
            IrcNetwork network = new IrcNetwork("test");
            Assert.AreEqual("test", network.Name);
            network = new IrcNetwork("foo");
            Assert.AreEqual("foo", network.Name);
        }
        
        /// <summary>
        /// Tests the AddServer message.
        /// </summary>
        [Test]
        public void AddServer() 
        {
            IrcNetwork network = new IrcNetwork("bar");
            IrcServerEndPoint server = network.AddServer("test", "localhost:6667");
            Assert.NotNull(server);
            Assert.AreEqual("localhost", server.Address);
            Assert.AreEqual("test", server.Name);
            Assert.AreEqual(6667, server.Port);
            Assert.AreEqual(network["test"], server);
            Assert.AreEqual(1, network.ServerCount);
            network = new IrcNetwork("bar");
            server = network.AddServer("foo", "www.google.de:6669");
            Assert.NotNull(server);
            Assert.AreEqual("www.google.de", server.Address);
            Assert.AreEqual("foo", server.Name);
            Assert.AreEqual(network["foo"], server);
            Assert.AreEqual(6669, server.Port);
            network.AddServer("test", "localhost:6667");
            Assert.AreEqual(2, network.ServerCount);
            try
            {
                network.AddServer("foo", "localhost:6669");
                Assert.Fail("Added a second server with the same name, as an already added one");
            }
            catch (ArgumentException)
            {
            }
        }
        
        /// <summary>
        /// Tests the RemoveServer method.
        /// </summary>
        [Test]
        public void RemoveServer() 
        {
            IrcNetwork network = new IrcNetwork("test");
            network.AddServer("test", "localhost:6667");
            network.RemoveServer("test");
            Assert.AreEqual(0, network.ServerCount);
            network = new IrcNetwork("test");
            network.AddServer("test1", "localhost:6667");
            network.AddServer("test2", "localhost:6667");
            Assert.IsTrue(network.RemoveServer("test2"));
            Assert.AreEqual(1, network.ServerCount);
            Assert.IsNull(network["test2"]);
            Assert.IsNotNull(network["test1"]);
            Assert.IsTrue(network.RemoveServer("test1"));
            Assert.AreEqual(0, network.ServerCount);
            Assert.IsNull(network["test1"]);
        }
    }
}
