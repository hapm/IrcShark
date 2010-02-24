// <copyright file="ExtensionInfoCollection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ExtensionInfoCollection class.</summary>

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
namespace IrcShark
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;

    using IrcShark.Extensions;

    /// <summary>
    /// A collection of <see cref="ExtensionInfo" />s.
    /// </summary>
    public class ExtensionInfoCollection : ICollection<Extensions.ExtensionInfo>, IXmlSerializable
    {
        /// <summary>
        /// Saves the list of ExtensionInfos.
        /// </summary>
        private List<Extensions.ExtensionInfo> extensions;
        
        /// <summary>
        /// Initializes a new instance of the ExtensionInfoCollection class.
        /// </summary>
        public ExtensionInfoCollection()
        {
            extensions = new List<Extensions.ExtensionInfo>();
        }
        
        /// <summary>
        /// Gets the number of added extensions.
        /// </summary>
        /// <value>The number of extensions in this collection.</value>
        public int Count 
        {
            get 
            {
                return extensions.Count;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether the collection is read only or not.
        /// </summary>
        /// <value>This collection isn't read only. Therefor the value is false.</value>
        /// <remarks>
        /// Later this will relate to the permission of the calling context.
        /// </remarks>
        public bool IsReadOnly 
        {
            get 
            {
                return false;
            }
        }
        
        /// <summary>
        /// Adds a new extension to the collection.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(IrcShark.Extensions.ExtensionInfo item)
        {
            extensions.Add(item);
        }
        
        /// <summary>
        /// Adds multible ExtensionInfos at once.
        /// </summary>
        /// <param name="collection">The collection to add from.</param>
        public void AddRange(IEnumerable<ExtensionInfo> collection)
        {
            extensions.AddRange(collection);
        }
        
        /// <summary>
        /// Removes all extensions from the collection.
        /// </summary>
        public void Clear()
        {
            extensions.Clear();
        }
        
        /// <summary>
        /// Checks if the given extension is in this collection.
        /// </summary>
        /// <param name="item">The extension to check for.</param>
        /// <returns>True if the extension is in the collection, else false.</returns>
        public bool Contains(IrcShark.Extensions.ExtensionInfo item)
        {
            return extensions.Contains(item);
        }
        
        /// <summary>
        /// Copys all extensions in this collection to the given array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The index to start copy to.</param>
        public void CopyTo(ExtensionInfo[] array, int arrayIndex)
        {
            extensions.CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// Returns an array of all ExtensionInfos in this collection.
        /// </summary>
        /// <returns>The array.</returns>
        public ExtensionInfo[] ToArray()
        {
            return extensions.ToArray();
        }
        
        /// <summary>
        /// Removes an extension form the collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed, else false.</returns>
        public bool Remove(IrcShark.Extensions.ExtensionInfo item)
        {
            return extensions.Remove(item);
        }
        
        /// <summary>
        /// Gets a generic enumerator for this collection.
        /// </summary>
        /// <returns>The generic enumerator.</returns>
        public IEnumerator<IrcShark.Extensions.ExtensionInfo> GetEnumerator()
        {
            return extensions.GetEnumerator();
        }
        
        /// <summary>
        /// Gets an enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (extensions as IEnumerable).GetEnumerator();
        }
        
        /// <summary>
        /// Gets the XmlSchema for this type.
        /// </summary>
        /// <returns>The XmlSchema.</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Reads a collection of all ExtensionInfos.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }
            
            reader.Read();
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "extension":
                                Extensions.ExtensionInfo info = new Extensions.ExtensionInfo();
                                info.ReadXml(reader);
                                extensions.Add(info);
                                break;
                            default:
                                reader.Skip();
                                break;
                        }
                        
                        break;
                    default:
                        reader.Read();
                        break;
                }
            }
        }
        
        /// <summary>
        /// Writes the collection to an XmlWriter.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        public void WriteXml(XmlWriter writer)
        {
            foreach (Extensions.ExtensionInfo info in extensions)
            {
                writer.WriteStartElement("extension");
                info.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
    }
}
