using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	// Digunakan untuk menggenerate obstacle random pada platform
	// Obstacle apa yang akan keluar akan di-random sebelumnya
	// Setiap beberapa detik obstacle akan digenerate [Menggunakan ActualCoolDown dan CoolDown]
	// Tempat obstacle akan keluar sesuai dengan item yang digenerate [Jika ground obstacles maka akan di bawah jika sky maka di atas]

	//Obstacles yang akan dirandom
	public GameObject[] Obstacles;

	//Timer cool down
	[SerializeField]
	private float CoolDown = 2f;
	[SerializeField]
	private float ActualCoolDown=2f;

	public bool CreatingNow;
	public GameObject GroundSpawner;
	public GameObject SkySpawner;
	public GameObject[] BeeSpawner;

	private PlayerController ThePlayer;

	public Rigidbody2D MyBody;
	public InfoController TheInfo;

	public float RandCoolDown;
	public int Num;
	// Use this for initialization
	void Start () {
		MyBody = GetComponent<Rigidbody2D> ();
		ThePlayer = FindObjectOfType<PlayerController> ();
		CreatingNow = false;
		TheInfo = FindObjectOfType<InfoController> ();
		CoolDown = 1;
	}
	
	// Update is called once per frame
	void Update () {
		//Timer akan terus bertambah, Akan menggenerate obstacle jika waktu Actual Cooldown>= CoolDown dan Actual Cool Down kembali ke 0
		ActualCoolDown += Time.deltaTime;
		if (ActualCoolDown >= CoolDown && TheInfo.IsShowingInfo == false) {
			CreatingNow = true;
			StartCoroutine ("BackToNotCreate");

			//Mengatur apa saja obstacle yg akan keluar. Jika score makin tinggi maka obstacle yg keluar semakin beragam
			if (ThePlayer.TheScore.MyScore <= 500) {
				int num = Random.Range (0, 6);
				Num = num;
			} else if (ThePlayer.TheScore.MyScore > 500 && ThePlayer.TheScore.MyScore <= 1000) {
				int num = Random.Range (0, 10);
				Num = num;
			} else {
				int num = Random.Range (0, Obstacles.Length);
				Num = num;
			}

			//Mengatur cooldown obstacle yg akan keluar. Jika score makin tinggi maka obstacle yg keluar semakin cepat
			if (ThePlayer.TheScore.MyScore <= 250) {
				float randCoolDown = Random.Range (1f, 2.5f);
				RandCoolDown = randCoolDown;
			} else if (ThePlayer.TheScore.MyScore > 250 && ThePlayer.TheScore.MyScore <= 500) {
				float randCoolDown = Random.Range (0.8f, 1.5f);
				RandCoolDown = randCoolDown;
			} else if (ThePlayer.TheScore.MyScore > 500) {
				float randCoolDown = Random.Range (1f, 1.8f);
				RandCoolDown = randCoolDown;
			} else if (ThePlayer.TheScore.MyScore > 1500) {
				float randCoolDown = Random.Range (1.8f, 2.5f);
				RandCoolDown = randCoolDown;
			}
			CoolDown = RandCoolDown;

			//Mengatur dimana obstacle akan muncul
			if (Num == 2 || Num == 3 || Num == 7) {
				Instantiate (Obstacles [Num], SkySpawner.transform.position, SkySpawner.transform.rotation);
			} else if (Num == 4 || Num == 5 || Num == 8 || Num == 9 || Num == 12 || Num == 13) {
				Instantiate (Obstacles [Num], BeeSpawner[Random.Range(0,3)].transform.position, BeeSpawner[0].transform.rotation);
			}
			else
			{
				Instantiate (Obstacles [Num],GroundSpawner.transform.position, transform.rotation);
			}
			ActualCoolDown = 0;
		}
			
		MyBody.transform.position = new Vector3 (ThePlayer.transform.position.x + 35, MyBody.transform.position.y, MyBody.transform.position.z);
	}

	private IEnumerator BackToNotCreate()
	{
		yield return new WaitForSeconds (1.5f);
		CreatingNow = false;
	}
}
