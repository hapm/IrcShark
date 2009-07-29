using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.IO;
using System.Reflection.Emit;
using IrcShark.Extensions;

namespace IrcShark
{
    /// <summary>
    /// Creates ExtensionInfo objects for a given dll file.
    /// </summary>
    class ExtensionInfoBuilder : MarshalByRefObject
    {
        private Assembly SourceAssembly;
        private List<ExtensionInfo> ResultExtensions;

        public ExtensionInfoBuilder(String fileName)
        {
            ExtensionInfo ei;
            Type[] types;
            AssemblyName AsmName;
            Type ropt = typeof(Extension);
            ResultExtensions = new List<ExtensionInfo>();

            AsmName = AssemblyName.GetAssemblyName(fileName);
            SourceAssembly = Assembly.ReflectionOnlyLoadFrom(fileName);

            foreach (AssemblyName asm in SourceAssembly.GetReferencedAssemblies())
            {
                if (asm.FullName == typeof(Extension).Assembly.FullName)
                {
                    foreach(Type t in Assembly.ReflectionOnlyLoad(asm.FullName).GetExportedTypes())
                    {
                        if (t.Name == "Extension")
                        {
                            ropt = t;
                        }
                    }
                }
                else
                    Assembly.ReflectionOnlyLoad(asm.FullName);
            }
            try
            {
                types = SourceAssembly.GetExportedTypes();
                foreach (Type t in types)
                {
                    try
                    {
                        if (t.IsSubclassOf(ropt))
                        {
                            ei = new ExtensionInfo(t);
                            ResultExtensions.Add(ei);
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

        public ExtensionInfo[] Extensions
        {
            get { return ResultExtensions.ToArray(); }
        }
    }
}
