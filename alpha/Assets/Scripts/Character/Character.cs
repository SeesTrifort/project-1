using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	[SerializeField]
	UISprite[] colorSprites;

	int _color;
	public int color{
		get{
			return _color;
		}set{
			value = Mathf.Max(0, (Mathf.Min(CharacterCommon.characterColors.Length, value)));
			foreach(UISprite sprite in colorSprites){
				sprite.color = CharacterCommon.characterColors[value];
			}
			_color = value;
		}
	}

	public int id;

	public void SetLeftRightListId (int listId){
		GetComponent<UIWidget>().depth = 20 - listId;

		transform.localPosition = new Vector3(0, 100 * listId, 0);
	
		transform.localScale = Vector3.one;
		
		int dimension = 150 - listId * 14;
		GetComponent<UIWidget>().SetDimensions(dimension, dimension);

		id = listId;
	}

	public void LeftRightCorrectAnimation (){
		TweenPosition tp = UIUtils.MoveTween(gameObject, new Vector3(color%2 == 0 ? 200 : -200 , -300, 0), 0.2f);
		tp.AddOnFinished(DestorySelf);
	}

	public void DestorySelf(){
		GameObject.Destroy(gameObject);
	}
}
