using UnityEngine;
using System.Collections;

public abstract class SeesProject : MonoBehaviour {

	protected virtual void Awake(){
		SceneController.Initialize();
	}

	public abstract void DataLoaded();
}
