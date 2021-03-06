// <copyright file="IRCServerEndPointTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the tests for the IrcServerEndPoint class.</summary>

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
    using System.Net;
    using IrcShark.Chatting.Irc;
    using NUnit.Framework;
    
    /// <summary>
    /// A test class for <see cref="IrcShark.Chatting.Irc.IrcServerEndPoint"/>.
    /// </summary>
    [TestFixture()]
    public class IrcServerEndPointTest
    {
        private string address1;
        private int port1;
        private string address2;
        private int port2;
        private IPAddress ip1;
        private IPAddress ip2;    
        
        /// <summary>
        /// Sets up the default parameters for host and port to test with.
        /// </summary>
        [TestFixtureSetUp()]
        public void TestFixtureSetUp() 
        {
            address1 = "localhost";
            port1 = 6667;
            address2 = "www.mindforge.org";
            port2 = 6668;
            ip1 = Dns.GetHostAddresses(address1)[0];
            ip2 = Dns.GetHostAddresses(address2)[0];
        }
        
        /// <summary>
        /// Tests the construciotn of a new IrcServerEndPoint.
        /// </summary>
        [Test()]
        public void Constructor() 
        {
            IrcServerEndPoint point = new IrcServerEndPoint(address1, port1);
            Assert.IsNotNull(point);
            Assert.IsInstanceOf(typeof(IrcServerEndPoint), point);
            Assert.AreEqual(address1, point.Address);
            Assert.AreEqual(ip1, point.GetIPAddress());
            Assert.AreEqual(port1, point.Port);
            point = new IrcServerEndPoint(address2, port2);
            Assert.AreEqual(address2, point.Address);
            Assert.AreEqual(ip2, point.GetIPAddress());
            Assert.AreEqual(port2, point.Port);
            
            point = new IrcServerEndPoint(ip2, port2);
            Assert.AreEqual(ip2, point.GetIPAddress());
            Assert.AreEqual(port2, point.Port);
        }
        
        [Test()]
        public void IsIdentDRequired()
        {
            IrcServerEndPoint point = new IrcServerEndPoint(address1, port1);
            Assert.IsFalse(point.IsIdentDRequired);
            point.IsIdentDRequired = true;
            Assert.IsTrue(point.IsIdentDRequired);
            point.IsIdentDRequired = false;
            Assert.IsFalse(point.IsIdentDRequired);
        }
        
        [Test()]
        public void Password()
        {
            IrcServerEndPoint point = new IrcServerEndPoint(address1, port1);
            Assert.IsNull(point.Password);
            point.Password = "secret";
            Assert.AreEqual("secret", point.Password);
            point.Password = "private";
            Assert.AreEqual("private", point.Password);
        }
        
        [Test()]
        public void Address()
        {
            IrcServerEndPoint point = new IrcServerEndPoint(address1, port1);
            Assert.AreEqual(address1, point.Address);
            point.Address = address2;
            Assert.AreEqual(address2, point.Address);
            Assert.AreEqual(ip2, point.GetIPAddress());
        }
    }
}
