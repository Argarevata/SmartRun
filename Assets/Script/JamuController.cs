using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamuController : MonoBehaviour {

	//Digunakan untuk fungsi dari Jamu yaitu menambah health player

	public PlayerController ThePlayer;
	//Berapa besar health yang ingin ditambahkan
	public int PlusHealth;
	public GameObject Particles;

	public SFXController TheSFX;

	// Use this for initialization
	void Start () {
		ThePlayer = FindObjectOfType<PlayerController> ();
		TheSFX = FindObjectOfType<SFXController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Jika bersentuhan dengan player atau destroyer
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			TheSFX.PlayJamuSFX ();
			ThePlayer.Health += PlusHealth;
			transform.rotation = Quaternion.Euler (-90, 0, 0);
			//menampilkan partikel
			Instantiate (Particles, transform.position, transform.rotation);
			Destroy (gameObject);
		} else if (other.tag == "Destroyer") {
			Destroy (gameObject);
		} else if (other.tag == "Obstacle") {
			Destroy (other.gameObject);
		}
	}

	//Jika bertabrakan dengan player atau destroyer
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			TheSFX.PlayJamuSFX ();
			ThePlayer.Health += PlusHealth;
			transform.rotation = Quaternion.Euler (-90, 0, 0);
			//menampilkan partikel
			Instantiate (Particles, transform.position, transform.rotation);
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Destroyer") {
			Destroy (gameObject);
		} else if (other.gameObject.tag == "Obstacle") {
			Destroy (other.gameObject);
		}
	}
}
