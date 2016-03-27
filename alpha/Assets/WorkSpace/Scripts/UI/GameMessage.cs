using UnityEngine;
using System.Collections;

public class GameMessage : MonoBehaviour {

	public void SetMessage(string message, Color color, float delay = 0f){
		UILabel label = GetComponent<UILabel>();
		if (label != null){
			label.text = message;
			label.color = color;
		}
		if (delay != 0f) Delay(delay);
	}

	public void Delay (float time){
		UITweener[] tweens = gameObject.GetComponents<UITweener>();
		foreach(UITweener tween in tweens){
			tween.delay = time;
		}
	}
}
