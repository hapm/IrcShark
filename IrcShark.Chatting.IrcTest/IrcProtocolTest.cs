// <copyright file="IrcProtocolTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the tests for IrcProtocol class.</summary>

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
    using IrcShark.Chatting;
    using IrcShark.Chatting.Irc;
    using NUnit.Framework;
    
    /// <summary>
    /// Tests for the IrcProtocol class.
    /// </summary>
    [TestFixture]
    public class IrcProtocolTest
    {
        /// <summary>
        /// The instance used for all tests.
        /// </summary>
        private IrcProtocol instance;
        
        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = IrcProtocol.GetInstance();
        }
        
        /// <summary>
        /// Tests the Name property.
        /// </summary>
        /// <remarks>
        /// The protocol name needs to be always IRC.
        /// </remarks>
        [Test]
        public void Name()
        {
            Assert.AreEqual("IRC", instance.Name);
        }
        
        /// <summary>
        /// Tests the MutliServer property.
        /// </summary>
        /// <remarks>
        /// The irc protocol is a multiserver protocol.
        /// </remarks>
        [Test]
        public void MultiServer()
        {
            Assert.IsTrue(instance.MultiServer);
        }
        
        /// <summary>
        /// Tests the MutliServer property.
        /// </summary>
        /// <remarks>
        /// The irc protocol is a multiserver protocol.
        /// </remarks>
        [Test]
        public void MultiNetwork()
        {
            Assert.IsTrue(instance.MultiNetwork);
        }
        
        /// <summary>
        /// Tests if the CreateNetwork method creates a network.
        /// </summary>
        [Test]
        public void CreateNetwork()
        {
            INetwork network;
            network = instance.CreateNetwork("test");
            Assert.IsInstanceOf(typeof(IrcNetwork), network);
            Assert.AreEqual("test", network.Name);

            network = instance.CreateNetwork("another test");
            Assert.IsInstanceOf(typeof(IrcNetwork), network);
            Assert.AreEqual("another test", network.Name);
        }
    }
}
