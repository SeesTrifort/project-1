using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Nobel")]
public class Phase {

	public class Setting {
		[XmlAttribute]
		public string Background;

		[XmlAttribute]
		public string Animator;
	}

	public class CharSerif {
		[XmlAttribute]
		public string Name;

		[XmlAttribute]
		public string Image;

		[XmlAttribute]
		public int ImageType;

		[XmlAttribute]
		public bool ImageReset;

		[XmlAttribute]
		public string Serif;

		[XmlAttribute]
		public string BGM;

		[XmlAttribute]
		public string SE;
	}

	public class Choice {
		[XmlAttribute]
		public int Type;

		[XmlAttribute]
		public int NextPhaseNum;

		[XmlElement()]
		public List<Question> question;
	}

	public class Question {
		[XmlAttribute]
		public string Text;

		[XmlAttribute]
		public int NextPhaseNum;
	}


	[XmlElement()]
	public Setting setting;

	[XmlElement()]
	public List<CharSerif> charSerif;

	[XmlElement()]
	public Choice choice;

}



