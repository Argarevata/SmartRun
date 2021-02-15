using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	//Digunakan untuk menghapus semua hal yang sudah tidak dibutuhkan pada game agar game tidak berat

	public bool IsInStrikeGenerator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Jika Destroyer memasuki collider lain maka benda tersebut akan dihancurkan, kecuali benda tersebut roket
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (IsInStrikeGenerator == true) {
			if (other.tag != "Rocket" && other.tag != "Parallax") {
				Destroy (other.gameObject);
			}
		} else {
			if (other.tag != "Rocket") {
				Destroy (other.gameObject);
			}
		}
	}

	//Jika Destroyer menabrak collider lain maka benda tersebut akan dihancurkan, kecuali benda tersebut roket
	public void OnCollisionEnter2D(Collision2D other)
	{
		if (IsInStrikeGenerator == true) {
			if (other.gameObject.tag != "Rocket" && other.gameObject.tag != "Parallax") {
				Destroy (other.gameObject);
			}
		} else {
			if (other.gameObject.tag != "Rocket") {
				Destroy (other.gameObject);
			}
		}
	}
}
