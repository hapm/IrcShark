// <copyright file="ExtensionDependencyResolver.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExtensionDependencyResolver class.</summary>

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
namespace IrcShark
{
    using System;
    using System.Reflection;

    /// <summary>
    /// The ExtensionDependencyResolver helps an AppDomain to resolve
    /// dependencys to extension and library assemblys.
    /// </summary>
    public class ExtensionDependencyResolver
    {
        
        /// <summary>
        /// Initializes a new instance of the ExtensionDependencyResolver class.
        /// </summary>
        public ExtensionDependencyResolver()
        {
        }
        
        
        /// <summary>
        /// Resolves an assembly for the given ResolveEventArgs.
        /// </summary>
        /// <param name="sender">The object that wnats to resolve something.</param>
        /// <param name="args">The arguments that hold the name of what type to resolve.</param>
        /// <returns></returns>
        public Assembly Resolve(object sender, ResolveEventArgs args)
        {
            //application.Log.Log(new LogMessage(Logger.CoreChannel, 4001, LogLevel.Debug, "Resolving " + args.Name));
            return null;
        }
    }
}
