using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	Animator _animator;
	Animator animator{
		get{
			if (_animator == null) _animator = GetComponent<Animator>();
			return _animator;
		}
	}

	bool _isOpen = false;
	public bool isOpen {
		get{
			return _isOpen;
		}set{
			if (_isOpen != value){

				if (value) animator.SetTrigger("Open");
				else animator.SetBool("Open",false);

				_isOpen = value;
			}
		}
	}

	[HideInInspector] public int stackNum;

	public void Show(){
		isOpen = true;
	}

	public void Hide(){
		isOpen = false;
	}

	public void Close(){
		isOpen = false;
		animator.SetTrigger("Close");
	}

	public void ButtonClick(GameObject btn){
		SceneController.Instance.gameController.ButtonClick(btn);
	}
}
