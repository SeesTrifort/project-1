using UnityEngine;
using System.Collections;

public class Game1Controller : GameController {
	[SerializeField] 
	public Game1UI gameUI;

	public Game1Icon[] puzzles;

	[HideInInspector]
	public int presentShape = 0;

	[HideInInspector]
	public int presentlistId = 0;

	int level = 1;
	int correct = 0;
	int wrong = 0;
	int combo = 0;
	int maxcombo = 0;

	protected override void Awake (){
		base.Awake();
	}

	public override void DataLoaded (){

		gameTimer.SetTimer(Master.game1Data.timerLimit);

		GameSet();
	}

	void GameSet(){
		for (int i = 0; i < 20; i++) {
			int random1 = Random.Range(0, gameUI.puzzleColors.Length);
			int random2 = Random.Range(0, gameUI.puzzleColors.Length);
			
			if (random1 != random2){
				Color temp = gameUI.puzzleColors[random1];
				gameUI.puzzleColors[random1] = gameUI.puzzleColors[random2];
				gameUI.puzzleColors[random2] = temp;
			}
		}

		for (int i = 0; i < Master.game1Data.maxShape; i++) {
			gameUI.SetAnswer(ref presentShape);
		}
		
		puzzles = new Game1Icon[Master.game1Data.maxLine];
		for (int i = 0; i < Master.game1Data.maxLine; i++) {
			puzzles[i] = gameUI.MakePrefab(ref presentlistId, Master.game1Data.maxLine);
		}

		gameObject.AddComponent<DragHandler>().Initialize(this);

		GameStart();
	}

	public override void GameStart (){
		base.GameStart ();
	}

#if UNITY_EDITOR
	void Update(){
		if (gameTimer.timerFlag != 0) return;

		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			Left();
		}else if (Input.GetKeyDown(KeyCode.RightArrow)){
			Right();
		}
	}
#endif

	void Swipe(Vector3 move){
		if (gameTimer.timerFlag != 0) return;

		if (move.x < -5){
			Left ();
		}else if (move.x > 5){
			Right ();
		}
	}

	public void Left(){
		if (gameTimer.timerFlag != 0) return;
		
		if (puzzles[0].shapeNum % 2 == 0) Correct();
		else Wrong();
	}
	
	public void Right(){
		if (gameTimer.timerFlag != 0) return;

		if (puzzles[0].shapeNum % 2 == 1) Correct();
		else Wrong();
	}
	
	void Correct(){
		correct ++;
		combo ++;
		maxcombo = Mathf.Max(maxcombo, combo);
		StartCoroutine(CorrectAnimation());
	}

	IEnumerator CorrectAnimation(){
		gameTimer.timerFlag ++;
		
		if(level != correct/Master.game1Data.levelupSpan + 1){

			level = correct/Master.game1Data.levelupSpan + 1;
			gameUI.SetAnswer(ref presentShape);
			gameUI.messageLabel.text = "level up ! \n level : " + level;

			yield return new WaitForSeconds(0.5f);

			gameUI.messageLabel.text = "";
		}
		
		GameObject.Destroy(puzzles[0].gameObject);
		for (int i = 1; i < Master.game1Data.maxLine; i++) {
			puzzles[i-1] = puzzles[i];
		}
		puzzles[puzzles.Length-1] = gameUI.MakePrefab(ref presentlistId, Master.game1Data.maxLine);
		for (int i = 0; i < puzzles.Length; i++) {
			puzzles[i].lineId = i;
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

		gameUI.messageLabel.text = "Correct : " + correct + "\nWrong : " + wrong + "\nMaxCombo : " + maxcombo;
		
		gameUI.retryButton.SetActive(true);
	}

	public override void TimeOver (){
		GameEnd();
	}

	public void Retry(){
		Application.LoadLevel(Constants.Scene.MyPage);
	}
}
