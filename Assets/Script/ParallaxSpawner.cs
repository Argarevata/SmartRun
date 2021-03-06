﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSpawner : MonoBehaviour {

	//Digunakan untuk mengatur parallax, cloud dan background yg akan muncul pada game

	public GameObject[] Parallax;
	public GameObject Cloud;
	public GameObject CloudPos;

	public float CoolDown;
	public float ActualCoolDown;

	public float CloudCoolDown;
	public float ActualCloudCoolDown;

	public PlayerController ThePlayer;
	public Rigidbody2D MyBody;
	public float DistanceFromPlayer;

	// Use this for initialization
	void Start () {
		ThePlayer = FindObjectOfType<PlayerController> ();
		MyBody = GetComponent<Rigidbody2D> ();
		CloudCoolDown = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		//Timer akan terus bertambah. Jika sudah Actual CoolDownn >= CoolDown, maka parallax akan generate dan cooldown menjadi 0 lagi.
		ActualCoolDown += Time.deltaTime;
		ActualCloudCoolDown += Time.deltaTime;

		//untuk BackGround
		if (ActualCoolDown >= CoolDown) {
			ActualCoolDown = 0;
			switch (ThePlayer.Speed)
			{
				case 8:
					{
						CoolDown = Random.Range(1.5f, 2f);
						break;
					}
				case 10:
					{
						CoolDown = Random.Range(1.5f, 2f);
						break;
					}
				case 11:
					{
						CoolDown = Random.Range(1.5f, 2f);
						break;
					}
				case 12:
					{
						CoolDown = Random.Range(1.5f, 2f);
						break;
					}
				case 13:
					{
						CoolDown = Random.Range(1.5f, 2f);
						break;
					}
				case 14:
					{
						CoolDown = Random.Range(1.5f, 1.6f);
						break;
					}
				case 15:
					{
						CoolDown = Random.Range(1f, 1.1f);
						break;
					}
			}

			int rand = 0;
			if (ThePlayer.TheScore.MyScore <= 100)
			{
				rand = Random.Range(30, 60);
			}
			else
			{
				rand = Random.Range(0, 30);
			}
			
			Instantiate (Parallax [rand], transform.position, transform.rotation);
		}

		//untuk cloud
		if (ActualCloudCoolDown >= CloudCoolDown) {
			ActualCloudCoolDown = 0;
			Instantiate (Cloud, CloudPos.transform.position, CloudPos.transform.rotation);
		}

		MyBody.transform.position = new Vector3 (ThePlayer.transform.position.x+DistanceFromPlayer, MyBody.transform.position.y, MyBody.transform.position.z);
	}
}
