using UnityEngine;
using System.Collections;

public class Game2Controller : GameController {

	[SerializeField] 
	public Game2UI gameUI;

	public Game2Icon[] puzzles;

	Game2Icon puzzleAnswer;

	[HideInInspector]
	public int width = 3;

	[HideInInspector]
	public int height = 3;

	int answerNum = 0;
	int correctCount = 0;

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

		SetTile();

		GameStart();
	}
	
	void SetTile(){

		if (puzzleAnswer != null) GameObject.Destroy(puzzleAnswer.gameObject);
		foreach(Game2Icon icon in puzzles){
			GameObject.Destroy(icon.gameObject);
		}

		int answer = Random.Range(0, Mathf.Min(gameUI.puzzleColors.Length, level+2));

		puzzleAnswer = gameUI.SetAnswer(answer);

		width = (level + 5) / 2 ;
		height = (level + 5) / 2 ;
		if (level % 2 == 0) height ++;

		int presentListId = 0;

		puzzles = new Game2Icon[width * height];
		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				puzzles[presentListId] = gameUI.MakePrefab(ref presentListId, level, answer);
			}
		}

		answerNum = Random.Range(1, width * height);

		int change = 0;
		while(change != answerNum){
			int random = Random.Range(0, width*height);
			if (puzzles[random].shapeNum != answer){
				puzzles[random].shapeNum = answer;
				change ++;
			}
		}
	}

	public override void GameStart () {

		base.GameStart ();
	}


	public void IconClick(Game2Icon icon){
		if (icon.shapeNum != puzzleAnswer.shapeNum) Wrong();
		else{
			correctCount ++;

			if (correctCount == answerNum) Correct();
		}
	}

	void Correct(){
		correctCount = 0;
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

		SetTile();

		gameTimer.timerFlag --;
	}
	
	void Wrong(){
		correctCount = 0;
		wrong ++;
		combo = 0;
		
		StartCoroutine(WrongAnimation());
	}
	
	IEnumerator WrongAnimation(){
		gameTimer.timerFlag ++;
		
		gameUI.messageLabel.text = "INCORRECT !";
		
		yield return new WaitForSeconds(0.5f);
		
		gameUI.messageLabel.text = "";

		SetTile();

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
