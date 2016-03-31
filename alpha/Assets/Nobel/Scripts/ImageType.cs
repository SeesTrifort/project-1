using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageType : MonoBehaviour {

	[SerializeField]
	Image image;

	[SerializeField]
	int imageType = 0;

	public void SetImage (Phase.CharSerif serif) 
	{
		if (serif.ImageType == imageType)
		{
			Debug.Log(serif.Image);
			image.sprite = (Sprite)Resources.Load("Image/"+serif.Image , typeof(Sprite));
			gameObject.SetActive(true);
		}
		else if (serif.ImageReset)
		{
			gameObject.SetActive(false);
		}
	}
}
