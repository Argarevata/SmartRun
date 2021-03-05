using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour {
	// ButtonFunction digunakan untuk mengatur waktu Timescale setiap kali game berjalan
	// Mengatur orientasi layar dan seeting swipe atau button
	// Mengatur fungsi dari button pada gameplay seperi retry, pause dan quit

	public bool IsDead;
	public bool IsNotGameplay;
	public Canvas PortraitCanvas;

	public Camera LandscapeCamera;
	public Camera PortraitCamera;
	public GameObject ControlSettings;
	public GameObject OrientationSettings;
	public GameObject Buttons;
	public GameObject ButtonsLandscape;

	public bool inMainMenu;
	public Image cowokImg, cewekImg;

	// Use this for initialization
	void Start () {
		//Mereset timescale setiap game berjalam
		Time.timeScale = 1;

		//Cek jika player sudah mati dan akan respawn maka menggunakan fungsi ini
		if (IsDead == true) {
			Application.LoadLevel (PlayerPrefs.GetString("Level"));
		}

		if (IsNotGameplay == false) {
			//Mengatur Orientasi dan Setting control apakah portrait atau landscape . Apakah Butons atau swipe
			if (PlayerPrefs.GetInt ("Orientation") == 1 || PlayerPrefs.GetInt ("Orientation") == null) {
				Screen.orientation = ScreenOrientation.Portrait;
				if (IsNotGameplay == false) {
					PortraitCamera.gameObject.SetActive (true);
					PortraitCanvas.gameObject.SetActive (true);
				}
				if (PlayerPrefs.GetInt ("Control") == 0) {
					Buttons.SetActive (true);
				} else {
					Buttons.SetActive (false);
				}
			} else {
				Screen.orientation = ScreenOrientation.Landscape;
				PortraitCamera.gameObject.SetActive (false);
				PortraitCanvas.gameObject.SetActive (false);
				if (PlayerPrefs.GetInt ("Control") == 0) {
					ButtonsLandscape.SetActive (true);
				} else {
					ButtonsLandscape.SetActive (false);
				}
			}
		}

		if (inMainMenu)
		{
			if (PlayerPrefs.GetInt("gender") == 0)
			{
				PlayerPrefs.SetInt("gender", 1);
			}

			if (PlayerPrefs.GetInt("gender") == 1)
			{
				cewekImg.color = new Color(0.6f, 0.6f, 0.6f, 1);
				cowokImg.color = new Color(1, 1, 1, 1);
			}
			else if (PlayerPrefs.GetInt("gender") == 2)
			{
				cowokImg.color = new Color(0.6f, 0.6f, 0.6f, 1);
				cewekImg.color = new Color(1, 1, 1, 1);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {


	}

	//tombol untuk melakukan retry
	public void Retry()
	{
		Application.LoadLevel ("DIE");
	}

	//tombol untuk setting layar menjadi landscape
	public void Landscape()
	{
		//0 is Landscape 1 is Portrait
		PlayerPrefs.SetInt ("Orientation", 0);
		OrientationSettings.SetActive (false);
		ControlSettings.SetActive (true);
	}

	//tombol untuk setting layar menjadi Portrait
	public void Portrait()
	{
		//0 is Landscape 1 is Portrait
		PlayerPrefs.SetInt ("Orientation", 1);
		OrientationSettings.SetActive (false);
		ControlSettings.SetActive (true);
	}
		
	//Mengatur untuk setting control menjadi swipe
	public void SetControlSwipe()
	{
		//1 is Swipe 0 is Button
		PlayerPrefs.SetInt ("Control", 1);
		Application.LoadLevel ("Endless");
	}

	//setting control menjadi Buttons
	public void SetControlButton()
	{
		//1 is Swipe 0 is Button
		PlayerPrefs.SetInt ("Control", 0);
		Application.LoadLevel ("Endless");
	}

	public void GoToMainMenu(){
		Application.LoadLevel ("MainMenu");
	}

	public void GoToLevel(string x)
	{
		Application.LoadLevel (x);
	}

	public void ChooseGender(int x)
	{
		// 1 = cowo || 2 = cewek
		PlayerPrefs.SetInt("gender", x);

		if (PlayerPrefs.GetInt("gender") == 1)
		{
			cewekImg.color = new Color(0.6f, 0.6f, 0.6f, 1);
			cowokImg.color = new Color(1, 1, 1, 1);
		}
		else if (PlayerPrefs.GetInt("gender") == 2)
		{
			cowokImg.color = new Color(0.6f, 0.6f, 0.6f, 1);
			cewekImg.color = new Color(1, 1, 1, 1);
		}
	}
}
