using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

	public TextMeshProUGUI TextDisplay;
	public TextMeshProUGUI NameDisplay;

	public string[] Sentences;
	private int Index;
	public float TypingSpeed;

	public string[] Name;

	public GameObject ButtonContinue;

	public Image[] Chara;

	public string LevelToLoad;

	//CutScene 1 Props
	public bool CutScene1;
	public GameObject[] TheImages;

	void Start()
	{
		TextDisplay.text = "";
		ButtonContinue.SetActive (false);

		if (CutScene1 == true) {
			StartCoroutine (TypeCutScene1 ());
		} else {
			StartCoroutine (Type ());
		}
	}

	void Update()
	{
		if (TextDisplay.text == Sentences [Index]) {
			ButtonContinue.SetActive (true);
		}
	}

	IEnumerator Type()
	{
		NameDisplay.text = ""+Name [Index];
		for (int i = 0; i < Chara.Length; i++) {
			Chara [i].color = new Color (0.75f, 0.75f, 0.75f, 1);
			Chara [i].rectTransform.localScale = new Vector2 (0.9f, 0.9f);
		}
		Chara [Index].color = new Color (1, 1, 1, 1);
		Chara [Index].rectTransform.localScale = new Vector2 (1.1f, 1.1f);


		foreach (char letter in Sentences[Index].ToCharArray()) {
			TextDisplay.text += letter;
			yield return new WaitForSeconds (TypingSpeed);
		}		
	}

	public void NextSentence()
	{
		ButtonContinue.SetActive (false);

		if (Index >= Sentences.Length-1) {
			Application.LoadLevel (LevelToLoad);
		}

		if (Index < Sentences.Length - 1) {
			Index++;
			TextDisplay.text = "";
			StartCoroutine (Type ());
		} else {
			TextDisplay.text = "";
			NameDisplay.text = "";
			ButtonContinue.SetActive (false);
		}
	}

	IEnumerator TypeCutScene1()
	{
		foreach (char letter in Sentences[Index].ToCharArray()) {
			TextDisplay.text += letter;
			yield return new WaitForSeconds (TypingSpeed);
		}	
	}

	public void NextSentenceCutScene1()
	{
		ButtonContinue.SetActive (false);

		if (Index >= Sentences.Length-1) {
			Application.LoadLevel (LevelToLoad);
		}

		if (Index < Sentences.Length - 1) {
			for (int i = 0; i < TheImages.Length; i++) {
				TheImages [i].SetActive (false);
			}
			TheImages [Index + 1].SetActive (true);

			NameDisplay.text = "" + Name [Index + 1];

			if (Index < Sentences.Length - 1) {
				Index++;
				TextDisplay.text = "";
				StartCoroutine (TypeCutScene1 ());
			} else {
				TextDisplay.text = "";
				NameDisplay.text = "";
				ButtonContinue.SetActive (false);
			}
		}
	}
		
}
