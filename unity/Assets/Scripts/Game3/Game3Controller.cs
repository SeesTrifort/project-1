using UnityEngine;
using System.Collections;

public class Game3Controller : GameController {

	[SerializeField] 
	public Game3UI gameUI;

	public Game3Icon[] puzzles;

	int level = 1;
	int correct = 0;
	int wrong = 0;
	int combo = 0;
	int maxcombo = 0;
	
	protected override void Awake (){
		base.Awake();
	}
	
	public override void DataLoaded (){
		gameTimer.SetTimer(Master.game2Data.timerLimit);

		GameSet();
	}

	void GameSet(){

		GameStart();
	}

	public override void GameStart () {

		base.GameStart ();
	}
	
	void Correct(){
		correct ++;
		combo ++;
		maxcombo = Mathf.Max(maxcombo, combo);
		StartCoroutine(CorrectAnimation());
	}
	
	IEnumerator CorrectAnimation(){
		gameTimer.timerFlag ++;
		
		if(level != correct/Master.game2Data.levelupSpan + 1){
			
			level = correct/Master.game2Data.levelupSpan + 1;

			gameUI.messageLabel.text = "level up ! \n level : " + level;
			
			yield return new WaitForSeconds(0.5f);
			
			gameUI.messageLabel.text = "";
		}
	
		gameTimer.timerFlag --;
	}
	
	void Wrong(){
		wrong ++;
		combo = 0;
		
		StartCoroutine(WrongAnimation());
	}
	
	IEnumerator WrongAnimation(){
		gameTimer.timerFlag ++;
		
		gameUI.messageLabel.text = "INCORRECT !";
		
		yield return new WaitForSeconds(0.5f);
		
		gameUI.messageLabel.text = "";

		gameTimer.timerFlag --;
	}

	public override void GameEnd (){
		base.GameEnd ();
		
		gameUI.messageLabel.text = "GameOver";
		
		gameUI.retryButton.SetActive(true);
	}
	
	public override void TimeOver (){
		GameEnd();
	}

	public void Retry(){
		Application.LoadLevel(Constants.Scene.MyPage);
	}
}
