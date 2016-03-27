using UnityEngine;
using System.Collections;

public class GameData{
	
	public int gameId;
	
	public string gameName;
	
	public int timeLimit;

	public int maxColor;
	
	public GameData SetData(Hashtable data){
		gameId = (int)data["gameId"];
		gameName = (string)data["gameName"];
		timeLimit = (int)data["timeLimit"];
		maxColor = (int)data["maxColor"];
		return this;
	}
}