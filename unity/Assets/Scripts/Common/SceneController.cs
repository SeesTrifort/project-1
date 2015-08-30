using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	public static SeesProject mainController;

	static SceneController _Instance;
	public static SceneController Instance{
		get{
			if (_Instance == null){
				_Instance = new GameObject().AddComponent<SceneController>();
				_Instance.name = "SceneController";
			}
			return _Instance;
		}
	}

	public static void Initialize(){
		Instance.DataLoad();
	}

	void DataLoad(){
		StartCoroutine(StartDataLoad());
	}

	IEnumerator StartDataLoad(){
	
		Master.Initialize();

		yield return new WaitForEndOfFrame();

		mainController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SeesProject>();
	
		mainController.DataLoaded();
	}
}
