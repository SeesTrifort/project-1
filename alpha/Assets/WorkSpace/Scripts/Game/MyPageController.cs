using UnityEngine;
using System.Collections;

public class MyPageController : GameController {

	public override void ButtonClick (GameObject btn){
		switch(btn.name){
		case "btn_userinfo":
			PopupController.Instance.ShowPopup(PopupController.PopupName.UserInfo);
			break;
		case "btn_option":
			PopupController.Instance.ShowPopup(PopupController.PopupName.Option);
			break;
		case "btn_games":
			PopupController.Instance.ShowPopup(PopupController.PopupName.GameList);
			break;
		case "btn_leftright":
			PopupController.Instance.ClosePopups();
			SceneController.Instance.GotoScene(SceneController.SceneName.LeftRight);
			break;
		case "btn_close":
			PopupController.Instance.ClosePopups();
			break;

		}
	}
}
