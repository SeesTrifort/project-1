using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PopupController : MonoBehaviour {
	public enum PopupName{
		UserInfo,
		Option,
		GameList,
		GameStart,
		GamePause,
		GameEnd,
	}



	/// <summary>
	/// 全てのシーンにはSceneControllerがおいてあって同じGameObjectにPopupControllerが付いている.
	/// </summary> 

	static PopupController _Instance;
	public static PopupController Instance{
		get{
			return _Instance;
		}
	}
	
	void Awake(){
		if (_Instance == null){
			_Instance = this;
			DontDestroyOnLoad(gameObject);
		}else{
			GameObject.Destroy(gameObject);
		}
	}



	/// <summary>
	/// ShowPopupが呼ばれたら現在のものを隠して、新しいポップアップを表示.
	/// BeforePopupが呼ばれたら現在のものを破棄し、前のポップアップを表示.
	/// ClosePopupが呼ばれたら全てのポップアップを破棄.
	/// </summary>
	 
	List<Popup> popups = new List<Popup>();
	Popup newPopup = null;
	public Popup ShowPopup(PopupName popupName){

		foreach(Popup popup in popups){
			popup.Hide();
		}

		newPopup = (GameObject.Instantiate(Resources.Load("Prefabs/Popup/"+popupName)) as GameObject).GetComponent<Popup>();
		newPopup.transform.SetParent(transform);
		newPopup.name = popupName.ToString();
		newPopup.stackNum = popups.Count;
		newPopup.Show();
		popups.Add(newPopup);

		return newPopup;
	}

	public void ClosePopups(){
		
		foreach(Popup popup in popups){
			popup.Close();
		}
		popups.Clear();
	}

	public void BeforePopup(){
		Popup before = null ;
		foreach(Popup popup in popups){
			if (popup.stackNum == newPopup.stackNum -1){
				before = popup;
			}
		}
		
		newPopup.Close();
		popups.Remove(newPopup);

		if (before != null) before.Show();
		newPopup = before;
	}
}
