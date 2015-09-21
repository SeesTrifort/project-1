using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIWidget)), ExecuteInEditMode]
public class UIAnimatableWidth : MonoBehaviour
{
	[SerializeField]
	public float width;

	[SerializeField]
	public UIWidget target;

	void Awake ()
	{

	}

	void Update ()
	{
		if (target == null) {
			return;
		}

		target.width = Mathf.RoundToInt(width);
	}

	void Reset ()
	{
		target = GetComponent<UIWidget>();
		if (target != null) {
			width = target.width;
		}
	}
}
