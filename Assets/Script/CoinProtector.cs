using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinProtector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print ("START");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Obstacle") {
			Destroy (other.gameObject);
		}
	}
		
}
