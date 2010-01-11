// $Id$
// 
// Note:
// 
// Copyright (C) 2009 IrcShark Team
//  
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

using System;
using IrcSharp;
using NUnit.Framework;

namespace IrcSharpTest
{
	/// <summary>
	/// test class for <see cref="IrcSharp.FlagDefinition"/>
	/// </summary>
	[TestFixture]
	public class FlagDefinitionTest
	{		
		[Test]
		public void Constructor1()
		{
			FlagDefinition fd = new FlagDefinition('f', ModeArt.User);
			Assert.IsNotNull(fd);
			Assert.AreEqual(ModeArt.User, fd.AppliesTo);
			Assert.AreEqual('f', fd.Character);
			fd = new FlagDefinition('g', ModeArt.Channel);
			Assert.AreEqual(ModeArt.Channel, fd.AppliesTo);
			Assert.AreEqual('g', fd.Character);
			fd = new FlagDefinition('h', ModeArt.Channel);
			Assert.AreEqual(ModeArt.Channel, fd.AppliesTo);
			Assert.AreEqual('h', fd.Character);
			fd = new FlagDefinition('i', ModeArt.User);
			Assert.AreEqual(ModeArt.User, fd.AppliesTo);
			Assert.AreEqual('i', fd.Character);
		}	
		
		[Test]
		public void Constructor2()
		{
			FlagDefinition fd = new FlagDefinition('f', ModeArt.User, FlagParameter.None);
			Assert.IsNotNull(fd);
			Assert.AreEqual(ModeArt.User, fd.AppliesTo);
			Assert.AreEqual('f', fd.Character);
			Assert.AreEqual(FlagParameter.None, fd.SetParameter);
			Assert.AreEqual(FlagParameter.None, fd.UnsetParameter);
			fd = new FlagDefinition('i', ModeArt.Channel, FlagParameter.Required);
			Assert.AreEqual(ModeArt.Channel, fd.AppliesTo);
			Assert.AreEqual('i', fd.Character);
			Assert.AreEqual(FlagParameter.Required, fd.SetParameter);
			Assert.AreEqual(FlagParameter.Required, fd.UnsetParameter);
			fd = new FlagDefinition('i', ModeArt.User, FlagParameter.Optional);
			Assert.AreEqual(ModeArt.User, fd.AppliesTo);
			Assert.AreEqual('i', fd.Character);
			Assert.AreEqual(FlagParameter.Optional, fd.SetParameter);
			Assert.AreEqual(FlagParameter.Optional, fd.UnsetParameter);
		}
		
		[Test]
		public void Constructor3()
		{
			FlagDefinition fd = new FlagDefinition('f', ModeArt.User, FlagParameter.None, FlagParameter.Optional);
			Assert.IsNotNull(fd);
			Assert.AreEqual(FlagParameter.None, fd.SetParameter);
			Assert.AreEqual(FlagParameter.Optional, fd.UnsetParameter);
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Required, FlagParameter.None);
			Assert.AreEqual(FlagParameter.Required, fd.SetParameter);
			Assert.AreEqual(FlagParameter.None, fd.UnsetParameter);
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Optional, FlagParameter.Required);
			Assert.AreEqual(FlagParameter.Optional, fd.SetParameter);
			Assert.AreEqual(FlagParameter.Required, fd.UnsetParameter);
		}
		
		[Test]
		public void NeedsParameter() {
			FlagDefinition fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Required);
			Assert.IsTrue(fd.NeedsParameter(FlagArt.Set));
			Assert.IsTrue(fd.NeedsParameter(FlagArt.Unset));
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Optional);
			Assert.IsFalse(fd.NeedsParameter(FlagArt.Set));
			Assert.IsFalse(fd.NeedsParameter(FlagArt.Unset));
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.None);
			Assert.IsFalse(fd.NeedsParameter(FlagArt.Set));
			Assert.IsFalse(fd.NeedsParameter(FlagArt.Unset));
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Required, FlagParameter.Optional);
			Assert.IsTrue(fd.NeedsParameter(FlagArt.Set));
			Assert.IsFalse(fd.NeedsParameter(FlagArt.Unset));
		}
		
		[Test]
		public void IsParameter() {
			FlagDefinition fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Required);
			Assert.IsTrue(fd.IsParameter(FlagArt.Set, "any text here"));
			Assert.IsTrue(fd.IsParameter(FlagArt.Unset, "any other text here"));
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Optional);
			Assert.IsTrue(fd.IsParameter(FlagArt.Set, "any text here"));
			Assert.IsTrue(fd.IsParameter(FlagArt.Unset, "any other text here"));
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.None);
			Assert.IsFalse(fd.IsParameter(FlagArt.Set, "bla"));
			Assert.IsFalse(fd.IsParameter(FlagArt.Unset, "foo"));
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Required, FlagParameter.None);
			Assert.IsTrue(fd.IsParameter(FlagArt.Set, "bar"));
			Assert.IsFalse(fd.IsParameter(FlagArt.Unset, "blubb"));
		}
		
		/*[Test]
		public void ParameterRegex()
		{
			FlagDefinition fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Required, ".*");
			Assert.AreEqual(fd.ParameterRegex, ".*");
			fd = new FlagDefinition('f', ModeArt.User, FlagParameter.Required, "\\d+");
			Assert.AreEqual(fd.ParameterRegex, "\\d+");	
		}*/
	}
}
