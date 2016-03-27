using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIWidget)), ExecuteInEditMode]
public class UIAnimatableHeight : MonoBehaviour
{
	[SerializeField]
	public float height;

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

		target.height = Mathf.RoundToInt(height);
	}

	void Reset ()
	{
		target = GetComponent<UIWidget>();
		if (target != null) {
			height = target.height;
		}
	}
}
