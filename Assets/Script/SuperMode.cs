using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMode : MonoBehaviour {

	public Slider EnergyBar;
	private float CoolDown;
	private float ActualCoolDown;

	// Use this for initialization
	void Start () {
		EnergyBar.value = 0;
		CoolDown = 1;
	}
	
	// Update is called once per frame
	void Update () {
		ActualCoolDown += Time.deltaTime;

		if (ActualCoolDown >= CoolDown) {
			ActualCoolDown = 0;
			EnergyBar.value += 2;
		}


	}
}
