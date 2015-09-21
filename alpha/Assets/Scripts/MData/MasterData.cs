using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MasterData {

	public static void SetData(){

		// DEBUG Game DATA //
		Dictionary<int, Hashtable> gameDatas = new Dictionary<int, Hashtable>();

		Hashtable leftright = new Hashtable();
		leftright["gameId"] = 1;
		leftright["gameName"] = "leftright";
		leftright["timeLimit"] = 30;
		gameDatas.Add(1, leftright);

		GameMaster.SetData(gameDatas);



	}
}

public enum ResourceType{
	// Column : ResourceType

	diamond = 1,
	coin = 2,
	exp = 3,
}

public enum GameIds{
	// Column : GameId

	All = 0,
	LeftRight = 1,
}

public enum GameEffectType{
	// Colume : GameEffectType

	PlusTime = 1,
	PlusBomb = 2,
	PlusCoin = 3,
	PlusExp = 4,
}

