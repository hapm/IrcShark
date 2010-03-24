// <copyright file="MslScriptTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the tests for the MslScript class.</summary>

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
    using IrcShark.Extensions.Scripting;
    using IrcShark.Extensions.Scripting.Msl;

    [TestFixture]
    public class MslScriptTest
    {
        [Test]
        public void Check1()
        {
            string testScript = "alias test {\n  if ($true) echo -a test\n}";
            MslScriptEngine engine = new MslScriptEngine();
            ScriptContainer script = engine.Compile("test", testScript, "Extensions\\");
            MslScript mslScript = script.Instance as MslScript;
            Assert.IsFalse(mslScript.Check(null));
            Assert.IsFalse(mslScript.Check(""));
            Assert.IsFalse(mslScript.Check(" "));
            Assert.IsFalse(mslScript.Check("0"));
            Assert.IsFalse(mslScript.Check("0.0"));
            Assert.IsFalse(mslScript.Check("$false"));
            Assert.IsTrue(mslScript.Check("a"));
            Assert.IsTrue(mslScript.Check("b"));
            Assert.IsTrue(mslScript.Check("ö"));
            Assert.IsTrue(mslScript.Check("-1"));
            Assert.IsTrue(mslScript.Check("$true"));
        }
        
        [Test, Sequential]
        public void Check3(
            [Values(  "b",   "5",    "foo", "10", "foobar")] string v1,
            [Values( "==",  "==",     "==", "==",     "==")] string op,
            [Values(   "",   "4", "foobar", "10", "foobar")] string v2,
            [Values(false, false,    false, true,     true)] bool result )
        {
            string testScript = "alias test {\n  if ($true) echo -a test\n}";
            MslScriptEngine engine = new MslScriptEngine();
            ScriptContainer script = engine.Compile("test", testScript, "Extensions\\");
            MslScript mslScript = script.Instance as MslScript;
            if (result)
            {
                Assert.IsTrue(mslScript.Check(v1, op, v2));
            }
            else
            {
                Assert.IsTrue(mslScript.Check(v1, op, v2));
            }
        }
    }
}
