// <copyright file="ParameterCollection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ParameterCollection class.</summary>

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
namespace IrcShark.Extensions.Chatting
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;
    
    /// <summary>
    /// The ParameterCollection is used to save named string parameters of an object.
    /// </summary>
    public class ParameterCollection : IDictionary<string, string>, IXmlSerializable
    {
        /// <summary>
        /// Saves all the parameters.
        /// </summary>
        private Dictionary<string, string> data;
        
        /// <summary>
        /// Initializes a new instance of the ParameterCollection class.
        /// </summary>
        public ParameterCollection()
        {
            data = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// Gets a list of the names of all parameters.
        /// </summary>
        /// <value>A collection of parameter names.</value>
        public ICollection<string> Keys 
        {
            get 
            {
                return data.Keys;
            }
        }
        
        /// <summary>
        /// Gets a list of all parameter values.
        /// </summary>
        /// <value>
        /// The list of parameter values.
        /// </value>
        public ICollection<string> Values 
        {
            get 
            {
                return data.Values;
            }
        }
        
        /// <summary>
        /// Gets the number of parameters in this collection.
        /// </summary>
        /// <value>
        /// The number of parameters.
        /// </value>
        public int Count 
        {
            get 
            {
                return data.Count;
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether this collection is read only.
        /// </summary>
        /// <value>
        /// This is always false.
        /// </value>
        bool ICollection<KeyValuePair<string, string>>.IsReadOnly 
        {
            get 
            {
                return false;
            }
        }
        
        /// <summary>
        /// Gets the parameter alue of the parameter with the given name.
        /// </summary>
        /// <value>
        /// The parameter value.
        /// </value>
        /// <param name="key">The name of the parameter.</param>
        public string this[string key] 
        {
            get 
            {
                return data[key];
            }
            
            set 
            {
                data[key] = value;
            }
        }
        
        /// <summary>
        /// Checks if the given parameter exists.
        /// </summary>
        /// <param name="key">The name of the parameter to check.</param>
        /// <returns>Its true if the parameter exists, false otherwise.</returns>
        public bool ContainsKey(string key)
        {
            return data.ContainsKey(key);
        }
        
        /// <summary>
        /// Adds a new parameter.
        /// </summary>
        /// <param name="key">The name of the parameter.</param>
        /// <param name="value">The parameter value.</param>
        public void Add(string key, string value)
        {
            data.Add(key, value);
        }
        
        /// <summary>
        /// Removes the given parameter from the collection.
        /// </summary>
        /// <param name="key">The name of the parameter to remove.</param>
        /// <returns>Its true if the parameter was removed, false otherwise.</returns>
        public bool Remove(string key)
        {
            return data.Remove(key);
        }
        
        /// <summary>
        /// Tries to get the value of the given parameter.
        /// </summary>
        /// <param name="key">The parameter to get.</param>
        /// <param name="value">A reference where the value is saved.</param>
        /// <returns>Its true if the value was received correctly, false otherwise.</returns>
        public bool TryGetValue(string key, out string value)
        {
            return data.TryGetValue(key, out value);
        }
        
        /// <summary>
        /// Adds a pait of parameter name and value.
        /// </summary>
        /// <param name="item">The pair of name and value.</param>
        void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
        {
            data.Add(item.Key, item.Value);
        }
        
        /// <summary>
        /// Removes all parameters from the collection.
        /// </summary>
        void ICollection<KeyValuePair<string, string>>.Clear()
        {
            data.Clear();
        }
        
        /// <summary>
        /// Checks if the collection contains the given parmater with the given value.
        /// </summary>
        /// <param name="item">The pair of parameter name and value.</param>
        /// <returns>Its true if the collection contains the pair, false otherwise.</returns>
        bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)data).Contains(item);
        }
        
        /// <summary>
        /// Copys all name value pairs in the parameter collection to an array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The index where the name value pairs should start at.</param>
        void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, string>>)data).CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// Removes a name value pair from the parameter collection.
        /// </summary>
        /// <param name="item">The name value pair.</param>
        /// <returns>Its true if the pair was removed, false otherwise.</returns>
        bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
        {
            return ((ICollection<KeyValuePair<string, string>>)data).Remove(item);
        }
        
        /// <summary>
        /// Gets a generic enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, string>>)data).GetEnumerator();
        }
        
        /// <summary>
        /// Gets an enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
        
        /// <summary>
        /// Gets the schema definition of the ParameterCollection class.
        /// </summary>
        /// <returns>The XmlSchema instance.</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            System.Xml.Schema.XmlSchema result = System.Xml.Schema.XmlSchema.Read(new System.IO.MemoryStream(Properties.Resources.ParametersSchema), null);
            return result;
        }
        
        /// <summary>
        /// Reads the data of the parameter collection from an XmlReader.
        /// </summary>
        /// <param name="reader">The reader to read from.</param>
        public void ReadXml(XmlReader reader)
        {
            string name;
            string value;
            if (reader.IsEmptyElement)
            { 
                reader.Read();
                return;
            }
            
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals("param"))
                        {
                            name = reader.GetAttribute("name");
                            value = reader.ReadElementContentAsString();
                            data.Add(name, value);
                        }
                        else
                        {
                            reader.Skip();
                        }
                        
                        break;
                    case XmlNodeType.EndElement:
                        //reader.Read();
                        return;
                }
            }
        }
        
        /// <summary>
        /// Writes the data of the parameter collection to an XmlWriter.
        /// </summary>
        /// <param name="writer">The XmlWriter to write to.</param>
        public void WriteXml(XmlWriter writer)
        {
            foreach (KeyValuePair<string, string> item in data)
            {
                writer.WriteStartElement("param", "http://www.ircshark.net/2010/parameters");
                writer.WriteAttributeString("name", "http://www.ircshark.net/2010/parameters", item.Key);
                writer.WriteString(item.Value);
                writer.WriteEndElement();
            }
        }
    }
}
