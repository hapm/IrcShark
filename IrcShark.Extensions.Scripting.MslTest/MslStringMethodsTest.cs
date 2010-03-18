// <copyright file="MslStringMethodsTest.cs" company="IrcShark Team">
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
namespace IrcShark.Extensions.Scripting.MslTest
{
    using System;
    using NUnit.Framework;
    using IrcShark.Extensions.Scripting.Msl;

    /// <summary>
    /// Test for MslStringMethods class.
    /// </summary>
    [TestFixture]
    public class MslStringMethodsTest
    {
        /// <summary>
        /// Tests the Left method.
        /// </summary>
        [Test]
        public void Left()
        {
            string s1 = "hallo";
            string s2 = "foobar";
            Assert.AreEqual("h", MslStringMethods.Left(s1, 1));
            Assert.AreEqual("hal", MslStringMethods.Left(s1, 3));
            Assert.AreEqual("ha", MslStringMethods.Left(s1, -3));
            Assert.AreEqual("hallo", MslStringMethods.Left(s1, 6));
            Assert.IsEmpty(MslStringMethods.Left(s1, -6));
            Assert.AreEqual("h", MslStringMethods.Left(s1, -4));
            Assert.AreEqual("fo", MslStringMethods.Left(s2, 2));
            Assert.AreEqual("foo", MslStringMethods.Left(s2, 3));
            Assert.AreEqual("foo", MslStringMethods.Left(s2, -3));
            Assert.AreEqual("foobar", MslStringMethods.Left(s2, 6));
            Assert.IsEmpty(MslStringMethods.Left(s2, -8));
            Assert.AreEqual("fo", MslStringMethods.Left(s2, -4));
        }
        
        /// <summary>
        /// Tests the Chr method.
        /// </summary>
        [Test]
        public void Chr()
        {
            Assert.AreEqual(MslStringMethods.Chr((int)'f'), "f");
            Assert.AreEqual(MslStringMethods.Chr((int)'b'), "b");
            Assert.AreEqual(MslStringMethods.Chr((int)'a'), "a");
        }
        
        /// <summary>
        /// Tests the Asc method.
        /// </summary>
        [Test]
        public void Asc()
        {
            Assert.AreEqual(MslStringMethods.Asc("foo"), ((int)'f').ToString());
            Assert.AreEqual(MslStringMethods.Asc("bar"), ((int)'b').ToString());
            Assert.AreEqual(MslStringMethods.Asc("a"), ((int)'a').ToString());
            Assert.AreEqual(MslStringMethods.Asc("9"), ((int)'9').ToString());
        }
    }
}
