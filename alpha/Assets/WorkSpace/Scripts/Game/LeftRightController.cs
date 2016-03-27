using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LeftRightController : GameController {

	[SerializeField] Transform gamePivot;
	[SerializeField] Transform leftPivot;
	[SerializeField] Transform rightPivot;
	[SerializeField] GameObject characterPrefab;
	
	[SerializeField] Transform messagePivot;
	[SerializeField] GameObject messagePrefab;

	List<Character> charactersAnswer = new List<Character>();
	List<Character> characters = new List<Character>();
	

	[SerializeField] UILabel scoreLabel;


	public override void Init(){
		data = GameMaster.GetData(GameIds.LeftRight);

		PopupController.Instance.ShowPopup(PopupController.PopupName.GameStart);
		timer.SetTimer(data.timeLimit);
	}
	
	public override void ButtonClick (GameObject btn){
		switch(btn.name){
		case "btn_start" :
			PopupController.Instance.ClosePopups();
			GameStart();
			break;
		case "btn_mypage":
			PopupController.Instance.ClosePopups();
			SceneController.Instance.GotoScene(SceneController.SceneName.MyPage);
			break;
		case "btn_left":
			Left();
			break;
		case "btn_right":
			Right();
			break;
		case "btn_pause":
			Pause();
			break;
		case "btn_resume":
			Resume();
			break;
		}
	}

	void GameStart(){
		
		gameObject.AddComponent<DragHandler>().Initialize(this);

		CharacterCommon.ShuffleColor();

		SetAnswer();
		SetAnswer();

		for (int i = 0; i < 8; i++) {
			SetCharacter();

		}

		timer.GameStart();
		
	}

	void SetAnswer(){

		Character answer = UIUtils.MakePrefab(characterPrefab, charactersAnswer.Count % 2 == 0 ? leftPivot : rightPivot).GetComponent<Character>();
		answer.color = charactersAnswer.Count;
	
		answer.GetComponent<UIWidget>().depth = answer.transform.parent.GetComponent<UIWidget>().depth;
		answer.transform.localPosition = new Vector3(0, -75 -100 * (charactersAnswer.Count/2), 0);

		charactersAnswer.Add(answer);

		rightPivot.transform.parent.GetComponent<UISprite>().bottomAnchor.absolute = -150 - (100 * (charactersAnswer.Count/2));
		leftPivot.transform.parent.GetComponent<UISprite>().bottomAnchor.absolute = -150 - (100 * ((charactersAnswer.Count+1)/2));
	}

	void SetCharacter(){
		Character character = UIUtils.MakePrefab(characterPrefab, gamePivot).GetComponent<Character>();
		character.color = Random.Range(0, charactersAnswer.Count);

		character.SetLeftRightListId(characters.Count);

		characters.Add(character);
	}

#if UNITY_EDITOR
	void Update(){
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			Left();
		}else if (Input.GetKeyDown(KeyCode.RightArrow)){
			Right();
		}
	}
#endif

	public override void Swipe (Vector3 swipeDistance)
	{
		if (swipeDistance.x < -5) Left ();
		else if (swipeDistance.x > 5) Right();
	}

	void Left(){
		if (timer.timerFlag != 0) return;

		Character character0 = characters.FirstOrDefault(character => character.id == 0);

		if (character0.color % 2 == 0){
			StartCoroutine(Correct(character0));
		}else{
			StartCoroutine(Wrong());
		}
	}

	void Right(){
		if (timer.timerFlag != 0) return;

		Character character0 = characters.FirstOrDefault(character => character.id == 0);
		
		if (character0.color % 2 == 1){
			StartCoroutine(Correct(character0));
		}else{
			StartCoroutine(Wrong());
		}
	}


	IEnumerator Correct(Character character0){
		correct ++;
		combo ++;
		maxcombo = Mathf.Max(maxcombo, combo);

		score += (correct * 10 + maxcombo * 15 + combo * 25);
		scoreLabel.text = score.ToString();

		characters.Remove(character0);
		character0.LeftRightCorrectAnimation();

		int level = correct / 30 ;
		if (level != charactersAnswer.Count -2){
			timer.timerFlag ++;
			MakeMessage("level up !", 1f);
			SetAnswer();
			yield return new WaitForSeconds(1f);
			MakeMessage("time +2s !", 0.5f);
			timer.PlusTime(2f);
			yield return new WaitForSeconds(0.5f);
			timer.timerFlag --;
		}else{
			MakeMessage(combo + " Combo !");
		}

		foreach(Character character in characters){
			character.SetLeftRightListId (character.id -1);
		}

		SetCharacter();
	}

	IEnumerator Wrong(){
		wrong ++;
		combo = 0;

		MakeMessage(" Wrong !", Color.red);
		yield return new WaitForSeconds(0f);
	}

	void MakeMessage(string message, float delay = 0f){
		MakeMessage(message, Color.black, delay);
	}
	void MakeMessage(string message, Color color, float delay = 0f){
		GameObject msg = UIUtils.MakePrefab(messagePrefab, messagePivot);
		msg.GetComponent<GameMessage>().SetMessage(message, color, delay);
	}

	// 一時停止関連処理
	// ポーズボタンを押した時、アプリから離れた時呼ばれる.
	void OnApplicationPause(bool pause){
		if (!pause) Pause();
		else StopAllCoroutines();
	}

	void Pause(){
		if (timer.timerFlag != 0) return;

		timer.GamePause();

		PopupController.Instance.ShowPopup(PopupController.PopupName.GamePause);
	}

	void Resume(){
		timer.GameResume();

		PopupController.Instance.ClosePopups();
	}


	// 時間終了
	public override void TimeOver (){

		Destroy(GetComponent<DragHandler>());

		timer.GameEnd();
	
		PopupController.Instance.ShowPopup(PopupController.PopupName.GameEnd);
	}


	public override IEnumerator SceneEndAnimation (string nextScene){
		yield return new WaitForSeconds(0f);
	}
}
