// <copyright file="CommandCallTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcShark.Extensions.TerminalTest
{
    using System;
    using IrcShark.Extensions.Terminal;
    using NUnit.Framework;

    /// <summary>
    /// Tests for the CommandCall class.
    /// </summary>
    [TestFixture]
    public class CommandCallTest
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Test]
        public void Constructor()
        {
            CommandCall call;
            try
            { 
                call = new CommandCall(" ");
                Assert.Fail("We shouldn't be able to pass an empty string.");
            }
            catch (Exception)
            {
                // TODO: Should add some action here.
            }
            
            call = new CommandCall("testline");
            Assert.NotNull(call);
            Assert.AreEqual("testline", call.CommandName);
            Assert.AreEqual(0, call.Parameters.Length);
            
            call = new CommandCall("testline with parameters");
            Assert.AreEqual("testline", call.CommandName);
            Assert.AreEqual(2, call.Parameters.Length);
            Assert.AreEqual("with", call.Parameters[0]);
            Assert.AreEqual("parameters", call.Parameters[1]);
            
            call = new CommandCall("another testline with parameters and \"multi word\" parameters");
            Assert.AreEqual("another", call.CommandName);
            Assert.AreEqual(6, call.Parameters.Length);
            Assert.AreEqual("testline", call.Parameters[0]);
            Assert.AreEqual("with", call.Parameters[1]);
            Assert.AreEqual("parameters", call.Parameters[2]);
            Assert.AreEqual("and", call.Parameters[3]);
            Assert.AreEqual("multi word", call.Parameters[4]);
            Assert.AreEqual("parameters", call.Parameters[5]);
            
            call = new CommandCall("testline with \\\" and \"multi word \\\"\"");
            Assert.AreEqual("testline", call.CommandName);
            Assert.AreEqual(4, call.Parameters.Length);
            Assert.AreEqual("with", call.Parameters[0]);
            Assert.AreEqual("\"", call.Parameters[1]);
            Assert.AreEqual("and", call.Parameters[2]);
            Assert.AreEqual("multi word \"", call.Parameters[3]);
        }
    }
}
