using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIWidget)), ExecuteInEditMode]
public class UIAnimatableColor : MonoBehaviour
{
	[SerializeField]
	public Color color = Color.white;

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

		target.color = color;
	}

	void Reset ()
	{
		target = GetComponent<UIWidget>();
		if (target != null) {
			color = target.color;
		}
	}
}
