// <copyright file="ModeTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the test for the Mode class.</summary>

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
    /// Tests the Mode class.
    /// </summary>
    [TestFixture]
    public class ModeTest
    {
        /// <summary>
        /// Tests the constructors of the Mode class.
        /// </summary>
        [Test]
        public void Constructor()
        {
            FlagDefinition flag1 = new FlagDefinition('m', ModeArt.Channel);
            FlagDefinition flag2 = new FlagDefinition('v', ModeArt.Channel, FlagParameter.Required);
            Mode mode = new Mode(flag1, FlagArt.Unset);
            Assert.NotNull(mode);
            Assert.AreEqual(FlagArt.Unset, mode.Art);
            Assert.AreEqual(mode.Flag, flag1);
            Assert.IsNull(mode.Parameter);
            mode = new Mode(flag2, FlagArt.Set, "nick");
            Assert.NotNull(mode);
            Assert.AreEqual(FlagArt.Set, mode.Art);
            Assert.AreEqual(mode.Flag, flag2);
            Assert.AreEqual(mode.Parameter, "nick");
        }
    }
}
