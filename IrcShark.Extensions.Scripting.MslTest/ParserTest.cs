﻿// <copyright file="ParserTest.cs" company="IrcShark Team">
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
    using System.CodeDom;
    using System.IO;
    using System.Text;
    using IrcShark.Extensions.Scripting.Msl;
    using NUnit.Framework;
    using Microsoft.CSharp;
    
    [TestFixture]
    public class MslParserTest
    {
        [Test]
        public void Parse()
        {
            string testScript = "alias test {\n  echo -a Hallo $left($me, 1) $+ .\n }\nalias -l coolHu { echo -a private! }";
            StringBuilder csharpCode = new StringBuilder();
            TextReader reader = new StringReader(testScript);
            TextWriter writer = new StringWriter(csharpCode);
            Parser p = new Parser();
            CodeCompileUnit result = p.Parse(reader);
            ICodeGenerator csharpGen = new CSharpCodeProvider().CreateGenerator(writer);
            csharpGen.GenerateCodeFromCompileUnit(result, writer, null);
            Console.WriteLine(csharpCode.ToString());
            Console.Out.Flush();
        }
    }
}