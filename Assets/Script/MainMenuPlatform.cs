using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlatform : MonoBehaviour {

	// Berfungsi untuk mengatur gerakan platform pada Main Menu, bergerak ke kiri dan kemabli ke kanan setelah mentok

	public GameObject StartPoint;
	public int Speed;

	public Rigidbody2D MyBody;

	// Use this for initialization
	void Start () {
		MyBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		MyBody.velocity = new Vector2 (-Speed, 0);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Portal") {
			MyBody.transform.position = new Vector2 (StartPoint.transform.position.x, StartPoint.transform.position.y);
		}
	}
}
