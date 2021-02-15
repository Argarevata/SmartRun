using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

	//Mengatur SFX pada game

	public AudioSource CoinSFX;
	public AudioSource JamuSFX;
	public AudioSource JumpSFX;
	public AudioSource SlideSFX;
	public AudioSource BGM;
	public AudioSource WarningBGM;
	public AudioSource DamagedSFX;
	public AudioSource RocketSFX;
	public AudioSource BoostSFX;

	private PlayerController ThePlayer;

	public TimeManager TheTime;
	// Use this for initialization
	void Start () {
		BGM.Play ();
		TheTime = FindObjectOfType<TimeManager> ();
		ThePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ThePlayer.Health > 0) {
			if (TheTime.Slow == true) {
				if (WarningBGM.isPlaying == false) {
					PauseBGM ();
					PlayWarningBGM ();
				}
			} else {
				if (BGM.isPlaying == false) {
					WarningBGM.Stop ();
					BGM.Play ();
				}
			}
		}
	}

	public void PlayCoinSFX()
	{
		CoinSFX.Play ();
	}

	public void PlayJamuSFX()
	{
		JamuSFX.Play ();
	}

	public void PlayDamagedSFX()
	{
		DamagedSFX.Play ();
	}

	public void PlayJumpSFX()
	{
		JumpSFX.Play ();
	}

	public void PlaySlideSFX()
	{
		SlideSFX.Play ();
	}

	public void PauseBGM()
	{
		BGM.Pause ();
	}

	public void PlayWarningBGM()
	{
		WarningBGM.Play ();
	}

	public void PlayRocketSFX()
	{
		RocketSFX.Play ();
	}
}
