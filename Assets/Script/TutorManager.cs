using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorManager : MonoBehaviour {

	//Mengatur Tutorial Screen

	public GameObject[] Tutor;
	public PlayerController ThePlayer;
	public bool CoolDown = true;
	private SuperMode theSuper;

	// Use this for initialization
	void Start () {
		StartCoroutine ("ShowTutor1");
		ThePlayer = FindObjectOfType<PlayerController> ();
		theSuper = FindObjectOfType<SuperMode> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Mengatur Health agar tidak 0
		if (ThePlayer.Health <= 30) {
			ThePlayer.Health = 100;
		}
	}

	public IEnumerator ShowTutor1()
	{
		//Mengatur tutorial board yg muncul
		yield return new WaitForSeconds (3);
		Tutor [0].SetActive (false);
		yield return new WaitForSeconds (0.5f);
		Tutor [1].SetActive (true);
		print ("1");

		yield return new WaitForSeconds (7);
		Tutor [1].SetActive (false);
		yield return new WaitForSeconds (0.5f);
		Tutor [2].SetActive (true);
		print ("2");

		yield return new WaitForSeconds (5);
		Tutor [2].SetActive (false);
		yield return new WaitForSeconds (0.5f);
		Tutor [3].SetActive (true);
		print ("3");

		yield return new WaitForSeconds (8);
		Tutor [3].SetActive (false);
		yield return new WaitForSeconds (0.5f);
		Tutor [4].SetActive (true);
		print ("4");

		yield return new WaitForSecondsRealtime (3);
		Tutor [4].SetActive (false);
		yield return new WaitForSecondsRealtime (0.5f);
		Tutor [5].SetActive (true);
		print ("5");

		yield return new WaitForSeconds (5);
		Tutor [5].SetActive (false);
		yield return new WaitForSeconds (0.5f);
		Tutor [6].SetActive (true);
		print ("6");

		yield return new WaitForSecondsRealtime (4f);
		Tutor [6].SetActive (false);
		yield return new WaitForSeconds (0.5f);
		Tutor [7].SetActive (true);

		yield return new WaitForSecondsRealtime (5);
		Tutor [7].SetActive (false);
		yield return new WaitForSeconds (0.5f);
		Tutor [8].SetActive (true);

		yield return new WaitForSecondsRealtime (3);
		Tutor [8].SetActive (false);

	}

	void OnTriggerExit2D(Collider2D other)
	{
		//Membuat player berhenti saat bertemu obstacle
		if (other.tag == "Player" && CoolDown == true) {
			ThePlayer.Health = 100;
			Time.timeScale = 0;
			ThePlayer.Grounded = true;
			ThePlayer.ActualSlideCoolDown = ThePlayer.SlideCoolDown;
			ThePlayer.ActualJumpCoolDown = ThePlayer.JumpCoolDown;
			ThePlayer.transform.position = new Vector2 (ThePlayer.transform.position.x, 0.77f);
			ThePlayer.MyBody.velocity = new Vector2 (ThePlayer.MyBody.velocity.x, 0);
			CoolDown = false;
			StartCoroutine ("CoolDownTrue");
		}
		
	}

	public IEnumerator CoolDownTrue()
	{
		yield return new WaitForSeconds (2);
		CoolDown = true;
	}
}
