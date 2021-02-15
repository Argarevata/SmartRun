using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketTimer : MonoBehaviour {

	//Untuk Mengatur timer lingkaran saat roket muncul

	public float TimeLeft;
	public int DeadLine;
	public Slider WheelSlider;
	public Text CounterText;
	public int Counter;

	// Use this for initialization
	void Start () {
		TimeLeft = 0;
		WheelSlider.maxValue = DeadLine;
		WheelSlider.value = TimeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		TimeLeft += Time.unscaledDeltaTime;
		CounterText.text = "" + (Mathf.RoundToInt(DeadLine - TimeLeft)); 
		WheelSlider.value = TimeLeft;

		//Jika waktu habis maka akan disable
		if (TimeLeft >= DeadLine) {
			gameObject.SetActive (false);
		}
	}
}
