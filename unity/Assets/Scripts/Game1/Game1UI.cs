using UnityEngine;
using System.Collections;

public class Game1UI : GameUI {

	public Transform puzzleParent;

	public GameObject puzzlePrefab;

	public Color[] puzzleColors;

	public Transform answerLeft;

	public Transform answerRight;

	public UILabel messageLabel;

	public GameObject btnRetry;

	public void SetAnswer(ref int presentShape){
		GameObject puzzleObject = ((GameObject)GameObject.Instantiate(puzzlePrefab));
		puzzleObject.transform.SetParent(presentShape%2 == 0 ? answerLeft : answerRight);
		Puzzle puzzle = puzzleObject.GetComponent<Puzzle>();
		puzzle.shapeNum = presentShape;
		puzzle.lineId = (presentShape/2) * -1;
		presentShape ++;
	}
	
	public Puzzle MakePrefab(ref int presentlistId, int maxLine){
		GameObject puzzleObject = ((GameObject)GameObject.Instantiate(puzzlePrefab));
		puzzleObject.transform.SetParent(puzzleParent);
		Puzzle puzzle = puzzleObject.GetComponent<Puzzle>();
		puzzle.lineId = Mathf.Min(presentlistId, maxLine);
		presentlistId ++ ;
		return puzzle;
	}
}
