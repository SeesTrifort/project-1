using UnityEngine;
using System.Collections;

public class GameData{
	
	public int gameId;
	
	public string gameName;
	
	public int timeLimit ;
	
	public GameData SetData(Hashtable data){
		gameId = (int)data["gameId"];
		gameName = (string)data["gameName"];
		timeLimit = (int)data["timeLimit"];
		return this;
	}
}