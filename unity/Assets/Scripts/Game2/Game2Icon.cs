using UnityEngine;
using System.Collections;

public class Game2Icon : MonoBehaviour {

	Game2Controller controller ;
	
	[SerializeField]
	UITexture image;

	[SerializeField]
	UIButton button;

	int _shapeNum;
	public int shapeNum{
		get{
			return _shapeNum;
		}set{
			_shapeNum = value;
			image.color = controller.gameUI.puzzleColors[value];

			button.hover = image.color;
			button.defaultColor = image.color;
			button.disabledColor = image.color;
			button.pressed = image.color;
		}
	}

	public int listId{
		set{
			if (value == -1){
				transform.localPosition = Vector3.zero;
				transform.localScale = Vector3.one;
				gameObject.name = "puzzle answer";
			}else{
				int width = value % controller.width;
				int height = value / controller.width;
				
				float widthf = width - ((float)controller.width/2) + 0.5f;
				float heightf = height - ((float)controller.height/2) + 0.5f;
				
				transform.localPosition = new Vector3(widthf * 95f , heightf * -105f, 0f);
				transform.localScale = Vector3.one;
				gameObject.name = "puzzle " + value;
			}
		}
	}
	
	void Awake(){
		controller = SceneController.mainController as Game2Controller;
	}
	
	public void ButtonClick(){
		Destroy(button);

		image.color = new Color(image.color.r, image.color.g, image.color.b, 0.4f);

		controller.IconClick(this);
	}
}
