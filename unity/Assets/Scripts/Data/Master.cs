using UnityEngine;
using System.Collections;

public static class Master {

	public static void Initialize(){
		game1Data = new Game1Data();
		game2Data = new Game2Data();
	}

	public static Game1Data game1Data;
	public static Game2Data game2Data;

}
