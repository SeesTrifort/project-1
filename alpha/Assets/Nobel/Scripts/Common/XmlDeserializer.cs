using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XmlDeserializer
{
	public static T DeserializeXml<T> (string _xml_string) where T : class 
	{
		StringReader stringReader = new StringReader(_xml_string);			
		XmlSerializer xmlSerializer = new XmlSerializer (typeof(T));
		return (T)xmlSerializer.Deserialize(stringReader);
	}
}