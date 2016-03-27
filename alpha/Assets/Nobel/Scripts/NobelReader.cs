using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NobelReader : MonoBehaviour {

	[SerializeField]
	Text nameArea;

	[SerializeField]
	Text textArea;

	Phase phase;

	public void Start() {
		ReadPhase(1);
	}

	public void ReadPhase(int phaseNum) {
		phase = XmlDeserializer.DeserializeXml<Phase>(((TextAsset)Resources.Load("Nobel" + phaseNum, typeof(TextAsset))).text);
		Debug.Log(phase.charSerif[0].Name);
	}
}
