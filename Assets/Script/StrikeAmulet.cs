using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeAmulet : MonoBehaviour {

	//Digunakan untuk mengontrol Strike Amulet jika terkena player maka akan memunclkan pertanyaan

	public PlayerController ThePlayer;
	public GameObject Particles;
	public bool IsStar;
	public InfoController TheInfo;

	void Start()
	{
		ThePlayer = FindObjectOfType<PlayerController> ();
		TheInfo = FindObjectOfType<InfoController> ();
	}

	//Jika terkena player maka pertanyaan akan munucl pada script "StrikeGenerator"
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {

			if (ThePlayer.SuperMode == false) {

				if (IsStar == false) {
					ThePlayer.Strike = 1;
				} else {
					ThePlayer.Strike = 3;
				}
				Instantiate (Particles, transform.position, transform.rotation);
				ThePlayer.Grounded = true;
				ThePlayer.ActualSlideCoolDown = ThePlayer.SlideCoolDown;
				ThePlayer.transform.position = new Vector2 (ThePlayer.transform.position.x, 0.09f);
				ThePlayer.MyBody.velocity = new Vector2 (ThePlayer.MyBody.velocity.x, 0);
				ThePlayer.CanMove = false;
				Destroy (gameObject);
			} else {
				Destroy (gameObject);
				Instantiate (Particles, transform.position, transform.rotation);
			}

		} else if (other.tag == "Destroyer") {
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Obstacle") {
			Destroy (other.gameObject);
		}
	}
}
