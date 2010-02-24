// <copyright file="ExtensionInfoBuilder.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExtensionInfoBuilder class.</summary>

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
namespace IrcShark.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;

    /// <summary>
    /// Creates ExtensionInfo objects for a given dll file.
    /// </summary>
    /// <remarks>
    /// Searches all types in an assembly. If the type inherits from Extension,
    /// an ExtensionInfo for this type is created. This class is used as a 
    /// communication class between the <see cref="ExtensionAnalyzer" /> and the 
    /// inner AppDomain.
    /// </remarks>
    internal class ExtensionInfoBuilder : MarshalByRefObject
    {
        /// <summary>
        /// Saves the instance of the source assembly.
        /// </summary>
        private Assembly sourceAssembly;
        
        /// <summary>
        /// Saves the instance of the list of all ExtenfionInfos found.
        /// </summary>
        private ExtensionInfoCollection resultExtensions;

        /// <summary>
        /// Initializes a new instance of the ExtensionInfoBuilder class for the given assembly.
        /// </summary>
        /// <param name="fileName">The file name and path to the assembly to check.</param>
        public ExtensionInfoBuilder(string fileName)
        {
            ExtensionInfo ei;
            Type[] types;
            resultExtensions = new ExtensionInfoCollection();
            Type ropt = typeof(Extension);

            sourceAssembly = Assembly.ReflectionOnlyLoadFrom(fileName);

            ropt = ReflectionOnlyTypeFromAssembly(sourceAssembly, ropt);
            try
            {
                types = sourceAssembly.GetExportedTypes();
                foreach (Type t in types)
                {
                    try
                    {
                        if (t.IsSubclassOf(ropt) && !t.IsAbstract)
                        {
                            ei = new ExtensionInfo(t);
                            resultExtensions.Add(ei);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        ei = null;
                    }
                }
            }
            catch (ReflectionTypeLoadException)
            {
                return;
            }
        }

        /// <summary>
        /// Gets all extensions of the given source assembly.
        /// </summary>
        /// <value>An array of all found ExtensionInfos.</value>
        public ExtensionInfo[] Extensions
        {
            get { return resultExtensions.ToArray(); }
        }
        
        /// <summary>
        /// Gets the reflection only type instance of a given type.
        /// </summary>
        /// <param name="asm">The assembly to search from.</param>
        /// <param name="type">The normal type instance.</param>
        /// <returns>The reflection only type.</returns>
        /// <remarks>
        /// To be able to check for superclasses of a given reflection only loaded type,
        /// you need to use the reflection only class type of the superclass. This method
        /// gets the reflection only type for a given normal type instance.
        /// </remarks>
        private static Type ReflectionOnlyTypeFromAssembly(Assembly asm, Type type)
        {
            Type resType = type;
            foreach (AssemblyName asmName in asm.GetReferencedAssemblies())
            {
                if (CompareAssemblys(asmName, type.Assembly.GetName()))
                {
                    Assembly.Load(asmName.FullName);
                    Assembly ircshark = Assembly.ReflectionOnlyLoad(asmName.FullName);
                    foreach (AssemblyName asm2 in ircshark.GetReferencedAssemblies()) 
                    {
                        Assembly.ReflectionOnlyLoad(asm2.FullName);
                    }
                    
                    foreach (Type t in ircshark.GetExportedTypes())
                    {
                        if (t.Name == "Extension")
                        {
                            resType = t;
                            break; 
                        }
                    }
                }
                else
                {
                    Assembly.ReflectionOnlyLoad(asmName.FullName);
                }
            }
            
            return resType;
        }
        
        /// <summary>
        /// Compares to AssemblyNames to check if they a re the same.
        /// </summary>
        /// <param name="asm1">The first AssemblyName.</param>
        /// <param name="asm2">The second AssemblyName.</param>
        /// <returns>Its true if the AssemblyNames are equals, false otherwise.</returns>
        /// <remarks>
        /// This is a quick and dirty solution, as the method only compares the
        /// textual name. Need to find a better method here!
        /// </remarks>
        private static bool CompareAssemblys(AssemblyName asm1, AssemblyName asm2)
        {
            // TODO change this simple comparision to something more robust.
            if (asm1.Name != asm2.Name)
            {
                return false;
            }
            
            return true;
        }
    }
}