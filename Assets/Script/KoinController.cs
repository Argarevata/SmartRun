using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoinController : MonoBehaviour {

	//Digunakan untuk mengatur fungsi dari koin/Info Amulet [Men-trigger info controller untuk menampilkan info]

	public GameObject Particles;
	public InfoController TheInfos;
	public ScoreController TheScore;
	public StrikeGenerator TheStrike;

	public bool isNotEndless;
	public int InfoToShow;
	public bool NoInfo;
	public PlayerController ThePlayer;

	public SFXController TheSFX;

	// Use this for initialization
	void Start () {
		if (NoInfo == false) {
			TheInfos = FindObjectOfType<InfoController> ();
			TheStrike = FindObjectOfType<StrikeGenerator> ();
		}
		TheScore = FindObjectOfType<ScoreController> ();
		TheSFX = FindObjectOfType<SFXController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isNotEndless == true) {
			TheInfos = FindObjectOfType<InfoController> ();
			TheStrike = FindObjectOfType<StrikeGenerator> ();
		}
		TheScore = FindObjectOfType<ScoreController> ();
		TheSFX = FindObjectOfType<SFXController> ();
		
	}


	// Jika bertabrakan dengan player
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			TheSFX.PlayCoinSFX ();
			Instantiate (Particles, transform.position, transform.rotation);
			if (NoInfo == false) {
				if (isNotEndless == true) {
					TheInfos.ShowSelectedInfo (InfoToShow);
				} else {
					TheInfos.ShowInfo ();
				}
			}

			//menambah points
			if (NoInfo == false) {
				TheScore.MyScore += 10;
				//PlayGameScript.IncrementAchievement (GPGSIds.achievement_little_knowledge, 1);
				//PlayGameScript.IncrementAchievement (GPGSIds.achievement_medium_knowledge, 1);
				//PlayGameScript.IncrementAchievement (GPGSIds.achievement_big_knowledge, 1);
			} else {
				TheScore.MyScore += 5;
			}

			//Me-nonaktifkan questions
			if (NoInfo == false) {
				TheStrike.TheQuestion.gameObject.SetActive (false);
				TheStrike.TheRightAnswer.gameObject.SetActive (false);
				TheStrike.TheWrongAnswer.gameObject.SetActive (false);
				TheStrike.JumpTitle.gameObject.SetActive (false);
				TheStrike.SlideTitle.gameObject.SetActive (false);
			}

			Destroy (gameObject);
		} else if (other.gameObject.tag == "Destroyer") {
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Obstacle") {
			Destroy (other.gameObject);
		}
	}

	//Jika kena dengan obstacle, obstacle dihancurkan agar tidak tumpang tindih
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Obstacle") {
			Destroy (other.gameObject);
		}
	}
		
}
