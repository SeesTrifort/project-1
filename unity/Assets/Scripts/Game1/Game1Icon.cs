using UnityEngine;
using System.Collections;

public class Game1Icon : MonoBehaviour {

	Game1Controller controller ;

	[SerializeField]
	UITexture image;

	int _shapeNum;
	public int shapeNum{
		get{
			return _shapeNum;
		}set{
			_shapeNum = value;
			image.color = controller.gameUI.puzzleColors[value];
		}
	}

	public int lineId{
		set{
			transform.localPosition = new Vector3(0, 155 * (value), 0);
			transform.localScale = Vector3.one;
			gameObject.name = "puzzle " + value;
		}
	}

	public int listId;

	void Awake(){
		controller = SceneController.mainController as Game1Controller;
		shapeNum = Random.Range(0,controller.presentShape);
		listId = controller.presentlistId;
	}
}
