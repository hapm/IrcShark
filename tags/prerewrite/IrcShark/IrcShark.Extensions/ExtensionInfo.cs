using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace IrcShark.Extensions
{
    [Serializable]
    public class ExtensionInfo
    {
        private Guid AssemblyGUIDValue;
        private String SourceAssemblyValue;
        private String SourceFileValue;
        private String TypeNameValue;

        public ExtensionInfo(Type extType)
        {
            Assembly asm;
            asm = extType.Assembly;
            SourceAssemblyValue = extType.AssemblyQualifiedName;
            //If Not PluginType.IsSubclassOf(GetType(Plugin)) Then
            //Throw New ArgumentOutOfRangeException("PluginType", PluginType.FullName & " is no subtype " & GetType(Plugin).FullName)
            //End If
            SourceFileValue = asm.CodeBase;
            TypeNameValue = extType.FullName;
            AssemblyGUIDValue = extType.GUID;
        }

        public ExtensionInfo(ExtensionInfo baseInfo)
        {
            SourceAssemblyValue = baseInfo.SourceAssembly;
            AssemblyGUIDValue = baseInfo.AssemblyGuid;
            SourceFileValue = baseInfo.SourceFile;
            TypeNameValue = baseInfo.TypeName;
        }

        public String SourceAssembly
        {
            get { return SourceAssemblyValue; }
        }

        public String TypeName
        {
            get { return TypeNameValue; }
        }

        public String SourceFile
        {
            get { return SourceFileValue; }
        }

        public Guid AssemblyGuid
        {
            get { return AssemblyGUIDValue; }
        }
    }
}
