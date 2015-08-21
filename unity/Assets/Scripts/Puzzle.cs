using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour {

	PuzzleController controller ;
	Image image;

	public int _shapeNum;
	public int shapeNum{
		get{
			return _shapeNum;
		}set{
			_shapeNum = value;
			if (image == null) image = gameObject.GetComponent<Image>();
			image.sprite = controller.puzzleSprites[value];
			image.color = controller.puzzleColors[value];
		}
	}

	public int _lineId;
	public int lineId{
		get{
			return _lineId;
		}set{
			_lineId = value;
			transform.localPosition = new Vector3(0, 50 * (value+1), 0);
			gameObject.name = "puzzle " + value;
		}
	}

	public int listId;

	void Awake(){
		controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<PuzzleController>();
		shapeNum = Random.Range(0,controller.presentShape);
		listId = controller.presentlistId;
	}
}
