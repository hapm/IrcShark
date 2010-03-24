// <copyright file="MslScriptEngineTest.cs" company="IrcShark Team">
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
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Text;
    using Microsoft.CSharp;
    using IrcShark.Extensions.Scripting;
    using IrcShark.Extensions.Scripting.Msl;
    using NUnit.Framework;
    
    [TestFixture]
    public class MslScriptEngineTest
    {
        [Test]
        public void Compile()
        {
            string testScript = "alias test {\n  echo -a Hallo $left($me, 1) $+ .\n }\nalias -l coolHu { echo -a private! }";
            StringBuilder sourceCode = new StringBuilder();
            MslScriptEngine engine = new MslScriptEngine();
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            ScriptContainer script = engine.Compile("test", testScript, "Extensions\\");
            CSharpCodeProvider provider = new CSharpCodeProvider();
            provider.GenerateCodeFromCompileUnit(script.ScriptDom, new StringWriter(sourceCode), options);
            script.Instance.Execute("Aliastest", new object[] { null });
        }
        
        [Test]
        public void CompileIf()
        {
            string testScript = "alias test {\n  if ($true) echo -a test\n}";
            StringBuilder sourceCode = new StringBuilder();
            MslScriptEngine engine = new MslScriptEngine();
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            ScriptContainer script = engine.Compile("test", testScript, "Extensions\\");
            CSharpCodeProvider provider = new CSharpCodeProvider();
            provider.GenerateCodeFromCompileUnit(script.ScriptDom, new StringWriter(sourceCode), options);
            script.Instance.Execute("Aliastest", new object[] { null });
        }            
    }
}
