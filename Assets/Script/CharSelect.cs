using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour {

	public Image[] CharPreview;
	private int LevelDone;
	public Button playButton;
	private int charPreviewActive;
	public GameObject selections;

	// Use this for initialization
	void Start () {
		LevelDone = PlayerPrefs.GetInt ("LevelDone");

		if (LevelDone > 3) {
			CharPreview [1].color = new Color (1, 1, 1, 1);
		}

		if (LevelDone > 7) {
			CharPreview [2].color = new Color (1, 1, 1, 1);
		}

		if (LevelDone > 11) {
			CharPreview [3].color = new Color (1, 1, 1, 1);
		}

		if (LevelDone > 15) {
			CharPreview [4].color = new Color (1, 1, 1, 1);
		}

		selections.transform.position = new Vector3(selections.transform.position.x, -362, selections.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		if (charPreviewActive == 1 && LevelDone <= 3) {
			playButton.interactable = false;
		} else if (charPreviewActive == 2 && LevelDone <= 7) {
			playButton.interactable = false;
		} else if (charPreviewActive == 3 && LevelDone <= 11) {
			playButton.interactable = false;
		} else if (charPreviewActive == 4 && LevelDone <= 15) {
			playButton.interactable = false;
		} else {
			playButton.interactable = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Char1") {
			PlayerPrefs.SetInt("Player",0);
			for(int z=0;z<CharPreview.Length;z++)
			{
				CharPreview [z].gameObject.SetActive (false);
			}
			CharPreview [0].gameObject.SetActive (true);
			charPreviewActive = 0;
		}

		if (other.name == "Char2") {
			PlayerPrefs.SetInt("Player",1);
			for(int z=0;z<CharPreview.Length;z++)
			{
				CharPreview [z].gameObject.SetActive (false);
			}
			CharPreview [1].gameObject.SetActive (true);
			charPreviewActive = 1;
		}

		if (other.name == "Char3") {
			PlayerPrefs.SetInt("Player",2);
			for(int z=0;z<CharPreview.Length;z++)
			{
				CharPreview [z].gameObject.SetActive (false);
			}
			CharPreview [2].gameObject.SetActive (true);
			charPreviewActive = 2;
		}

		if (other.name == "Char4") {
			PlayerPrefs.SetInt("Player",3);
			for(int z=0;z<CharPreview.Length;z++)
			{
				CharPreview [z].gameObject.SetActive (false);
			}
			CharPreview [3].gameObject.SetActive (true);
			charPreviewActive = 3;
		}

		if (other.name == "Char5") {
			PlayerPrefs.SetInt("Player",4);
			for(int z=0;z<CharPreview.Length;z++)
			{
				CharPreview [z].gameObject.SetActive (false);
			}
			CharPreview [4].gameObject.SetActive (true);
			charPreviewActive = 4;
		}
	}
}
