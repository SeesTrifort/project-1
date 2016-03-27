using UnityEngine;
using System.Collections;

public class UIUtils {
	public static GameObject MakePrefab (Object prefab, Transform parent){
		GameObject go = (GameObject.Instantiate(prefab)) as GameObject;
		go.transform.SetParent(parent);
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one;

		return go;
	}

	public static TweenPosition MoveTween (GameObject go , Vector3 distance, float time){
		TweenPosition tp = go.AddComponent<TweenPosition>();
		tp.from = go.transform.localPosition;
		tp.to = go.transform.localPosition + distance;
		tp.duration = time;

		return tp;
	}
}
