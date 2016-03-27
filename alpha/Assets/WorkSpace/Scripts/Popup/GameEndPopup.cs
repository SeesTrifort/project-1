using UnityEngine;
using System.Collections;

public class GameEndPopup : Popup {

	[SerializeField]
	UILabel score;

	[SerializeField]
	UILabel result;

	void Awake(){
		score.text =  "[0000FF]Score : [-]" + SceneController.Instance.gameController.score.ToString();

		int highscore = TransactionData.GetHightScore(SceneController.Instance.gameController.data.gameId);

		if (highscore >= SceneController.Instance.gameController.score){
			result.text = "[0000FF]High Score : [-]" + highscore + "\n\n";

		}else{
			// New Record 
			result.text = "[FF0000]High Score : [-]" + (SceneController.Instance.gameController.score) + "\n\n";
		
			TransactionData.SetHightScore(SceneController.Instance.gameController.data.gameId, SceneController.Instance.gameController.score);
		}

		result.text += "[0000FF]Max Combo : [-]" + SceneController.Instance.gameController.maxcombo.ToString() + "\n\n"
					+ "[0000FF]Correct   : [-]" + SceneController.Instance.gameController.correct.ToString() + "\n\n"
					+ "[0000FF]Wrong     : [-]" + SceneController.Instance.gameController.wrong.ToString() + "\n\n";
	}
}
