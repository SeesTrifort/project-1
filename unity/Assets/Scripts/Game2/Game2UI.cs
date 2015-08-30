using UnityEngine;
using System.Collections;

public class Game2UI : GameUI {
	public Transform puzzleParent;
	
	public GameObject puzzlePrefab;

	public Color[] puzzleColors;

	public Transform answer;

	public UILabel messageLabel;
	
	public GameObject retryButton;

	public Game2Icon SetAnswer(int presentShape){
		GameObject puzzleObject = ((GameObject)GameObject.Instantiate(puzzlePrefab));
		puzzleObject.transform.SetParent(answer);
		transform.localScale = Vector3.one;
		Game2Icon puzzle = puzzleObject.GetComponent<Game2Icon>();
		puzzle.shapeNum = presentShape;
		puzzle.listId = -1;
		return puzzle;
	}

	public Game2Icon MakePrefab(ref int presentlistId, int level, int answer){
		GameObject puzzleObject = ((GameObject)GameObject.Instantiate(puzzlePrefab));
		puzzleObject.transform.SetParent(puzzleParent);
		transform.localScale = Vector3.one;
		Game2Icon puzzle = puzzleObject.GetComponent<Game2Icon>();
		int shape = Random.Range(0, Mathf.Min(puzzleColors.Length, level+2));
		puzzle.shapeNum = shape == answer ? shape + 1 : shape;
		puzzle.listId = presentlistId;
		presentlistId ++ ;
		return puzzle;
	}
}
