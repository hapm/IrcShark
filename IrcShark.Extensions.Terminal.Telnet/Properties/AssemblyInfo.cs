#region Using directives

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Mono.Addins;

#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("IrcShark.Extensions.Terminal.Telnet")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("IrcShark-Team")]
[assembly: AssemblyProduct("IrcShark.Extensions.Terminal.Telnet")]
[assembly: AssemblyCopyright("Copyright 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

[assembly: Addin("TelnetTerminal", "0.1")]
[assembly: AddinDependency("TerminalSessions", "0.1")]
[assembly: AddinDependency("Terminal", "0.1")]
[assembly: AddinDependency("Sessions", "0.1")]
[assembly: AddinDependency("IrcShark","0.1")]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("0.1.*")]
