using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour {

	//Digunakan untuk mengontrol player. Lompat, Slide, Health  dan player animation

	public Rigidbody2D MyBody;
	public float Speed;
	public float NormalSpeed = 7;
	public float JumpForce;
	public int Strike = 0;

	public bool Grounded = true;
	public bool Endless;

	[SerializeField]
	public float SlideCoolDown = 0.7f;
	[SerializeField]
	public float ActualSlideCoolDown=0f;

	[SerializeField]
	public float JumpCoolDown = 0.5f;
	[SerializeField]
	public float ActualJumpCoolDown=0f;

	public Animator Anim;
	public Slider HealthBar;
	public Slider HealthBarLandscape;
	public int Health;
	public GameObject Death;
	public GameObject DeathLandscape;
	public SwipeControl TheSwipe;
	public TimeManager TheTime;
	public StrikeGenerator TheStrike;
	public ScoreController TheScore;
	//public PlayGameScript ThePlayGameScript;
	public string ThisLevelName;
	public bool CanMove;
	public int addToAchievements;
	public GameObject JumpDust;
	public GameObject SlideDust;
	public GameObject JumpDustPos;
	public GameObject SlideDustPos;
	public SuperMode theSuper;

	public SFXController TheSFX;

	public GameObject SpeedParticles;
	public GameObject SpeedText;
	private int SpeedUpCount;

	public Pause ThePause;

	public GameObject[] Char;

	public bool SuperMode;
	public GameObject SuperEffect;
	private float SuperModeCoolDown;
	private float ActualSuperMode;

	private InfoController theInfo;
	public int infoNum;

    private void Awake()
    {
		PlayerPrefs.SetInt("Orientation", 1);
		PlayerPrefs.SetInt("Control", 1);
	}
    // Use this for initialization
    void Start () {
		if (Endless == true) {
			int p = PlayerPrefs.GetInt ("Player");
			Char [p].SetActive (true);
		}
		theInfo = FindObjectOfType<InfoController> ();
		CanMove = true;
		MyBody = GetComponent<Rigidbody2D> ();
		Grounded = true;
		Anim = GetComponentInChildren<Animator> ();
		Health = 100;
		TheSwipe = FindObjectOfType<SwipeControl> ();
		TheTime = FindObjectOfType<TimeManager> ();
		TheStrike = FindObjectOfType<StrikeGenerator> ();
		TheScore = FindObjectOfType<ScoreController> ();
		PlayerPrefs.SetString ("Level", ThisLevelName);
		JumpCoolDown = 0.3f;
		ActualJumpCoolDown = JumpCoolDown;
		ActualSlideCoolDown = SlideCoolDown;
		CanMove = true;
		//ThePlayGameScript = GetComponent<PlayGameScript> ();
		addToAchievements = 0;
		TheSFX = FindObjectOfType<SFXController> ();
		SpeedUpCount = 0;
		ThePause = FindObjectOfType<Pause> ();
		//HealthBar = FindObjectOfType<Slider> ();
		SuperMode = false;
		SuperModeCoolDown = 2.5f;
		ActualSuperMode = 0f;
		theSuper = FindObjectOfType<SuperMode> ();
	}
	
	// Update is called once per frame
	void Update () {	
		//Player akan terus berlari sesuai dengan speed yang dimiliki. Speed akan terus meningkat sesuai dengan score, semakin tinggi maka speed semakin cepat
		MyBody.velocity = new Vector2 (Speed, MyBody.velocity.y);
		ActualSlideCoolDown += Time.unscaledDeltaTime;
		ActualJumpCoolDown += Time.unscaledDeltaTime;
		//Jika player tekkan 'Space' atau tombol lompat
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || TheSwipe.Swipe_Up == true) && Grounded == true && CanMove == true && (PlayerPrefs.GetInt("Control")==1)) {
			Jump ();
		}

		//Jika player tekkan tombol slide
		if ((Input.GetKeyDown (KeyCode.DownArrow) || TheSwipe.Swipe_Down == true) && ActualSlideCoolDown>=SlideCoolDown && CanMove ==true && (PlayerPrefs.GetInt("Control")==1)) {
			Slide ();
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			//Health = 0;
			theSuper.EnergyBar.value=100;
		}

		// Developer Function untuk tes Slow Motion [DELETED SOON]
		if (Input.GetKeyDown (KeyCode.W)) {
			if (TheTime.Slow == false) {
				TheTime.SlowMo ();
				Health = 100;
				TheTime.Slow = true;
			} else {
				TheTime.UnSlowMo ();
				TheTime.Slow = false;
			}
		}

		// Mengatur Slide Animation
		if (ActualSlideCoolDown >= SlideCoolDown) {
			Anim.SetBool ("Slide", false);
			TheSFX.SlideSFX.Stop ();
			Speed = NormalSpeed;
		}
			
		if (Input.GetKeyDown (KeyCode.U)) {
			//theInfo.ShowSelectedInfo (infoNum);
		}

		//Mengatur Health dengan Healthbar
		HealthBar.value = Health;
		if (Health > 100) {
			Health = 100;
		}

		//Jika health <=0 maka mati
		if (Health <= 0) {
			if (PlayerPrefs.GetInt ("Orientation") == 1 || PlayerPrefs.GetInt ("Orientation") == null) {
				Death.SetActive (true);
			} else {
				DeathLandscape.SetActive (true);
			}
			if (Endless == true && addToAchievements<1) {
				//PlayGameScript.AddScoreToLeaderboard (GPGSIds.leaderboard_leaderboard, TheScore.MyScore);
				//KTGameCenter.SharedCenter().SubmitScore(TheScore.MyScore,"com.Evan.CalegRun.Leaderboard");
				//PlayGameScript.IncrementAchievement (GPGSIds.achievement_newbie_runner, 1);
				//PlayGameScript.IncrementAchievement (GPGSIds.achievement_intermediate_runner, 1);
				//PlayGameScript.IncrementAchievement (GPGSIds.achievement_pro_runner, 1);
				addToAchievements += 2;
			}
			if (TheSFX.BGM.isPlaying == true) {
				TheSFX.PauseBGM ();
			}
			Time.timeScale = 0;
		}

		//Mengatur kecepatan player. Semakin tinggi skor maka player semakin cepat
		if (TheScore.MyScore >= 500) {
				NormalSpeed = 15;
				Speed = 15;
				Anim.speed = 1.8f;
				SlideCoolDown = 0.6f;
			if (SpeedUpCount<6 && Endless == true) {
				StartCoroutine ("SpeedUp");
				SpeedUpCount = 6;
			}
		} else if (TheScore.MyScore < 500 && TheScore.MyScore >= 400) {
			NormalSpeed = 14;
			Speed = 14;
			Anim.speed = 1.6f;
			SlideCoolDown = 0.6f;
			if (SpeedUpCount<5 && Endless == true) {
				StartCoroutine ("SpeedUp");
				SpeedUpCount = 5;
			}
		} else if (TheScore.MyScore < 400 && TheScore.MyScore >= 250) {
			NormalSpeed = 13;
			Speed = 13;
			Anim.speed = 1.4f;
			SlideCoolDown = 0.6f;
			if (SpeedUpCount<4 && Endless == true) {
				StartCoroutine ("SpeedUp");
				SpeedUpCount = 4;
			}
		}
		else if (TheScore.MyScore < 250 && TheScore.MyScore >= 150) {
			NormalSpeed = 12;
			Speed = 12;
			Anim.speed = 1.3f;
			SlideCoolDown = 0.6f;
			if (SpeedUpCount<3 && Endless == true) {
				StartCoroutine ("SpeedUp");
				SpeedUpCount = 3;
			}
		} else if (TheScore.MyScore < 150 && TheScore.MyScore >= 60) {
			NormalSpeed = 11;
			Speed = 11;
			Anim.speed = 1.2f;
			SlideCoolDown = 0.7f;
			if (SpeedUpCount<2 && Endless == true) {
				StartCoroutine ("SpeedUp");
				SpeedUpCount = 2;
			}
		} else if (TheScore.MyScore < 60 && TheScore.MyScore >= 30) {
			NormalSpeed = 10;
			Speed = 10;
			Anim.speed = 1.1f;
			if (SpeedUpCount<1 && Endless == true) {
				StartCoroutine ("SpeedUp");
				SpeedUpCount = 1;
			}

		}

		//Super Mode
		if (TheSwipe.Swipe_Right == true && Time.timeScale==1 && theSuper.EnergyBar.value>99) {
			SuperMode = true;
			theSuper.EnergyBar.value = 98;
			TheSFX.BoostSFX.Play ();
		}

		if (SuperMode == true) {
			ActualSuperMode += Time.deltaTime;
			theSuper.EnergyBar.value -= Time.deltaTime*40;
			Speed += 20;
			if (ActualSlideCoolDown >= SlideCoolDown) {
				SuperEffect.SetActive (true);
			} else {
				SuperEffect.SetActive (false);
			}
			if (ActualSuperMode >= SuperModeCoolDown) {
				ActualSuperMode = 0;
				Speed -= 20;
				SuperMode = false;
				SuperEffect.SetActive (false);
				TheSFX.BoostSFX.Stop ();
			}
		}

		//freeze pergerakan player untuk sementara waktu
		if (CanMove != true) {
			StartCoroutine ("CanMoveAgain");
		}

		if (TheScore.MyScore >= 1000 && Endless == true) {
			//PlayGameScript.UnlockAchievement (GPGSIds.achievement_small_point);
			//KTGameCenter.SharedCenter ().SubmitAchievement (100, "70628579", true);
		}

		if (TheScore.MyScore >= 2500 && Endless == true) {
			//PlayGameScript.UnlockAchievement (GPGSIds.achievement_medium_point);
			//KTGameCenter.SharedCenter ().SubmitAchievement (100, "70628580", true);
		}

		if (TheScore.MyScore >= 5000 && Endless == true) {
			//PlayGameScript.UnlockAchievement (GPGSIds.achievement_big_point);
			//KTGameCenter.SharedCenter ().SubmitAchievement (100, "70628581", true);
		}

	}

	//Fitur lompat
	public void Jump()
	{
		if (Grounded == true && CanMove == true && ActualJumpCoolDown>=JumpCoolDown && ThePause.GamePaused == false) 
		{
				TheSFX.SlideSFX.Stop ();
				Grounded = false;
				//TheTime.UnSlowMo ();
				Speed = NormalSpeed;
				MyBody.velocity = new Vector2 (Speed, JumpForce);
				Anim.SetBool ("Jump", true);
				Anim.SetBool ("Slide", false);
				ActualJumpCoolDown = 0;
				TheSwipe.Reset ();
				Instantiate (JumpDust, JumpDustPos.transform.position, JumpDustPos.transform.rotation);
				TheSFX.PlayJumpSFX ();
		}
	}

	//fitur slide
	public void Slide()
	{
		if (CanMove == true && ThePause.GamePaused == false) 
		{
			Grounded = false;
			//TheTime.UnSlowMo ();
			MyBody.velocity = new Vector2 (Speed, -JumpForce * 2);
			Anim.SetBool ("Slide", true);
			ActualSlideCoolDown = 0f;
			TheSwipe.Reset ();
			//TheSFX.PlaySlideSFX ();
			//Instantiate (JumpDust, JumpDustPos.transform.position, JumpDustPos.transform.rotation);
		}
	}

	//Jika player menyentuh tanah/ platform [Jump Function maka bisa digunakan lagi]
	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground" && Grounded == false) {
			Grounded = true;
			Anim.SetBool ("Jump", false);
			if(Anim.GetBool("Slide") == true)
			{
				Instantiate (SlideDust, SlideDustPos.transform.position, SlideDustPos.transform.rotation);
				TheSFX.PlaySlideSFX ();
			}
		}
	}

	public IEnumerator CanMoveAgain(){
		yield return new WaitForSecondsRealtime (0.5f);
		CanMove = true;
	}

	public IEnumerator SpeedUp(){
		SpeedText.SetActive (true);
		//SpeedParticles.SetActive (true);
		yield return new WaitForSecondsRealtime (1f);
		SpeedText.SetActive (false);
		//SpeedParticles.SetActive (false);
	}

	public void UnpauseTime()
	{
		Time.timeScale = 1;
	}

}
