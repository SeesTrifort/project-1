using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UITexture)), ExecuteInEditMode]
public class UIAnimatableTexture : MonoBehaviour
{
	[SerializeField]
	public Texture mainTexture;

	[SerializeField]
	public UITexture target;

	void Update ()
	{
		if (target == null) {
			return;
		}

		target.mainTexture = mainTexture;
	}

	void Reset ()
	{
		target = GetComponent<UITexture>();
		if (target != null) {
			mainTexture = target.mainTexture;
		}
	}
}
