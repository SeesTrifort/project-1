using UnityEngine;
using System.Collections;

public static class CharacterCommon {

	static Color[] _characterColors;
	public static Color[] characterColors {
		get{
			if (_characterColors == null){
				_characterColors = new Color[10];
				_characterColors[0] = new Color(1f, 0f, 0f, 1f);
				_characterColors[1] = new Color(0f, 1f, 0f, 1f);
				_characterColors[2] = new Color(0f, 0f, 1f, 1f);
				_characterColors[3] = new Color(1f, 1f, 0f, 1f);
				_characterColors[4] = new Color(1f, 0f, 1f, 1f);
				_characterColors[5] = new Color(0f, 1f, 1f, 1f);
				_characterColors[6] = new Color(0.7f, 0.7f, 0.7f, 1f);
				_characterColors[7] = new Color(0f, 0.5f, 1f, 1f);
				_characterColors[8] = new Color(1f, 0f, 0.5f, 1f);
				_characterColors[9] = new Color(0.5f, 1f, 0f, 1f);
			}
			return _characterColors;
		}
	}

	public static void ShuffleColor (){
		for (int i = 0; i < 20; i++) {
			int random1 = Random.Range(0, characterColors.Length);
			int random2 = Random.Range(0, characterColors.Length);
			
			if (random1 != random2){
				Color temp = characterColors[random1];
				characterColors[random1] = characterColors[random2];
				characterColors[random2] = temp;
			}
		}
	}


}
