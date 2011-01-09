#region Using directives

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Mono.Addins;

#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("IrcShark.Extensions.Sessions")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("IrcShark.Extensions.Sessions")]
[assembly: AssemblyCopyright("Copyright 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: Addin("Sessions", "0.1")]
[assembly: AddinDependency("IrcShark","0.1")]
// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]
[assembly: IrcShark.Extensions.ProvidesRole(InternalName="IrcShark.UserManager", NameResource="UserManagerRole", DescriptionResource="UserManagerRoleDescription")]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
