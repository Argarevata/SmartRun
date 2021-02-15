using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	//Function untuk mengurus button dan tampilan di Main Menu
	public GameObject SureQuit;
	public GameObject OrientationSelect;
	public GameObject ControlSelect;
	public Animator TheAnim;

	public AudioSource BoinkSFX;
	public GameObject[] LockSymbol;
	public GameObject[] Stars;
	public Button[] LevelsButton;
	public GameObject[] CharPreview;
	private int LevelDone;
	private int CharActive;

	public Button[] CharsButtons;

	// Use this for initialization
	void Start () {
		print (PlayerPrefs.GetInt ("LevelDone"));
		Time.timeScale = 1;	
		PlayerPrefs.SetInt ("Orientation", 1);
		PlayerPrefs.SetInt ("Control", 1);
		LevelDone = PlayerPrefs.GetInt ("LevelDone");
		CharActive = PlayerPrefs.GetInt ("Player");

		if (PlayerPrefs.GetInt ("NewPlayer") < 101) {
			PlayerPrefs.SetInt ("NewPlayer", 101);
			Application.LoadLevel ("Level0");

			for (int i = 0; i < 103; i++) {
				PlayerPrefs.SetInt (i.ToString(), 0);
			}
		}

		if (LevelDone > 3) {
			CharsButtons [1].interactable = true;
		}
		if (LevelDone > 7) {
			CharsButtons [2].interactable = true;
		}
		if (LevelDone > 11) {
			CharsButtons [3].interactable = true;
		}
		if (LevelDone > 15) {
			CharsButtons [4].interactable = true;
		}

		//Button pada Level Select akan menampilkan symbol "Done" jika level telah selesai
		for (int i = 1; i <= LevelDone; i++) {
			LockSymbol [i-1].SetActive (false);
			LevelsButton [i - 1].interactable = true;
			Stars [i - 1].SetActive (true);
		}

		//Berlaku pada iOS untuk Authenticate
		KTGameCenter.SharedCenter().Authenticate();
	
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Z)) {
			PlayerPrefs.SetInt ("LevelDone", 5);
			PlayerPrefs.SetInt ("StarLevel1", 3);
			PlayerPrefs.SetInt ("StarLevel2", 3);
			PlayerPrefs.SetInt ("StarLevel3", 2);
			PlayerPrefs.SetInt ("StarLevel4", 2);
			PlayerPrefs.SetInt ("StarLevel5", 0);
			//PlayerPrefs.SetInt ("NewPlayer", 0);
		}

		if (Input.GetKeyDown (KeyCode.V)) {
			PlayerPrefs.SetInt ("StarLevel1", 0);
			PlayerPrefs.SetInt ("StarLevel2", 0);
			PlayerPrefs.SetInt ("StarLevel3", 0);
			PlayerPrefs.SetInt ("StarLevel4", 0);
			PlayerPrefs.SetInt ("StarLevel5", 0);
			PlayerPrefs.SetInt ("StarLevel6", 0);
			PlayerPrefs.SetInt ("StarLevel7", 0);
			PlayerPrefs.SetInt ("StarLevel8", 0);
			PlayerPrefs.SetInt ("StarLevel9", 0);
			PlayerPrefs.SetInt ("StarLevel10", 0);
			PlayerPrefs.SetInt ("StarLevel11", 0);
			PlayerPrefs.SetInt ("StarLevel12", 0);
			PlayerPrefs.SetInt ("StarLevel13", 0);
			PlayerPrefs.SetInt ("StarLevel14", 0);
			PlayerPrefs.SetInt ("StarLevel15", 0);
		}
	}

	public void ShowExit()
	{
		//SureQuit.SetActive (true);
		TheAnim.SetBool("Exit", true);
		BoinkSFX.Play ();
	}

	public void ShowOrientation()
	{
		OrientationSelect.SetActive (true);
	}

	public void PlayTutor()
	{
		Application.LoadLevel ("Level0");
	}

	public void ShowControl()
	{
		ControlSelect.SetActive (true);
	}

	public void OrientationPortrait()
	{
		PlayerPrefs.SetInt ("Orientation", 1);
		OrientationSelect.SetActive (false);
	}

	public void OrientationLandscape()
	{
		PlayerPrefs.SetInt ("Orientation", 0);
		OrientationSelect.SetActive (false);
	}

	public void ControlSwipe()
	{
		PlayerPrefs.SetInt ("Control", 1);
		ControlSelect.SetActive (false);
	}

	public void ControlButton()
	{
		PlayerPrefs.SetInt ("Control", 0);
		ControlSelect.SetActive (false);
	}

	public void NotQuitYet()
	{
		//SureQuit.SetActive (false);
		TheAnim.SetBool("Exit", false);
		BoinkSFX.Play ();
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void GoEndlessMenu()
	{
		CharActive = PlayerPrefs.GetInt ("Player");
		TheAnim.SetBool ("Home", false);
		TheAnim.SetBool ("Endless", true);
		for(int z=0;z<CharPreview.Length;z++)
		{
			CharPreview [z].SetActive (false);
		}
		CharPreview [CharActive].SetActive (true);
		BoinkSFX.Play ();
	}

	public void GoEndless()
	{
		if (PlayerPrefs.GetInt ("NewPlayer") >= 99) {
			Application.LoadLevel ("Endless");
		} else {
			PlayerPrefs.SetInt ("NewPlayer", 100);
			Application.LoadLevel ("Level0");
		}
	}

	public void Story()
	{
		TheAnim.SetBool ("Home", false);
		TheAnim.SetBool ("Level", true);
		BoinkSFX.Play ();
	}

	public void Info()
	{
		TheAnim.SetBool ("Home", false);
		TheAnim.SetBool ("Info", true);
		BoinkSFX.Play ();
	}

	public void Home()
	{
		TheAnim.SetBool ("Home", true);
		TheAnim.SetBool ("Level", false);
		TheAnim.SetBool ("Endless", false);
		TheAnim.SetBool ("Settings", false);
		TheAnim.SetBool ("Info", false);
		BoinkSFX.Play ();
	}

	public void Settings()
	{
		TheAnim.SetBool ("Home", false);
		TheAnim.SetBool ("Settings", true);
	}

	public void LoadLevel(string x)
	{
		if (PlayerPrefs.GetInt ("NewPlayer") >=99) {
			Application.LoadLevel (x);
		} else {
			PlayerPrefs.SetInt ("NewPlayer", 100);
			Application.LoadLevel ("Level0");
		}
	}

	public void SelectChar(int x)
	{
		//Player0 = Red, Player1 = Yellow
		PlayerPrefs.SetInt("Player",x);
		for(int z=0;z<CharPreview.Length;z++)
		{
			CharPreview [z].SetActive (false);
		}
		CharPreview [x].SetActive (true);
	}

	public void ShowLeaderBoardIOS()
	{
		KTGameCenter.SharedCenter ().ShowLeaderboard ("com.Evan.CalegRun.Leaderboard");
	}

	public void ShowAchievementIOS()
	{
		KTGameCenter.SharedCenter().ShowAchievements();
	}
		
}
