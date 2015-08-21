using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public void LoadScene(GameObject go){
		Application.LoadLevel(go.name);
	}
}
