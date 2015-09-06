using UnityEngine;
using System.Collections;

public class Game3Timer : GameTimer {

	public override void ShowTimer(int miliSecond){
		timerLabel.text = (miliSecond/100).ToString("D2") + ":" + (miliSecond%100).ToString("D2");
	}
}
