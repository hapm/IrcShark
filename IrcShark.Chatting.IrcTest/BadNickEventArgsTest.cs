// <copyright file="BadNickEventArgsTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the tests for the BadNickEventArgs class.</summary>

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
    /// Tests for the BadNickEventArgs class.
    /// </summary>
    [TestFixture]
    public class BadNickEventArgsTest
    {
        /// <summary>
        /// Test for the constructor.
        /// </summary>
        [Test]
        public void Constructor()
        {
            IrcClient client = new IrcClient();
            IrcLine erroneousNick = new IrcLine(client, ":prefix 432 :Erroneous nickname");
            IrcLine nickInUse = new IrcLine(client, ":prefix 433 :Nickname is already in use");
            BadNickEventArgs args = new BadNickEventArgs(erroneousNick, true);
            Assert.IsTrue(args.IsLogin);
            Assert.AreEqual(BadNickReasons.ErroneusNickname, args.Reason);
            args = new BadNickEventArgs(nickInUse, false);
            Assert.IsFalse(args.IsLogin);
            Assert.AreEqual(BadNickReasons.NicknameInUse, args.Reason);
            args = new BadNickEventArgs(erroneousNick, false);
            Assert.IsFalse(args.IsLogin);
            Assert.AreEqual(BadNickReasons.ErroneusNickname, args.Reason);
            args = new BadNickEventArgs(nickInUse, true);
            Assert.IsTrue(args.IsLogin);
            Assert.AreEqual(BadNickReasons.NicknameInUse, args.Reason);
        }
    }
}
