using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour {

	//Digunakan untuk mengatur informasi apa yang akan muncul

	public GameObject[] Info;
	public GameObject Box;
	public bool IsShowingInfo;
	public GameObject Face;

	public int SumNum;
	public int[] OutInfos;

	[SerializeField]
	private int LastInfoShown;

	private PlayerController thePlayer;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt ("Orientation", 0);
		Box.SetActive(false);
		SumNum = 0;
		LastInfoShown = 0;
		thePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Function untuk melakukan random dan menampilkan info yang akan muncul
	public void ShowInfo()
	{
		StopCoroutine ("Hide");
		Face.SetActive (true);
		IsShowingInfo = true;

		int RandomNum = Random.Range (1, Info.Length);

		if (RandomNum == LastInfoShown) {
			do {
				RandomNum = Random.Range (1, Info.Length);
			} while(RandomNum == LastInfoShown);
		}
			
		LastInfoShown = RandomNum;
		if (thePlayer.Endless) {
			PlayerPrefs.SetInt (LastInfoShown.ToString (), 99);
		}

		for (int i = 0; i < Info.Length; i++) {
			Info [i].SetActive (false);
		}
		
		Box.SetActive (true);
		Info [RandomNum].SetActive (true);
		StartCoroutine ("Hide");

		if (OutInfos [0] == 0) {
			for (int i = 0; i < OutInfos.Length; i++) {
				OutInfos [i] = RandomNum;
			}
		} else {
			OutInfos [SumNum] = RandomNum;
		}
		SumNum++;
		if (SumNum >= 5) {
			SumNum = 0;
		}
	}

	//Function untuk menampilkan info terpilih, melempar parameter info yang ingin ditampilkan
	public void ShowSelectedInfo(int x)
	{
		StopCoroutine ("Hide");
		IsShowingInfo = true;
		for (int i = 0; i < Info.Length; i++) {
			Info [i].SetActive (false);
		}

		if (thePlayer.Endless) {
			PlayerPrefs.SetInt (x.ToString (), 99);
		}

		Box.SetActive (true);
		Info [x].SetActive (true);
		StartCoroutine ("Hide");
	}

	public IEnumerator Hide(){
		yield return new WaitForSeconds (4);
		Face.SetActive (false);
		IsShowingInfo = false;
		Box.SetActive (false);
		for (int i = 0; i < Info.Length; i++) {
			Info [i].SetActive (false);
		}
	}
}
