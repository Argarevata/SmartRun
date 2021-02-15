using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public GameObject ThePlayer;

	// Use this for initialization
	void Start () {
		Instantiate (ThePlayer, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
