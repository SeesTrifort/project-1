using UnityEngine;
using System;

public class GameTimer : MonoBehaviour{
	
	float updateSpan = 0.13f;

	float timerSpeed = 1f;

	[SerializeField]
	UILabel timerLabel;
	
	int timeLimit = 0;

	int timerFlag = 1;
	
	float miliTime = 0f;
	
	float updateTime = 0f;

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

	void Update(){
		if (timerFlag != 0) return;
		
		updateTime += Time.deltaTime * timerSpeed;

		if (updateTime > updateSpan){
			miliTime += updateSpan;
			updateTime -= updateSpan;

			ShowTimer();
		}
	}
	
	public void ShowTimer(){
		if (timeLimit == 0){
			ShowTimer((int)(miliTime * 100f));
		}else{
			int time = timeLimit - (int)(miliTime * 100f);
			if (time < 0){
				ShowTimer(0);
				TimeOver();
			}else{
				ShowTimer(timeLimit - (int)(miliTime * 100f));
			}
		}
	}
	
	public void ShowTimer(int miliSecond){
		if (timerLabel != null) timerLabel.text = (miliSecond/100).ToString("D2") + ":" + (miliSecond%100).ToString("D2");
	}

	void TimeOver(){
		SceneController.Instance.gameController.TimeOver();
	}
}