using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public virtual void Init(){}

	public virtual void TimeOver(){}

	public virtual IEnumerator SceneEndAnimation(string nextScene){
		Debug.Log("Goto " + nextScene);
		yield return new WaitForSeconds(0f);
	}

	public virtual void ButtonClick(GameObject btn){
		Debug.Log("ButtonClick " + btn.name);
	}
}