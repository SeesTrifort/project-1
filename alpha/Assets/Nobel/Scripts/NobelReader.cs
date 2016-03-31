using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NobelReader : MonoBehaviour {

	[SerializeField]
	Text nameArea;

	[SerializeField]
	Text textArea;

	[SerializeField]
	ChoiceType[] choiceType;

	[SerializeField]
	ImageType[] imageType;

	int presentPhase;

	Phase phase;

	bool phaseEnd;

	bool readEnd;

	InputManager inputManager ;

	public void Start() {
		inputManager = InputManager.Instance;
		LoadPhase();
	}

	void StartReadPhase (int phaseNum)
	{
		presentPhase = phaseNum;

		StartCoroutine(ReadPhase(phaseNum));
	}

	IEnumerator ReadPhase (int phaseNum) 
	{
		phase = XmlDeserializer.DeserializeXml<Phase>(((TextAsset)Resources.Load("Nobel/" + phaseNum, typeof(TextAsset))).text);

		phaseEnd = false;

		int serifNum = 0;

		while (!phaseEnd) {

			if (serifNum < phase.charSerif.Count) yield return StartCoroutine(ShowCharSerif(phase.charSerif[serifNum]));
			else phaseEnd = true;

			serifNum ++;

			if (GameSetting.autoMode) {
				float time = 0f;
				while (time < 1f){
					time += Time.deltaTime;
					yield return new WaitForEndOfFrame();
				}
			} else {
				while (!InputManager.input){
					yield return new WaitForEndOfFrame();
				}
			}
			yield return new WaitForEndOfFrame();
		}

		if (phase.choice.NextPhaseNum == 0) StartCoroutine(ShowChoice(phase.choice));
		else StartReadPhase(phase.choice.NextPhaseNum);
	}

	IEnumerator ShowCharSerif (Phase.CharSerif charSerif) 
	{
		readEnd = false;

		foreach (ImageType type in imageType) {
			type.SetImage(charSerif);
		}
			
		char[] textArray = charSerif.Serif.ToCharArray();

		nameArea.text = charSerif.Name;

		textArea.text = "";

		int textCount = 0;

		while(!readEnd){

			if (textCount < textArray.Length) textArea.text += textArray[textCount];
			else readEnd = true;

			textCount ++;

			float time = 0f;
			while (time < 0.01f){
				time += Time.deltaTime;
				if (InputManager.input) readEnd = true;
				yield return new WaitForEndOfFrame();
			}
		}

		textArea.text = charSerif.Serif;
		yield return new WaitForEndOfFrame();

	}

	IEnumerator ShowChoice (Phase.Choice choice)
	{
		if (choice.question.Count == 0 || choice.Type == 0) Debug.Log("EndNobel");
		else {
			foreach (ChoiceType type in choiceType){
				type.SetQuestions(choice);
			}

			InputManager.inputButton = 0;

			while (InputManager.inputButton == 0)
			{
				yield return new WaitForEndOfFrame();
			}

			foreach (ChoiceType type in choiceType){
				type.SetDisable();
			}

			SavePhaseFlag();

			StartReadPhase(InputManager.inputButton);

			InputManager.inputButton = 0;
		}
	}

	public void SavePhase () {
		PlayerPrefs.SetInt("presentPhase", presentPhase);
		PlayerPrefs.Save();
	}

	public void LoadPhase () {
		int loadPhase = PlayerPrefs.GetInt("presentPhase", 1);
		StartReadPhase(loadPhase);
	}

	public void SavePhaseFlag () {
		PlayerPrefs.SetInt(presentPhase.ToString(), 2);
		PlayerPrefs.Save();
	}

#if UNITY_EDITOR
	public void ResetPhaseData () {
		PlayerPrefs.DeleteAll();
	}
#endif
}
