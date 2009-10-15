using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.IO;
using System.Reflection.Emit;

namespace IrcShark.Extensions
{
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
    	/// saves the instance of the source assembly
    	/// </summary>
        private Assembly sourceAssembly;
        
        /// <summary>
        /// saves the instance of the list of all ExtenfionInfos found
        /// </summary>
        private ExtensionInfoCollection resultExtensions;

        /// <summary>
        /// creates an ewn ExtensionInfoBuilder for the given dll
        /// </summary>
        /// <param name="fileName">the file name and path to the dll to check</param>
        public ExtensionInfoBuilder(string fileName)
        {
            ExtensionInfo ei;
            Type[] types;
            AssemblyName asmName;
            resultExtensions = new ExtensionInfoCollection();
            Type ropt = typeof(Extension);

            asmName = AssemblyName.GetAssemblyName(fileName);
            sourceAssembly = Assembly.ReflectionOnlyLoadFrom(fileName);

            ropt = ReflectionOnlyTypeFromAssembly(sourceAssembly, ropt);
            try
            {
                types = sourceAssembly.GetExportedTypes();
                foreach (Type t in types)
                {
                    try
                    {
                        if (t.IsSubclassOf(ropt))
                        {
                            ei = new ExtensionInfo(t);
                            resultExtensions.Add(ei);
                        }
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        ei = null;
                    }
                }
            }
            catch (ReflectionTypeLoadException)
            {
            }
        }
        
        /// <summary>
        /// Gets the reflection only type instance of a given type
        /// </summary>
        /// <param name="asm">the assembly to search from</param>
        /// <param name="type">the normal type instance</param>
        /// <returns>the reflection only type</returns>
        /// <remarks>
        /// To be able to check for superclasses of a given reflection only loaded type,
        /// you need to use the reflection only class type of the superclass. This method
        /// gets the reflection only type for a given normal type instance
        /// </remarks>
        private Type ReflectionOnlyTypeFromAssembly(Assembly asm, Type type)
        {
        	Type resType = type;
            foreach (AssemblyName asmName in asm.GetReferencedAssemblies())
            {
                if (asmName.FullName == type.Assembly.FullName)
                {
                    foreach(Type t in Assembly.ReflectionOnlyLoad(asmName.FullName).GetExportedTypes())
                    {
                        if (t.Name == "Extension")
                            resType = t;
                    }
                }
                else
                    Assembly.ReflectionOnlyLoad(asmName.FullName);
            }
            return resType;
        }

        /// <summary>
        /// Gets all extensions of the given source assembly
        /// </summary>
        public ExtensionInfo[] Extensions
        {
            get { return resultExtensions.ToArray(); }
        }
    }
}