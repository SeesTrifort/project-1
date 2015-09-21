using UnityEngine;
using System.Collections;

public class DragHandler : MonoBehaviour {

	GameController controller ;

	Vector3 startPosition = Vector3.zero;
	bool touchCollider;

	bool _onPress = false;
	bool onPress{
		get{
			return _onPress;
		}set{
			if (!_onPress && value){
				startPosition = Input.mousePosition;

				touchCollider = false;
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit)){
					if (hit.collider.name.Contains("btn")) touchCollider = true;
				}

			}else if (_onPress && !value){
				if (!touchCollider) controller.SendMessage("Swipe", Input.mousePosition - startPosition);
			}
			_onPress = value;
		}
	}

	public void Initialize (GameController _controller){
		controller = _controller;
	}

	void Update () {
		if (Input.GetMouseButton(0)){
			onPress = true;
		}else{
			onPress = false;
		}
	}
}
