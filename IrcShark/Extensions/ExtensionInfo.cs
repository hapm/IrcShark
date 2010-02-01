// $Id$
// 
// Note:
// 
// Copyright (C) 2009 IrcShark Team
//  
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
    using System.Reflection;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// An ExtensionInfo is used to identify an Extension.
    /// </summary>
    [XmlRoot(ElementName = "extension", DataType = "extension", Namespace = "http://www.ircshark.net/2009/extensions.xsd", IsNullable = false)]
    [XmlSchemaProviderAttribute("GetSchema")]
    [Serializable]
    public class ExtensionInfo : IXmlSerializable
    {
        /// <summary>
        /// Saves the name of the extension.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Saves the author name of the extension.
        /// </summary>
        private string author;
        
        /// <summary>
        /// Saves the description of the extension.
        /// </summary>
        private string description;
        
        /// <summary>
        /// Saves the name of the class type of the extension.
        /// </summary>
        private string classType;
        
        /// <summary>
        /// Saves the source file path of the assembly.
        /// </summary>
        private string sourceFile;
        
        /// <summary>
        /// Saves a list of all dependency extensions.
        /// </summary>
        private string[] dependencies;
        
        /// <summary>
        /// Saves the assembly guid.
        /// </summary>
        private Guid assemblyGuid;
        
        /// <summary>
        /// Saves the version number of the extension.
        /// </summary>
        private Version version;
        
        /// <summary>
        /// Saves if the extension ist trusted or not.
        /// </summary>
        private bool trusted;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionInfo class.
        /// </summary>
        public ExtensionInfo()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ExtensionInfo class.
        /// </summary>
        /// <param name="extType">The type to create the ExtensionInfo from.</param>
        public ExtensionInfo(Type extType)
        {
            Assembly asm;
            asm = extType.Assembly;
            
            // TODO Add this old versioned check for the new Extension type
            // If Not PluginType.IsSubclassOf(GetType(Plugin)) Then
            // Throw New ArgumentOutOfRangeException("PluginType", PluginType.FullName & " is no subtype " & GetType(Plugin).FullName)
            // End If
            sourceFile = asm.CodeBase;
            classType = extType.FullName;
            assemblyGuid = extType.GUID;
            version = asm.GetName().Version;
            
            /*foreach(Attribute atrb in extType.GetCustomAttributes(false))
            {
                ExtensionDependencyAttribute depAttr = atrb as ExtensionDependencyAttribute;
                if (depAttr != null) 
                {
                
                }
            }*/
            trusted = true;
        }
        
        /// <summary>
        /// Gets the name of the extension.
        /// </summary>
        /// <value>The name of the extension.</value>
        public string Name 
        {
            get { return name; }
        }
        
        /// <summary>
        /// Gets the author who wrote the extension.
        /// </summary>
        /// <value>The name of the author.</value>
        public string Author
        {
            get { return author; }
        }
        
        /// <summary>
        /// Gets a short description text for the extension.
        /// </summary>
        /// <value>The description of the extension.</value>
        public string Description
        {
            get { return description; }
        }
        
        /// <summary>
        /// Gets the full qualified type name of the class implementing the Extension.
        /// </summary>
        /// <value>The full class name as a string.</value>
        public string Class
        {
            get { return classType; }
        }
        
        /// <summary>
        /// Gets the source file of the Assembly, the extension comes from.
        /// </summary>
        /// <value>The source file path as a string.</value>
        public string SourceFile 
        {
            get { return sourceFile; }
        }
        
        /// <summary>
        /// Gets the version of this extension.
        /// </summary>
        /// <value>The version of the extension.</value>
        public Version Version 
        {
            get { return version; }
        }
        
        /// <summary>
        /// Gets a value indicating whether the ExtensionInfo is trusted or not.
        /// </summary>
        /// <value>Is true when extension is trusted, false otherwise.</value>
        public bool Trusted 
        {
            get { return trusted; }
        }
        
        /// <summary>
        /// Gets a list of all dependencies of this extension.
        /// </summary>
        /// <value>An array of all dependencies.</value>
        public string[] Dependencies 
        {
            get 
            { 
                if (dependencies != null)
                    return (string[])dependencies.Clone(); 
                return null;
            }
        }
        
        /// <summary>
        /// Gets the Guid of the assembly.
        /// </summary>
        /// <value>The assembly guid.</value>
        public Guid AssemblyGuid
        {
            get { return assemblyGuid; }
        }
        
        /// <summary>
        /// Compares this ExtensionInfo with another one.
        /// </summary>
        /// <param name="info">The ExtensionInfo to compare with.</param>
        /// <returns>Returns true, if the objects are equal, false otherwise.</returns>
        public bool CompareTo(ExtensionInfo info)
        {
            if (info == null)
                return false;
            if (!info.AssemblyGuid.Equals(AssemblyGuid))
                return false;
            if (!info.Class.Equals(Class))
                return false;
            return true;
        }

        #region IXmlSerializable implementation
        /// <summary>
        /// Returns the schema for the xml representation of an ExtensionInfo.
        /// </summary>
        /// <returns>
        /// A <see cref="XmlSchema"/>.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return XmlSchema.Read(XmlReader.Create("http://www.ircshark.net/2009/extensionmetadata.xsd"), null);
        }
        
        /// <summary>
        /// Reads all data about an ExtensionInfo from the given XmlReader.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="System.Xml.XmlReader"/> to read the data from.
        /// </param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            string name = null;
            string author = null;
            Version version = null;
            string classType = null;
            string[] dependencies = null;
            if (reader.NodeType != XmlNodeType.Element || reader.Name != "extension")
                throw new System.Runtime.Serialization.SerializationException("Couldn't load ExtensionInfo, extension tag missing");
            while (reader.MoveToNextAttribute()) 
            {
                switch (reader.LocalName) 
                {
                case "name":
                    name = reader.ReadContentAsString();
                    break;
                case "version":
                    version = new Version(reader.ReadContentAsString());
                    break;
                }
            }
            while (true) 
            {
                switch (reader.NodeType) 
                {
                case XmlNodeType.Attribute:
                    reader.Read();
                    break;
                case XmlNodeType.Element:
                    switch (reader.Name) 
                    {
                    case "class":
                        classType = reader.ReadString();
                        reader.Read();
                        break;
                    case "author":
                        author = reader.ReadString();
                        reader.Read();
                        break;
                    case "assembly":
                        assemblyGuid = new Guid(reader.ReadString());
                        reader.Read();
                        break;
                    case "description":
                        description = reader.ReadString();
                        reader.Read();
                        break;
                    case "dependencies":
                        List<string> deps = new List<string>();
                        bool done = false;
                        while (!done && reader.Read()) 
                        {
                            Console.WriteLine(reader.NodeType.ToString() + ": " + reader.Name);
                            switch (reader.NodeType) 
                            {
                            case XmlNodeType.Element:
                                if (reader.Name.Equals("dependency"))
                                    deps.Add(reader.ReadString());
                                break;
                            case XmlNodeType.EndElement:
                                if (reader.Name == "dependencies") 
                                    done = true;
                                break;
                            }
                        }
                        dependencies = deps.ToArray();
                        break;
                    }
                    break;
                case XmlNodeType.EndElement:
                    reader.Read();
                    this.name = name;
                    this.author = author;
                    this.version = version;
                    this.classType = classType;
                    this.dependencies = dependencies;
                    return;
                default:
                    reader.Read();
                    break;
                }
            }
        }
        
        /// <summary>
        /// Writes the current ExtensionInfo instance to the given XmlWriter.
        /// </summary>
        /// <param name="writer">
        /// The XmlWriter to write to <see cref="System.Xml.XmlWriter"/>.
        /// </param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("extension");
            writer.WriteAttributeString("name", Name);
            writer.WriteElementString("class", Class);
            if (Version != null)
                writer.WriteAttributeString("version", Version.ToString());
            if (!String.IsNullOrEmpty(author))
                writer.WriteElementString("author", Description);
            if (assemblyGuid != null)
                writer.WriteElementString("assembly", assemblyGuid.ToString());
            if (!String.IsNullOrEmpty(description))
                writer.WriteElementString("description", Description);
            if (dependencies != null)
            {
                if (dependencies.Length > 0) 
                {
                    writer.WriteStartAttribute("dependencies");
                    foreach (string dep in dependencies) 
                    {
                        writer.WriteElementString("dependency", dep);
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
        #endregion
    }
}
