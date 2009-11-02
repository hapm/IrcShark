// <copyright file="IrcSharkApplicationTest.cs" company="IrcShark Team">
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
    using System.Collections.Generic;
    using IrcShark;
    using NUnit.Framework;

    [TestFixture()]
    public class IrcSharkApplicationTest
    {
        [Test()]
        public void Constructor()
        {
            IrcSharkApplication instance;
            instance = new IrcSharkApplication();
            Assert.IsNotNull(instance);
        }
        
        [Test()]
        public void DefaultSettingsDirectory()
        {
            string settings;
            IrcSharkApplication instance;
            instance = new IrcSharkApplication();
            settings = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "Settings");
            Assert.AreEqual(settings, instance.SettingsDirectorys.Default);
        }
        
        [Test()]
        public void DefaultExtensionsDirectory()
        {
            string extensions;
            IrcSharkApplication instance;
            instance = new IrcSharkApplication();
            extensions = System.IO.Path.Combine(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "IrcShark"), "Extensions");
            Assert.AreEqual(extensions, instance.ExtensionsDirectorys.Default);
        }
        
        /// <summary>
        /// Tests the Extension property.
        /// </summary>
        [Test]
        public void Extensions()
        {
            IrcSharkApplication instance;
            instance = new IrcSharkApplication();
            Assert.NotNull(instance.Extensions);
            Assert.IsInstanceOf(typeof(ExtensionManager), instance.Extensions);
        }
        
        /// <summary>
        /// Tests the Log property.
        /// </summary>
        [Test]
        public void Log()
        {
            IrcSharkApplication instance;
            instance = new IrcSharkApplication();
            Assert.NotNull(instance.Log);
            Assert.IsInstanceOf(typeof(Logger), instance.Log);
        }
        
        /// <summary>
        /// Tests the Setting property.
        /// </summary>
        [Test]
        public void Setting()
        {
            IrcSharkApplication instance;
            instance = new IrcSharkApplication();
            Assert.NotNull(instance.Settings);
            Assert.IsInstanceOf(typeof(Settings), instance.Settings);
        }
    }
}
