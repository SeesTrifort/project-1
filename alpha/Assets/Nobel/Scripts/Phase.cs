using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Nobel")]
public class Phase {

	public class Setting {
		[XmlAttribute]
		public string Background; 
	}

	public class CharSerif {
		[XmlAttribute]
		public string Name;

		[XmlAttribute]
		public string Image;
	}


	[XmlElement()]
	public Setting setting;

	[XmlElement()]
	public List<CharSerif> charSerif;

}

public class XmlDeserializer
{
	public static T DeserializeXml<T> (string _xml_string) where T : class 
	{
		StringReader stringReader = new StringReader(_xml_string);			
		XmlSerializer xmlSerializer = new XmlSerializer (typeof(T));
		return (T)xmlSerializer.Deserialize(stringReader);
	}
}

