using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class GameMaster {
	
	public static List<GameData> gameDatas = new List<GameData>();

	public static void SetData(Dictionary<int, Hashtable> datas){
		gameDatas.Clear();

		foreach(var data in datas){
			gameDatas.Add(new GameData().SetData(data.Value));
		}
	}

	public static GameData GetData(GameIds gameId){
		return gameDatas.FirstOrDefault(data => data.gameId == (int)gameId);
	}
}
