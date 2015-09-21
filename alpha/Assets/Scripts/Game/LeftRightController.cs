using UnityEngine;
using System.Collections;

public class LeftRightController : GameController {
	public GameData data;

	public GameTimer timer;

	public override void Init(){
		data = GameMaster.GetData(GameIds.LeftRight);

		PopupController.Instance.ShowPopup(PopupController.PopupName.GameStart);
		timer.SetTimer(data.timeLimit);
	}

	public override void TimeOver (){
		timer.GameEnd();
		PopupController.Instance.ShowPopup(PopupController.PopupName.GameEnd);
	}

	public override IEnumerator SceneEndAnimation (string nextScene){
		yield return new WaitForSeconds(0f);
	}

	public override void ButtonClick (GameObject btn){
		switch(btn.name){
		case "btn_start" :
			PopupController.Instance.ClosePopups();
			timer.GameStart();
			break;
		case "btn_mypage":
			PopupController.Instance.ClosePopups();
			SceneController.Instance.GotoScene(SceneController.SceneName.MyPage);
			break;
		}
	}
}
