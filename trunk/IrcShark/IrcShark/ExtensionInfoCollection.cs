/*
 * Erstellt mit SharpDevelop.
 * Benutzer: markus
 * Datum: 21.09.2009
 * Zeit: 21:00
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using IrcShark.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace IrcShark
{
	/// <summary>
	/// A collection of <see cref="ExtensionInfo" />s.
	/// </summary>
	public class ExtensionInfoCollection : ICollection<Extensions.ExtensionInfo>, IXmlSerializable
	{
		/// <summary>
		/// Saves the list of ExtensionInfos
		/// </summary>
		private List<Extensions.ExtensionInfo> extensions;
		
		/// <summary>
		/// Creates a new collection of ExtensionInfos
		/// </summary>
		public ExtensionInfoCollection()
		{
			extensions = new List<Extensions.ExtensionInfo>();
		}
		
		/// <summary>
		/// Gets the number of added extensions
		/// </summary>
		public int Count {
			get {
				return extensions.Count;
			}
		}
		
		/// <summary>
		/// Gets if the collection is read only or not
		/// </summary>
		/// <remarks>
		/// Later this will relate to the permission of the calling context
		/// </remarks>
		public bool IsReadOnly {
			get {
				return true;
			}
		}
		
		/// <summary>
		/// Adds a new extension to the collection
		/// </summary>
		/// <param name="item">item to add</param>
		public void Add(IrcShark.Extensions.ExtensionInfo item)
		{
			extensions.Add(item);
		}
		
		public void AddRange(IEnumerable<ExtensionInfo> collection)
		{
			extensions.AddRange(collection);
		}
		
		/// <summary>
		/// Removes all extensions from the collection
		/// </summary>
		public void Clear()
		{
			extensions.Clear();
		}
		
		/// <summary>
		/// checks if the given extension is in this collection
		/// </summary>
		/// <param name="item">the extension to check for</param>
		/// <returns>true if the extension is in the collection, else false</returns>
		public bool Contains(IrcShark.Extensions.ExtensionInfo item)
		{
			return extensions.Contains(item);
		}
		
		/// <summary>
		/// Copys all extensions in this collection to the given array
		/// </summary>
		/// <param name="array">the array to copy to</param>
		/// <param name="arrayIndex">the index to start copy to</param>
		public void CopyTo(ExtensionInfo[] array, int arrayIndex)
		{
			extensions.CopyTo(array, arrayIndex);
		}
		
		/// <summary>
		/// Returns an array of all ExtensionInfos in this collection
		/// </summary>
		/// <returns>the array</returns>
		public ExtensionInfo[] ToArray()
		{
			return extensions.ToArray();
		}
		
		/// <summary>
		/// Removes an extension form the collection
		/// </summary>
		/// <param name="item">the item to remove</param>
		/// <returns>true if the item was removed, else false</returns>
		public bool Remove(IrcShark.Extensions.ExtensionInfo item)
		{
			//return false;
			return extensions.Remove(item);
		}
		
		public IEnumerator<IrcShark.Extensions.ExtensionInfo> GetEnumerator()
		{
			return extensions.GetEnumerator();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return (extensions as IEnumerable).GetEnumerator();
		}
		
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Reads a collection of all ExtensionInfos
		/// </summary>
		/// <param name="reader">the reader to read from</param>
		public void ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
				return;
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
		/// Writes the collection to an XmlWriter
		/// </summary>
		/// <param name="writer">the writer to write to</param>
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
