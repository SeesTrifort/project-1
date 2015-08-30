using UnityEngine;
using System.Collections;

public abstract class GameTimer : MonoBehaviour {
	[SerializeField]
	protected GameController gameController;

	[SerializeField]
	protected UILabel timerLabel;

	[HideInInspector]
	public int timerFlag = 1;

	float miliTime = 0f;

	int timeLimit = 0;

	public void SetTimer(int _timeLimit){
		timeLimit = _timeLimit * 100;
		ShowTimer();
	}

	public void GameStart(){
		timerFlag = 0;
		miliTime = 0f;
	}
	
	public void GamePause(){
		timerFlag ++;
	}
	
	public void GameResume(){
		timerFlag --;
	}
	
	public void GameEnd(){
		timerFlag = 1;
	}

	public void ShowTimer(){
		if (timeLimit == 0){
			ShowTimer((int)(miliTime * 100f));
		}else{
			int time = timeLimit - (int)(miliTime * 100f);
			if (time < 0){
				ShowTimer(0);
				gameController.TimeOver();
			}else{
				ShowTimer(timeLimit - (int)(miliTime * 100f));
			}
		}
	}

	public abstract void ShowTimer(int miliSecond);

	void Update(){
		if (timerFlag != 0) return;

		miliTime += Time.deltaTime;
		ShowTimer();
	}
}
