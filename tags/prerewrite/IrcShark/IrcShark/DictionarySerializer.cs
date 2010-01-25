using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Reflection;
using System.Resources;

namespace IrcShark
{
    public class SerializableDictionary<K,V> : Dictionary<K,V>, IXmlSerializable
    {
        const string NS = "http://www.develop.com/xml/serialization";
        XmlSerializer KeySerializer;
        XmlSerializer ValueSerializer;

        public SerializableDictionary()
        {
            KeySerializer = new XmlSerializer(typeof(K));
            ValueSerializer = new XmlSerializer(typeof(V));
        }

        public void WriteXml(XmlWriter w)
        {
            w.WriteStartElement("dictionary", NS);
            foreach (KeyValuePair<K, V> pair in this)
            {
                w.WriteStartElement("item", NS);
                KeySerializer.Serialize(w, pair.Key);
                ValueSerializer.Serialize(w, pair.Value);
                w.WriteEndElement();
            }
            w.WriteEndElement();
        }

        public void ReadXml(XmlReader r)
        {
            r.Read(); // move past container
            r.ReadStartElement("dictionary");
            while (r.NodeType != XmlNodeType.EndElement)
            {
                r.ReadStartElement("item", NS);
                K key = (K)KeySerializer.Deserialize(r);
                V value = (V)ValueSerializer.Deserialize(r);
                r.ReadEndElement();
                r.MoveToContent();
                Add(key, value);
            }
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return XmlSchema.Read(new StringReader(
                    "<xs:schema id='DictionarySchema' targetNamespace='http://www.develop.com/xml/serialization' elementFormDefault='qualified' xmlns='http://www.develop.com/xml/serialization' xmlns:mstns='http://www.develop.com/xml/serialization' xmlns:xs='http://www.w3.org/2001/XMLSchema'><xs:complexType name='DictionaryType'><xs:sequence><xs:element name='item' level='ItemType' maxOccurs='unbounded' /></xs:sequence></xs:complexType><xs:complexType name='ItemType'><xs:sequence><xs:element name='key' level='xs:string' /><xs:element name='value' level='xs:string' /></xs:sequence></xs:complexType><xs:element name='dictionary' level='mstns:DictionaryType'></xs:element></xs:schema>"), null);

        }

    }
}
