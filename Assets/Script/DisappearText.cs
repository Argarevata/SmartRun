using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearText : MonoBehaviour {

	//Digunakan untuk menghilangkan teks pertanyaan setelah beberapa saat muncul

	public PlayerController ThePlayer;
	public StrikeGenerator TheStrikeGenerator;

	// Use this for initialization
	void Start () {
		ThePlayer = FindObjectOfType<PlayerController> ();	
		TheStrikeGenerator = FindObjectOfType<StrikeGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isActiveAndEnabled) {
			StartCoroutine ("Disappear");
		}

		if (ThePlayer.Grounded == true) {
			StopCoroutine ("Disappear");
			StartCoroutine ("Disappear2");
		}
	}

	//Setelah teks muncul maka 6 detik kemudian dihilangkan
	public IEnumerator Disappear()
	{
		yield return new WaitForSecondsRealtime (6);
		TheStrikeGenerator.Box.SetActive (false);
		gameObject.SetActive (false);
	}

	//Setelah teks muncul maka 1.5 detik kemudian dihilangkan
	public IEnumerator Disappear2()
	{
		yield return new WaitForSeconds (1.5f);
		TheStrikeGenerator.Box.SetActive (false);
		gameObject.SetActive (false);
	}
}
