// <copyright file="AssemblyInfo.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains informations about the assembly.</summary>

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
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Addins;
using IrcShark.Extensions;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.
[assembly: AssemblyTitle("IrcShark.Core")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("IrcShark")]
[assembly: AssemblyProduct("IrcShark.Core")]
[assembly: AssemblyCopyright("IrcShark Team")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AddinRoot("IrcShark", "0.1")]
[assembly: ExtensionPoint (Path="/IrcShark/Roles", ExtensionAttributeType=typeof(ProvidesRoleAttribute))]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.
[assembly: AssemblyVersion("0.1.*")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
