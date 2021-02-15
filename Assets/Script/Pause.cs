using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	//Digunakan untuk melakukan pause pada game dan mengatur semua function pada pause screen

	public TimeManager TheTime;
	public Button PauseButton;
	public GameObject PauseScreen;
	public StrikeGenerator TheStrike;

	public bool GamePaused;

	// Use this for initialization
	void Start () {
		TheTime = FindObjectOfType<TimeManager> ();
		TheStrike = FindObjectOfType<StrikeGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (TheTime.Slow == true || TheStrike.DisablePause == true) {
			PauseButton.interactable = false;
		} else {
			PauseButton.interactable = true;
		}
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
		PauseScreen.SetActive (true);
		GamePaused = true;
	}

	public void ResumeGame()
	{
		Time.timeScale = 1;
		PauseScreen.SetActive (false);
		GamePaused = false;
	}

	public void RetryGame()
	{
		Application.LoadLevel ("DIE");
	}

	public void MainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
