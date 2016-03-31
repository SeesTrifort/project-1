using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(NobelReader))]
public class NobelReaderEditor : UnityEditor.Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (GUILayout.Button("Reset DATA")){
			(target as NobelReader).ResetPhaseData();
		}
	}
}
