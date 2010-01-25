using System;
using System.Collections.Generic;
using System.Text;
using IrcShark.Extensions;
using System.Xml;
using System.Xml.Serialization;

namespace IrcShark
{
    public class ExtensionManagerSettings
    {
        public class EditableExtensionInfo
        {
            private Guid AssemblyGUIDValue;
            private String SourceAssemblyValue;
            private String SourceFileValue;
            private String TypeNameValue;

            public EditableExtensionInfo(ExtensionInfo baseInfo)
            {
                SourceAssemblyValue = baseInfo.SourceAssembly;
                AssemblyGUIDValue = baseInfo.AssemblyGuid;
                SourceFileValue = baseInfo.SourceFile;
                TypeNameValue = baseInfo.TypeName;
            }

            public EditableExtensionInfo()
            {
            }

            [XmlAttribute]
            public String SourceAssembly
            {
                get { return SourceAssemblyValue; }
                set { SourceAssemblyValue = value; }
            }

            [XmlAttribute]
            public String TypeName
            {
                get { return TypeNameValue; }
                set { TypeNameValue = value; }
            }

            [XmlAttribute]
            public String SourceFile
            {
                get { return SourceFileValue; }
                set { SourceFileValue = value; }
            }

            [XmlAttribute]
            public Guid AssemblyGuid
            {
                get { return AssemblyGUIDValue; }
                set { AssemblyGUIDValue = value; }
            }

            public bool Equals(ExtensionInfo info)
            {
                //return AssemblyGuid == info.AssemblyGuid;
				return TypeName == info.TypeName;
            }
        }

        private List<EditableExtensionInfo> EnabledExtensionsValue;

        public ExtensionManagerSettings()
        {
            EnabledExtensionsValue = new List<EditableExtensionInfo>();
        }

        public List<EditableExtensionInfo> EnabledExtensions
        {
            get { return EnabledExtensionsValue; }
        }
    }
}
