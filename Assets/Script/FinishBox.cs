using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishBox : MonoBehaviour {

	//Digunakan untuk memberi finish line pada Level. Setelah player menytentuh  ini maka akan muncul finish screen

	public GameObject FinishScreen;
	public GameObject FinishScreenLandscape;
	public CameraController TheCam;
	public CameraController TheCamLandscape;
	public GameObject FinishImage;
	public PlayerController ThePlayer;
	public bool Win;

	public SFXController TheSFX;

	public bool IsLevel1;
	public bool IsLevel5;
	public bool IsLevel15;

	public int ThisLevel;

	public string PlayerPrefsName;
	private int GetStars;

	public Image[] StarsAchived;

	// Use this for initialization
	void Start () {
		Win = false;
		ThePlayer = FindObjectOfType<PlayerController> ();
		TheSFX = FindObjectOfType<SFXController> ();
		print (PlayerPrefs.GetInt (PlayerPrefsName));

		for (int i = 0; i < 3; i++) {
			StarsAchived[i].color = new Color(0.25f, 0.14f, 0.02f, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Fungsi saat player menyentuh finish box, maka finish screen akan muncul dan kamera berhenti bergerak
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			TheCam.Playing = false;
			//Confetti.SetActive (true);
			StartCoroutine ("Winning");
			//FinishImage.SetActive (false);
			Win = true;
			if (PlayerPrefs.GetInt ("LevelDone") < (ThisLevel+1)) {
				PlayerPrefs.SetInt ("LevelDone", ThisLevel+1);
			}

			switch (ThisLevel) {
			case 1:
				{
					if (ThePlayer.TheScore.MyScore > 330) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 330 && ThePlayer.TheScore.MyScore > 300) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}
				break;
			case 2:
				{
					if (ThePlayer.TheScore.MyScore > 360) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 360 && ThePlayer.TheScore.MyScore > 320) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 3:
				{
					if (ThePlayer.TheScore.MyScore > 540) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 540 && ThePlayer.TheScore.MyScore > 400) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 4:
				{
					if (ThePlayer.TheScore.MyScore > 900) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 900 && ThePlayer.TheScore.MyScore > 600) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 5:
				{
					if (ThePlayer.TheScore.MyScore > 1000) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 1000 && ThePlayer.TheScore.MyScore > 700) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 6:
				{
					if (ThePlayer.TheScore.MyScore > 880) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 880 && ThePlayer.TheScore.MyScore > 750) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 7:
				{
					if (ThePlayer.TheScore.MyScore > 1030) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 1030 && ThePlayer.TheScore.MyScore > 820) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 8:
				{
					if (ThePlayer.TheScore.MyScore > 880) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 880 && ThePlayer.TheScore.MyScore > 700) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 9:
				{
					if (ThePlayer.TheScore.MyScore > 1370) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 1370 && ThePlayer.TheScore.MyScore > 950) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 10:
				{
					if (ThePlayer.TheScore.MyScore > 1300) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 1300 && ThePlayer.TheScore.MyScore > 1100) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 11:
				{
					if (ThePlayer.TheScore.MyScore > 910) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 910 && ThePlayer.TheScore.MyScore > 650) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 12:
				{
					if (ThePlayer.TheScore.MyScore > 880) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 880 && ThePlayer.TheScore.MyScore > 650) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 13:
				{
					if (ThePlayer.TheScore.MyScore > 1080) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 1080 && ThePlayer.TheScore.MyScore > 800) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 14:
				{
					if (ThePlayer.TheScore.MyScore > 770) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 770 && ThePlayer.TheScore.MyScore > 550) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;
			case 15:
				{
					if (ThePlayer.TheScore.MyScore > 1360) {
						GetStars = 3;
					} else if (ThePlayer.TheScore.MyScore <= 1360 && ThePlayer.TheScore.MyScore > 950) {
						GetStars = 2;
					} else {
						GetStars = 1;
					}
				}break;

			}

			if (PlayerPrefs.GetInt (PlayerPrefsName) < GetStars) {
				PlayerPrefs.SetInt (PlayerPrefsName, GetStars);
			}

			for (int i = 0; i < GetStars; i++) {
				StarsAchived[i].color = new Color(1, 1, 0, 1);
			}
		}


	}


	public IEnumerator Winning()
	{
		Win = true;
		yield return new WaitForSeconds (1);

		//if (PlayerPrefs.GetInt ("Orientation") == 1) {
		//	FinishScreen.SetActive (true);
		//} else {

		//}

		//if (IsLevel1 == true) {
		//	//ThePlayer.PlayGameScript.UnlockAchievement (GPGSIds.achievement_big_point);
		//	PlayGameScript.UnlockAchievement (GPGSIds.achievement_first_step);
		//	KTGameCenter.SharedCenter ().SubmitAchievement (100, "70628584", true);
		//}

		//if (IsLevel5 == true) {
		//	//ThePlayer.PlayGameScript.UnlockAchievement (GPGSIds.achievement_big_point);
		//	PlayGameScript.UnlockAchievement (GPGSIds.achievement_halfway_there);
		//	KTGameCenter.SharedCenter ().SubmitAchievement (100, "70628583", true);
		//}

		//if (IsLevel15 == true) {
		//	//ThePlayer.PlayGameScript.UnlockAchievement (GPGSIds.achievement_big_point);
		//	PlayGameScript.UnlockAchievement (GPGSIds.achievement_congratulations);
		//	KTGameCenter.SharedCenter ().SubmitAchievement (100, "70628582", true);
		//}



	}
}
