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
    using IrcShark.Extensions.Scripting.Msl;
    using NUnit.Framework;
    
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
        /// Tests the Right method.
        /// </summary>
        [Test]
        public void Right()
        {
            string s1 = "hallo";
            string s2 = "foobar";
            Assert.AreEqual("o", MslStringMethods.Right(s1, 1));
            Assert.AreEqual("llo", MslStringMethods.Right(s1, 3));
            Assert.AreEqual("lo", MslStringMethods.Right(s1, -3));
            Assert.AreEqual("hallo", MslStringMethods.Right(s1, 6));
            Assert.IsEmpty(MslStringMethods.Right(s1, -6));
            Assert.AreEqual("o", MslStringMethods.Right(s1, -4));
            Assert.AreEqual("ar", MslStringMethods.Right(s2, 2));
            Assert.AreEqual("bar", MslStringMethods.Right(s2, 3));
            Assert.AreEqual("bar", MslStringMethods.Right(s2, -3));
            Assert.AreEqual("foobar", MslStringMethods.Right(s2, 6));
            Assert.IsEmpty(MslStringMethods.Right(s2, -8));
            Assert.AreEqual("ar", MslStringMethods.Right(s2, -4));
        }
        
        /// <summary>
        /// Tests the IsUpper method.
        /// </summary>
        [Test]
        public void IsUpper()
        {
            Assert.IsFalse(MslStringMethods.IsUpper("ABc"));
            Assert.IsFalse(MslStringMethods.IsUpper("abc"));
            Assert.IsTrue(MslStringMethods.IsUpper("AB#"));
            Assert.IsTrue(MslStringMethods.IsUpper("CDE"));
        }
        
        /// <summary>
        /// Tests the IsLower method.
        /// </summary>
        [Test]
        public void IsLower()
        {
            Assert.IsFalse(MslStringMethods.IsLower("ABc"));
            Assert.IsFalse(MslStringMethods.IsLower("AB#"));
            Assert.IsTrue(MslStringMethods.IsLower("cd'"));
            Assert.IsTrue(MslStringMethods.IsLower("cde"));
        }
        
        /// <summary>
        /// Tests the Lower method.
        /// </summary>
        [Test]
        public void Lower()
        {
            Assert.AreEqual("test", MslStringMethods.Lower("Test"));
            Assert.AreEqual("blah", MslStringMethods.Lower("blah"));
            Assert.AreEqual("foobar", MslStringMethods.Lower("FooBar"));
        }
        
        /// <summary>
        /// Tests the Upper method.
        /// </summary>
        [Test]
        public void Upper()
        {
            Assert.AreEqual("TEST", MslStringMethods.Upper("Test"));
            Assert.AreEqual("BLAH", MslStringMethods.Upper("blah"));
            Assert.AreEqual("FOOBAR", MslStringMethods.Upper("FooBar"));
        }
        
        /// <summary>
        /// Tests the Len method.
        /// </summary>
        [Test]
        public void Len()
        {
            Assert.AreEqual(5, MslStringMethods.Len("teste"));
            Assert.AreEqual(4, MslStringMethods.Len("blah"));
            Assert.AreEqual(9, MslStringMethods.Len("tumtumtum"));
            Assert.AreNotEqual(1, MslStringMethods.Len("foo"));
            Assert.AreNotEqual(4, MslStringMethods.Len("foobar"));
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
