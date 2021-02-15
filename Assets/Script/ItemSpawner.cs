using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

	// Digunakan untuk spawn item random pada platform
	// Item apa yang akan keluar akan di-random sebelumnya
	// Persentase item yang lebih sering keluar adalah Koin, Strike amulet dan Star amulet
	// Setiap beberapa detik item akan digenerate [Menggunakan ActualCoolDown dan CoolDown]
	// Tempat item akan keluar juga akan digenerate

	//Item yang akan dirandom
	public GameObject[] Items;
	public GameObject SelectedItem;

	//Untuk Timer 
	[SerializeField]
	private float CoolDown = 3f;
	[SerializeField]
	private float ActualCoolDown=0f;

	//Posisi Item akan dimunculkan
	public GameObject Mid;
	public GameObject High;

	private ObstacleSpawner TheObstacleSpawner;
	private PlayerController ThePlayer;
	public InfoController TheInfo;
	public Rigidbody2D MyBody;

	public int CoinCounter;
	public int CoinCounterMax;

	// Use this for initialization
	void Start () {
		MyBody = GetComponent<Rigidbody2D> ();
		ThePlayer = FindObjectOfType<PlayerController> ();
		TheObstacleSpawner = FindObjectOfType<ObstacleSpawner> ();
		TheInfo = FindObjectOfType<InfoController> ();
		CoinCounter = 0;
		CoinCounterMax = 3;
	}
	
	// Update is called once per frame
	void Update () {

			//Timer terus bertambah ,jika sudah >= waktu yg ditentukan maka akan generate item dan mulai dari 0 lagi
			ActualCoolDown += Time.deltaTime;
			if (ActualCoolDown >= CoolDown) {
				int numHeight = Random.Range (1, 4);
				float randCoolDown = Random.Range (2.5f, 4f);
				int randItem = Random.Range (0, 12);

			//Jika Coin Counter < Coin Counter Max akan menggenerate Semua item kecuali Star dan Strike Amulet
			if (CoinCounter < CoinCounterMax) {
				if (randItem >= 0 && randItem <=4) {
					SelectedItem = Items [0];
					CoinCounter++;
				} else if (randItem > 4 && randItem <= 7) {
					SelectedItem = Items [2];
					numHeight = 1;
				} else if (randItem > 7 && randItem <= 10) {
					SelectedItem = Items [3];
					numHeight = 1;
				} else if(randItem>=11){
					SelectedItem = Items [1];
					numHeight = 3;
				}
			} else if(CoinCounter>=CoinCounterMax){
				if (randItem < 9) {
					SelectedItem = Items [4];
					print ("Get Strike");
				} else if(randItem >= 9){
					SelectedItem = Items [5];
					print ("Get Star");
				}

				//Reset maka CoinCounter jadi 0 lagi
				int willWeReset = Random.Range (1, 11);
				if (willWeReset % 2 == 0) {
					CoinCounter = 0;
				
					//Konfigurasi tingkat kesulitan makin besar score makin sulit game nya
					if (ThePlayer.TheScore.MyScore <= 250) {
						CoinCounterMax = Random.Range (3, 7);
					} else if (ThePlayer.TheScore.MyScore > 250 && ThePlayer.TheScore.MyScore > 500) {
						CoinCounterMax = Random.Range (2, 5);
					} else if (ThePlayer.TheScore.MyScore > 500) {
						CoinCounterMax = Random.Range (1, 3);
					}
				}
			}

			CoolDown = randCoolDown;

			//Menentukan di mana item akan di-spawn
			if (numHeight == 3) {
				Instantiate (SelectedItem, High.transform.position, High.transform.rotation);
			} else if (numHeight == 2) {
				Instantiate (SelectedItem, Mid.transform.position, Mid.transform.rotation);
			} else {
				Instantiate (SelectedItem, transform.position, transform.rotation);
			}

			ActualCoolDown = 0;

		}

		//MyBody.velocity = new Vector2 (ThePlayer.Speed, MyBody.velocity.y);
		MyBody.transform.position = new Vector3 (ThePlayer.transform.position.x + 40, MyBody.transform.position.y, MyBody.transform.position.z);
	}
}
