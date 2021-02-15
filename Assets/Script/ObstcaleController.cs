using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstcaleController : MonoBehaviour {

	//Digunakan untuk mengatur obstacle bila terkena player[Player health berkurang, obstacle hancur, muncul partikel]

	public int MinusHealth;
	public PlayerController ThePlayer;
	public GameObject particles;
	public InfoController TheInfo;
	public TimeManager TheTime;
	public SFXController TheSFX;

	// Use this for initialization
	void Start () {
		ThePlayer = FindObjectOfType<PlayerController> ();
		TheInfo = FindObjectOfType<InfoController> ();
		TheTime = FindObjectOfType<TimeManager> ();
		TheSFX = FindObjectOfType<SFXController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Jika sedang ada info dan ini adalah endless maka langsung hilang
		if (TheInfo.IsShowingInfo == true && ThePlayer.Endless == true) {
			Instantiate (particles, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		//Jika sedang slow mo dan ini adalah endless, maka akan langsung hilang
		if (TheTime.Slow == true && ThePlayer.Endless == true) {
			Instantiate (particles, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

	//Jika terkena player
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			TheSFX.PlayDamagedSFX ();
			if (ThePlayer.SuperMode == false) {
				ThePlayer.Health -= MinusHealth;
			}
			Instantiate (particles, transform.position, transform.rotation);
			Destroy (gameObject);
		}

		if (other.tag == "Destroyer") {
			Destroy (gameObject);
		}
	}
}
