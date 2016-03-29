using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	static InputManager _Instance;
	public static InputManager Instance {
		get{
			if (_Instance == null) {
				_Instance = new GameObject().AddComponent<InputManager>();
				_Instance.name = "InputManager";
			}
			return _Instance;
		}
	}

	public static bool input = false;

	public static int inputButton = 0;

	const float inputResetTime = 0.2f;

	float inputTime = 0f;

	void Update () {

		if (inputTime > inputResetTime)
		{
			if (Input.GetMouseButtonDown(0))
			{
				input = true;
			}
			else 
			{
				input = false;
			}
		}
		else
		{
			inputTime += Time.deltaTime;

			input = false;
		}
	}
}
