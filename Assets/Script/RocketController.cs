using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

	//Digunakan untuk mengatur pergerakan rocket

	public Rigidbody2D MyBody;
	public float Speed;
	public PlayerController ThePlayer;
	[SerializeField]
	private int MinusHealth;
	public GameObject Particles;
	public ScoreController TheScore;
	public bool IsDone = false;
	public TimeManager TheTime;
	public StrikeGenerator TheStrike;
	public GameObject SmokeParticle;
	public GameObject Pop;
	public int X;

	public SFXController TheSFX;

	public float CoolDown;
	public float ActualCoolDown;

	// Use this for initialization
	void Start () {
		MyBody = GetComponent<Rigidbody2D> ();
		ThePlayer = FindObjectOfType<PlayerController> ();
		StartCoroutine ("KillMe");
		TheScore = FindObjectOfType<ScoreController> ();
		TheTime = FindObjectOfType<TimeManager> ();
		TheStrike = FindObjectOfType<StrikeGenerator> ();
		ThePlayer.CanMove = false;
		TheSFX = FindObjectOfType<SFXController> ();
		CoolDown = 0;
		ActualCoolDown = 0.15f;
	}
	
	// Update is called once per frame
	void Update () {
		if (TheTime.Slow == false) {
			Speed = 100;
			if (TheSFX.RocketSFX.isPlaying == false) {
				TheSFX.PlayRocketSFX ();
			}
		}
		MyBody.velocity = new Vector2 (-Speed, 0);

		//Generate asap particle
		if ((TheTime.Slow == false) && (MyBody.transform.position.x >= ThePlayer.transform.position.x-20)) {
			CoolDown += Time.deltaTime;
			if (CoolDown >= ActualCoolDown) {
				CoolDown = 0;
				ActualCoolDown = 5;
				Instantiate (SmokeParticle, Pop.transform.position, Pop.transform.rotation);
			}
		}

		//Jika player berhasil menghindar maka score bertambah 
		if (MyBody.transform.position.x < ThePlayer.transform.position.x && IsDone == false) {
			TheScore.MyScore += 20;
			//PlayGameScript.IncrementAchievement (GPGSIds.achievement_smart, 1);
			//PlayGameScript.IncrementAchievement (GPGSIds.achievement_brilliant, 1);
			//PlayGameScript.IncrementAchievement (GPGSIds.achievement_genius, 1);
			if (PlayerPrefs.GetInt ("Orientation") == 1) {
				TheStrike.GoodThumbs.SetActive (true);
			}else if (PlayerPrefs.GetInt ("Orientation") == 0) {
				TheStrike.GoodThumbsLandscape.SetActive (true);
			}
			IsDone = true;
		}

		// rocket dimusnahkan setelah melewati player
		if (MyBody.transform.position.x < ThePlayer.transform.position.x - 40) {
			Destroy (gameObject);
		}
	}

	//Jika terkena player maka akan mengurangi player health, memunculkan partikel
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			TheSFX.PlayDamagedSFX ();
			ThePlayer.Health -= MinusHealth;
			Instantiate (Particles, transform.position, transform.rotation);
			if (PlayerPrefs.GetInt ("Orientation") == 1) {
				TheStrike.BadThumbs.SetActive (true);
			}else if (PlayerPrefs.GetInt ("Orientation") == 0) {
				TheStrike.BadThumbsLandscape.SetActive (true);
			}
			Destroy (gameObject);
		}


	}

	public IEnumerator KillMe()
	{
		yield return new WaitForSeconds (7);
		Destroy (gameObject);
	}
}
