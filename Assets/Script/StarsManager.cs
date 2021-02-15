using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsManager : MonoBehaviour {

	public string PlayerPrefsName;
	//Pakai "StarLevel2"

	private int StarsGet;

	public Image[] StarsImage;
	// Use this for initialization
	void Start () {
		StarsGet = PlayerPrefs.GetInt (PlayerPrefsName);

		for (int i = 0; i < 3; i++) {
			StarsImage [i].color = new Color (0.25f, 0.14f, 0.02f, 255);
		}

		for (int i = 0; i < StarsGet; i++) {
			StarsImage [i].color = new Color (1, 1, 0, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
