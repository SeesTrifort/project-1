using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour {

	// Settings //
	public Transform puzzleParent;
	public GameObject puzzlePrefab;
	public Sprite[] puzzleSprites;
	public Color[] puzzleColors;
	public Transform answerLeft;
	public Transform answerRight;
	public Puzzle[] puzzles;
	public Text timerLabel;
	public Text messageLabel;
	public GameObject btnRetry;

	// Game Balance // 
	public float timeLimit = 30f;
	public int maxLine = 10;
	public int levelupSpan = 30;
	public int level = 1;
	public int maxShape = 2;
	public int presentShape = 0;

	// Game Scores //
	public int presentlistId = 0;
	public int correct = 0;
	public int wrong = 0;
	public int combo = 0;
	public int maxcombo = 0;

	void Start(){
		ShowTimer();
		GameStart();
	}

	bool timerFlag = false;
	void Update(){
		if (!timerFlag || gameEndFlag) return;

		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			Left();
		}else if (Input.GetKeyDown(KeyCode.RightArrow)){
			Right();
		}

		timeLimit -= Time.deltaTime;
		if (timeLimit < 0) GameEnd();
		else ShowTimer();

	}

	void ShowTimer(){
		int miliTime = (int)(timeLimit * 100f);
		timerLabel.text = (miliTime/100).ToString("D2") + ":" + (miliTime%100).ToString("D2");
	}

	public void GameStart(){

		RandomSprite();
		RandomColor();

		for (int i = 0; i < maxShape; i++) {
			SetAnswer();
		}

		puzzles = new Puzzle[maxLine];
		for (int i = 0; i < maxLine; i++) {
			puzzles[i] = MakePrefab();
		}

		timerFlag = true;
	}

	void RandomSprite(){
		for (int i = 0; i < 20; i++) {
			int random1 = Random.Range(0, puzzleSprites.Length);
			int random2 = Random.Range(0, puzzleSprites.Length);
			
			if (random1 != random2){
				Sprite temp = puzzleSprites[random1];
				puzzleSprites[random1] = puzzleSprites[random2];
				puzzleSprites[random2] = temp;
			}
		}
	}

	void RandomColor(){
		for (int i = 0; i < 20; i++) {
			int random1 = Random.Range(0, puzzleColors.Length);
			int random2 = Random.Range(0, puzzleColors.Length);
			
			if (random1 != random2){
				Color temp = puzzleColors[random1];
				puzzleColors[random1] = puzzleColors[random2];
				puzzleColors[random2] = temp;
			}
		}
	}

	public void SetAnswer(){
		GameObject puzzleObject = ((GameObject)GameObject.Instantiate(puzzlePrefab));
		puzzleObject.transform.SetParent(presentShape%2 == 0 ? answerLeft : answerRight);
		Puzzle puzzle = puzzleObject.GetComponent<Puzzle>();
		puzzle.shapeNum = presentShape;
		puzzle.lineId = (presentShape/2) * -1;
		presentShape ++;
	}

	public Puzzle MakePrefab(){
		GameObject puzzleObject = ((GameObject)GameObject.Instantiate(puzzlePrefab));
		puzzleObject.transform.SetParent(puzzleParent);
		Puzzle puzzle = puzzleObject.GetComponent<Puzzle>();
		puzzle.lineId = Mathf.Min(presentlistId, maxLine);
		presentlistId ++ ;
		return puzzle;
	}

	public void Left(){
		if (!timerFlag || gameEndFlag) return;

		if (puzzles[0].shapeNum % 2 == 0) Correct();
		else Wrong();
	}

	public void Right(){
		if (!timerFlag || gameEndFlag) return;

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
		timerFlag = false;

		if(level != correct/levelupSpan  + 1){
			level = correct/levelupSpan + 1;
			SetAnswer();

			messageLabel.text = "level up ! \n level : " + level;
			yield return new WaitForSeconds(0.5f);
			messageLabel.text = "";
		}
		
		GameObject.Destroy(puzzles[0].gameObject);
		for (int i = 1; i < maxLine; i++) {
			puzzles[i-1] = puzzles[i];
		}
		puzzles[puzzles.Length-1] = MakePrefab();
		for (int i = 0; i < puzzles.Length; i++) {
			puzzles[i].lineId = i;
		}

		timerFlag = true;
	}

	void Wrong(){
		wrong ++;
		combo = 0;

		StartCoroutine(WrongAnimation());
	}

	IEnumerator WrongAnimation(){
		timerFlag = false;
		messageLabel.text = "INCORRECT !";
		yield return new WaitForSeconds(0.5f);
		messageLabel.text = "";
		timerFlag = true;
	}

	bool gameEndFlag = false;
	public void GameEnd(){
		timerFlag = false;
		gameEndFlag = true;

		messageLabel.text = "Correct : " + correct + "\n Wrong : " + wrong + "\n MaxCombo : " + maxcombo;

		btnRetry.SetActive(true);
	}

	public void Retry(){
		Application.LoadLevel("Menu");
	}
}
