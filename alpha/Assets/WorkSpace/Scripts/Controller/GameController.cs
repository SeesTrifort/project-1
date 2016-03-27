using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameData data;

	public GameTimer timer;

	[HideInInspector] public int correct = 0;
	[HideInInspector] public int wrong = 0;
	[HideInInspector] public int combo = 0;
	[HideInInspector] public int maxcombo = 0;
	[HideInInspector] public int score = 0;

	public virtual void Init(){}

	public virtual void TimeOver(){}

	public virtual IEnumerator SceneEndAnimation(string nextScene){
		Debug.Log("Goto " + nextScene);
		yield return new WaitForSeconds(0f);
	}

	public virtual void ButtonClick(GameObject btn){
		Debug.Log("ButtonClick " + btn.name);
	}

	public virtual void Swipe(Vector3 swipeDistance){
		Debug.Log("Swipe " + swipeDistance);
	}
}