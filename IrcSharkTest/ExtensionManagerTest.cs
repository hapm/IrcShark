// <copyright file="ExtensionManagerTest.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ChatManagerExtension class.</summary>

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
namespace IrcSharkTest
{
    using System;
    using IrcShark;
    using NUnit.Framework;
    
    [TestFixture()]
    public class ExtensionManagerTest
    {
        [Test()]
        public void Constructor()
        {
            // we can't create an instance of ExtensionManager by ourself
            // only IrcSharkApplication can do that for us
            IrcSharkApplication app = new IrcSharkApplication();
            ExtensionManager extMan = new ExtensionManager(app);
            Assert.IsNotNull(app.Extensions);
            try 
            {
                // So this is the reason why we can't do it
                extMan = new ExtensionManager(app);
                Assert.Fail("there shouldn't be two instances of ExtensionManager for one IrcSharkApplication");
            }
            catch (ArgumentException)
            {
            }
            
            Assert.AreSame(app, app.Extensions.Application);
        }
    }
}
