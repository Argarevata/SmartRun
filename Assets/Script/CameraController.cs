using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	//Digunakan untuk mengatur gerakan kamera agar kamera selalu menikuti player

	public PlayerController ThePlayer;
	public Rigidbody2D MyBody;
	public float DistanceFromPlayer;
	public bool Playing;
	public bool IsMainMenu;

	// Use this for initialization
	void Start () {
		MyBody = GetComponent<Rigidbody2D> ();
		ThePlayer = FindObjectOfType<PlayerController> ();
		if (IsMainMenu == false) {
			Playing = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Kamera akan terus mengikuti player dengan posisi kamera lebih jauh sebesar "DistanceFromPlayer"
		if (Playing == true) {
			MyBody.transform.position = new Vector3 (ThePlayer.transform.position.x + DistanceFromPlayer, MyBody.transform.position.y, MyBody.transform.position.z);
		} else if (Playing == false) {
			MyBody.transform.position = new Vector3 (MyBody.transform.position.x, MyBody.transform.position.y, MyBody.transform.position.z);
		}
	}
}
