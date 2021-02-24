using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public SpriteRenderer Target;
	public PlayerController ThePlayer;

	public float greenColor = 1;
	public float redColor = 0;

	public SFXController TheSFX;
	public int MinusHealth;
	public GameObject particlesDamagePlayer;
	public GameObject particlesKilled;
	public bool clicked;
	public bool clickable;

	public SuperMode theSuper;
	public AudioSource BuzzSFX;

	[SerializeField]
	private float deadCoolDown;
	private float actualDeadCoolDown;

	// Use this for initialization
	void Start () {
		Target.color = new Color (0f, 1f, 0f, 1f);
		ThePlayer = FindObjectOfType<PlayerController> ();
		actualDeadCoolDown = 0;
		clicked = false;
		TheSFX = FindObjectOfType<SFXController> ();
		if (ThePlayer.Endless) {
			theSuper = FindObjectOfType<SuperMode> ();
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x >= ThePlayer.transform.position.x) {
			redColor = (9 / (transform.position.x - ThePlayer.transform.position.x));

			greenColor = 2 - redColor;
			
			Target.color = new Color (redColor, greenColor, 0f, 1f);
		}
		else{
			Target.color = new Color (1f, 0f, 0f, 1f);
		}

		if ((transform.position.x - ThePlayer.transform.position.x) <= 18) {
			transform.position = Vector2.MoveTowards (transform.position, ThePlayer.transform.position, 2 * Time.deltaTime);
			if (BuzzSFX.isPlaying == false) {
				BuzzSFX.Play ();
			}
		}
			

		if ((transform.position.x - ThePlayer.transform.position.x) <= 0) {
			transform.position = Vector2.MoveTowards (transform.position, ThePlayer.transform.position, 45 * Time.deltaTime);
		}

		if ((transform.position.x - ThePlayer.transform.position.x) <= -5) {
			Destroy (gameObject);
		}

		if (clicked == true) {
			ThePlayer.transform.position = Vector2.MoveTowards (ThePlayer.transform.position, transform.position, 30 * Time.deltaTime);
			actualDeadCoolDown += Time.deltaTime;
			Target.gameObject.SetActive (false);
		}

		if (actualDeadCoolDown >= deadCoolDown) {
			Destroy (gameObject);
			Instantiate (particlesKilled, transform.position, transform.rotation);
		}

		if (ThePlayer.SuperMode) {
			Destroy (gameObject);
		}
		
	}

	//Jika terkena player
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && clicked == false) {
			TheSFX.PlayDamagedSFX ();
			if (ThePlayer.SuperMode == false) {
				ThePlayer.Health -= MinusHealth;
			}
			Instantiate (particlesDamagePlayer, transform.position, transform.rotation);
			Destroy (gameObject);
		}

		if (other.tag == "Player" && clicked == true) {
			Target.gameObject.SetActive (false);
			TheSFX.PlayJumpSFX ();
			if (ThePlayer.Endless) {
				theSuper.EnergyBar.value += 20;
			}
			Instantiate (particlesKilled, transform.position, transform.rotation);
			ThePlayer.TheScore.MyScore += 10;
			Destroy (gameObject);
		}

		if (other.tag == "Destroyer") {
			Destroy (gameObject);
		}
	}

	public void OnMouseDown()
	{
		Time.timeScale = 1f;
		clicked = true;
	}
}
