using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	
	public enum SceneName{
		Load,
		MyPage,
		LeftRight
	}



	/// <summary>
	/// 全てのシーンにはSceneControllerがおいてあって最初に始まったシーンでInstanceを生成. 生成と同時にデータのロードも.
	/// (初めてのシーンの場合)データロード後、(遷移があった場合)シーンの遷移後、各シーンの GameController を Init を呼び、シーンが始まる.
	/// </summary> 

	static SceneController _Instance;
	public static SceneController Instance{
		get{
			return _Instance;
		}
	}
	
	IEnumerator Start(){
		if (_Instance == null){
			_Instance = this;
			DontDestroyOnLoad(gameObject);

			yield return StartCoroutine (InitData());

			Instance.sceneHistory[sceneHistory.Length - 1] = Application.loadedLevelName;
			InitGameController();

		}else{
			GameObject.Destroy(gameObject);
		}
	}

	IEnumerator OnLevelWasLoaded(){
		if (Application.loadedLevelName == SceneName.Load.ToString()){
			Application.LoadLevel(nextSceneName);
		}else {
			yield return new WaitForEndOfFrame();
			InitGameController();
		}
	}

	IEnumerator InitData(){
		// TODO : SetAllData.

		MasterData.SetData();

		yield return new WaitForSeconds(0f);
	}

	void InitGameController(){
		Instance.gameController = GameObject.FindObjectOfType<GameController>();
		gameController.Init();
	}



	/// <summary>
	/// シーンの遷移があった場合はsceneHistoryにシーンの名前の保存しておいて、前のシーンに遷移できるようにする. 保存は現状５つまで.
	/// シーンの遷移が発生すると各シーンのGameController の SceneEndAnimation を呼び、アニメーションが終わった後遷移.
	/// </summary> 

	string[] sceneHistory = new string[5];
	[HideInInspector] public GameController gameController;
	string nextSceneName = "";

	public void GotoScene(SceneName sceneName){
	
		if (sceneName.ToString() != Application.loadedLevelName){
			for (int i = 0; i < sceneHistory.Length -1 ; i++) {
				Instance.sceneHistory[i] = Instance.sceneHistory[i+1];
			}
			Instance.sceneHistory[sceneHistory.Length - 1] = sceneName.ToString();
		}

		StartCoroutine (Instance.SceneChangeAnimation(sceneName.ToString()));
	}

	public void GotoBackScene(){
		if ( Instance.sceneHistory[sceneHistory.Length -2] == ""){
			GotoScene(SceneName.MyPage);
		}else{
			for (int i = sceneHistory.Length -1 ; i > 0 ; i--) {
				Instance.sceneHistory[i] = Instance.sceneHistory[i-1];
			}
			Instance.sceneHistory[0] = "";

			StartCoroutine (Instance.SceneChangeAnimation(Instance.sceneHistory[sceneHistory.Length -2]));
		}
	}

	IEnumerator SceneChangeAnimation(string sceneName){
		
		yield return StartCoroutine(gameController.SceneEndAnimation(sceneName));

		nextSceneName = sceneName;
		Application.LoadLevel(SceneName.Load.ToString());
	}
}
