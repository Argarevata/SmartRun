using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	//Digunakan untuk mengatur score;
	//Setiap 1 detik score tambah 1 point

	public int MyScore;
	public Text MyText;
	public Text FinalScoreText;
	public Text FinalScoreTextShade;

	[SerializeField]
	private float CoolDown = 1f;
	[SerializeField]
	private float ActualCoolDown=1f;

	public bool Endless;
	public FinishBox TheFinish;
	public PlayerController ThePlayer;

	// Use this for initialization
	void Start () {
		ThePlayer = FindObjectOfType<PlayerController> ();
		if (ThePlayer.Endless == false) {
			TheFinish = FindObjectOfType<FinishBox> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Mengimplementasi point ke dalam teks UI
		MyText.text = "Score: " + MyScore;

		ActualCoolDown += Time.deltaTime;

		if (ThePlayer.SuperMode) {
			CoolDown = 0.05f;
		} else {
			CoolDown = 1;
		}

		if (ThePlayer.Endless == true) {
			if (ActualCoolDown >= CoolDown) {
				MyScore += 1;
				ActualCoolDown = 0;
			}
		}

		if (ThePlayer.Endless == false) {
			if (ActualCoolDown >= CoolDown && TheFinish.Win == false) {
				MyScore += 1;
				ActualCoolDown = 0;
			}
		}

		if (Endless == true) {
			FinalScoreText.text = "" + MyScore;
			FinalScoreTextShade.text = "" + MyScore;
		}
	}
}
