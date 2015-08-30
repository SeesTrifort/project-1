﻿using UnityEngine;
using System.Collections;

public class Game1UI : GameUI {

	public Transform puzzleParent;

	public GameObject puzzlePrefab;

	public Color[] puzzleColors;

	public Transform answerLeft;

	public Transform answerRight;

	public UILabel messageLabel;

	public GameObject retryButton;

	public void SetAnswer(ref int presentShape){
		GameObject puzzleObject = ((GameObject)GameObject.Instantiate(puzzlePrefab));
		puzzleObject.transform.SetParent(presentShape%2 == 0 ? answerLeft : answerRight);
		transform.localScale = Vector3.one;
		Game1Icon puzzle = puzzleObject.GetComponent<Game1Icon>();
		puzzle.shapeNum = presentShape;
		puzzle.lineId = (presentShape/2) * -1;
		presentShape ++;
	}
	
	public Game1Icon MakePrefab(ref int presentlistId, int maxLine){
		GameObject puzzleObject = ((GameObject)GameObject.Instantiate(puzzlePrefab));
		puzzleObject.transform.SetParent(puzzleParent);
		transform.localScale = Vector3.one;
		Game1Icon puzzle = puzzleObject.GetComponent<Game1Icon>();
		puzzle.lineId = Mathf.Min(presentlistId, maxLine);
		presentlistId ++ ;
		return puzzle;
	}
}
