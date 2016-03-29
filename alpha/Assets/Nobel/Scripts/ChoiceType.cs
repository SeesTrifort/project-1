using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoiceType : MonoBehaviour {

	[SerializeField]
	int choiceType = 0;

	[SerializeField]
	QuestionType[] questions;

	public void SetQuestions (Phase.Choice choice) 
	{
		if (choiceType != choice.Type) 
		{
			gameObject.SetActive(false);
		} 
		else
		{
			gameObject.SetActive(true);

			for (int i = 0; i < questions.Length; i++) {
				if (i < choice.question.Count)
				{
					questions[i].SetQuestion(choice.question[i]);
				}
			}
		}
	}

	public void SetDisable()
	{
		gameObject.SetActive(false);
	}
}

[System.Serializable]
public class QuestionType {

	[SerializeField]
	Text choiceQuestion;

	[SerializeField]
	Button choiceButton;

	int nextPhaseNum;

	public void SetQuestion (Phase.Question question)
	{
		choiceQuestion.text = question.Text;

		nextPhaseNum = question.NextPhaseNum;

		choiceButton.onClick.RemoveAllListeners();

		choiceButton.onClick.AddListener(SendInput);
	}
		
	void SendInput () 
	{
		InputManager.inputButton = nextPhaseNum;
	}

}
