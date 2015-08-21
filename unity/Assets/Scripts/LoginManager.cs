using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;

public class LoginManager : MonoBehaviour {

	public void ButtonClick (GameObject go){
		Debug.Log(go.name);
		switch(go.name){
		case "icon_facebook":
			FB.Init(SetInit, OnHideUnity);
			break;
		case "icon_gpgs":
			break;
		case "icon_gamecenter":
			break;
		}
	}

	void SetInit(){
		if (FB.IsLoggedIn == true){
			LoginCallBack();
		}else{
			FB.Login("", LoginCallBack);
		}
	}

	void LoginCallBack(){
		FB.API("/me",Facebook.HttpMethod.GET,GetUserData);
	}

	void LoginCallBack(FBResult fbresult){
		if (fbresult.Error == null){
			FB.API("/me",Facebook.HttpMethod.GET,GetUserData);
		}else{
			Debug.Log("Facebook Login ERROR");
		}
	}

	void GetUserData(FBResult fbresult){
		if (fbresult.Error == null){
			Debug.Log(fbresult.Text);
		}else{
			Debug.Log("Get User Data ERROR");
		}
	}

	void OnHideUnity(bool isGameShown) {
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}	
	}
}
