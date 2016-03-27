using UnityEngine;
using System.Collections;

public class AnimationEvent : MonoBehaviour {

	public void ResetParameter(string parameter){
		gameObject.GetComponent<Animator>().SetBool(parameter,false);
	}

	public void SendMessage(string message){
		GameObject.FindGameObjectWithTag("GameController").SendMessage(message, SendMessageOptions.DontRequireReceiver);
	}

	public void Inactive(){
		gameObject.SetActive(false);
	}

	public void DestorySelf(){
		GameObject.Destroy(gameObject);
	}
}
