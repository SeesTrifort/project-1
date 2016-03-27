using UnityEngine;
using System.Collections;

public static class TransactionData {

	public static int GetHightScore(int gameId){
		return PlayerPrefs.GetInt("HightScore_" + gameId, 0);
	}

	public static void SetHightScore(int gameId, int score){
		PlayerPrefs.SetInt("HightScore_" + gameId, score);
		PlayerPrefs.Save();
	}
}
