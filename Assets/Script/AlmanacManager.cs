using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacManager : MonoBehaviour {

	public Animator myAnim;
	public AudioSource theSFX;
	public string[] theInfos;
	public int addition;
	public Button[] theInfoButtons;
	public Text textToShow;
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("0", 99);
		PlayerPrefs.SetInt ("30", 99);
		PlayerPrefs.SetInt ("65", 99);
		PlayerPrefs.SetInt ("80", 99);

		theSFX = GetComponent<AudioSource> ();

		for (int i = 0; i < theInfoButtons.Length; i++) {
			string temp = (i + addition).ToString ();
			if (PlayerPrefs.GetInt (temp) > 1) {
				theInfoButtons [i].interactable = true;
			} else {
				theInfoButtons [i].interactable = false;
				theInfoButtons[i].GetComponentInChildren<Text>().text = "???";
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowTheInfo(int x)
	{
		textToShow.text = "" + theInfos [x];
		myAnim.SetBool ("show", true);
		theSFX.Play ();
	}

	public void HideTheInfo()
	{
		myAnim.SetBool ("show", false);
		theSFX.Play ();
	}

	public void GoMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
