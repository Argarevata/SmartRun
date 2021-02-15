using UnityEngine;

public class TimeManager : MonoBehaviour {

	//Digunakan untuk mengatur timescale dan fitur SlowMotion

	public float SlowDownFactor = 0.05f;
	public float SlowDownLength = 40f;

	public bool Slow;
	public RocketTimer TheTimer;
	public RocketTimer TheTimerLandscape;
	public PlayerController ThePlayer;

	[SerializeField]
	private float CoolDown = 5f;
	[SerializeField]
	private float ActualCoolDown=0f;

	void Start()
	{
		ThePlayer = FindObjectOfType<PlayerController> ();
	}

	void Update()
	{
		Time.timeScale = Mathf.Clamp (Time.timeScale, 0f, 1f);
		if (Slow) {
			if (ThePlayer.Grounded == false) {
				ThePlayer.Grounded = true;
			}
			ActualCoolDown += Time.unscaledDeltaTime;
			if (ActualCoolDown >= CoolDown) {
				UnSlowMo ();
				ActualCoolDown = 0;
			}
		}
	}

	//Melakukan Slow Motion
	public void SlowMo()
	{
		ThePlayer.Grounded = true;
		ThePlayer.ActualJumpCoolDown = ThePlayer.JumpCoolDown;
		ThePlayer.Anim.SetBool ("Jump", false);
		ThePlayer.Anim.SetBool ("Slide", false);
		Time.timeScale = SlowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.05f;
		Slow = true;
		TheTimer.gameObject.SetActive (true);
	}

	//Timescale Kembali normal
	public void UnSlowMo()
	{
		ActualCoolDown = 0;
		Time.timeScale = 1;
		Slow = false;
		TheTimer.TimeLeft = 0f;
		TheTimer.gameObject.SetActive (false);
	}
}
