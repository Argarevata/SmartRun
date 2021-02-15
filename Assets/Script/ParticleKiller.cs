using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour {

	//Digunakan untuk delete partikel beberapa detik setelahh tercipta [Dihapus agar tidak membebani memori]

	// Use this for initialization
	void Start () {
		StartCoroutine ("Kill");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator Kill()
	{
		yield return new WaitForSecondsRealtime (1);
		Destroy (gameObject);
	}
}
