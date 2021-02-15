using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbsController : MonoBehaviour {

	//Meng-hide jempol setelah jempol muncul

	[SerializeField]
	public float CoolDown = 1f;
	[SerializeField]
	public float ActualCoolDown=0f;

	public bool Activated;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Jika jempol aktif maka timer jalan
		if (isActiveAndEnabled) {
			ActualCoolDown += Time.unscaledDeltaTime;
		}
		//jika timer sudah mencapai batas maka jempol active = false
		if (ActualCoolDown >= CoolDown) {
			Activated = false;
			ActualCoolDown = 0;
			gameObject.SetActive (false);
		}

	}
		
}
