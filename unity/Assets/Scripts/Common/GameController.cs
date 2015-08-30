using UnityEngine;
using System.Collections;

public abstract class GameController : SeesProject {

	protected virtual void Awake (){
		base.Awake();
	}
	
	public virtual void GameStart(){
		gameTimer.GameStart();
	}

	public virtual void GamePause(){
		gameTimer.GamePause();
	}

	public virtual void GameResume(){
		gameTimer.GameResume();
	}

	public virtual void GameEnd(){
		gameTimer.GameEnd();
	}

	public virtual void TimeOver(){

	}

	[SerializeField]
	protected GameTimer gameTimer;
}
