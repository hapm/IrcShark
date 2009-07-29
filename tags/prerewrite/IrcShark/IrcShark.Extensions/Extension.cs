using System;
using System.Xml;

namespace IrcShark.Extensions
{
    public abstract class Extension : MarshalByRefObject
    {
        private String NameValue;
        private ExtensionInfo InfoValue;

        public Extension(String Name, ExtensionInfo ownInfo)
        {
            NameValue = Name;
        }

        public String Name
        {
            get { return NameValue; }
        }

        public ExtensionInfo Info
        {
            get { return InfoValue; }
        }
    }
}
