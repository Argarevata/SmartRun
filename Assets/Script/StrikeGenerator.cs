using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikeGenerator : MonoBehaviour {

	//Digunakan untuk generate pertanyaan random yang akan muncul setelah player mengambil strike amulet

	//Roket yang akan muncul
	public GameObject Rocket;

	//Posisi munculnya roket
	public GameObject High;
	public GameObject Low;
	public GameObject SpawnerPos;

	public GameObject BadThumbs, BadThumbsLandscape;
	public GameObject GoodThumbs, GoodThumbsLandscape;

	private PlayerController ThePlayer;
	private ObstacleSpawner TheObstacleSpawner;
	public Rigidbody2D MyBody;
	public GameObject TheDestroyer;
	public TimeManager TheTime;

	//Kumpulan Pertanyaan dan jawaban yang akan di-random
	public string[] Questions;
	public string[] RightAnswer;
	public string[] WrongAnswer;
	public GameObject Box;

	//Pertanyaan dan jawaban yang sudah dirandom dan akan muncul [Portrait]
	public Text TheQuestion;
	public Text TheRightAnswer;
	public Text TheWrongAnswer;
	public GameObject TheWarning;
	public Text JumpTitle;
	public Text SlideTitle;

	//Pertanyaan dan jawaban yang sudah dirandom dan akan muncul [Landscape]
	public Text TheQuestionLandscape;
	public Text TheRightAnswerLandscape;
	public Text TheWrongAnswerLandscape;
	public GameObject TheWarningLandscape;
	public Text JumpTitleLandscape;
	public Text SlideTitleLandscape;

	//Info yang akan disable
	public InfoController TheInfo;
	public InfoController TheInfoLandscape;
	public bool TheTriple;

	public bool DisablePause;

	[SerializeField]
	private int lastQuestion;

	// Use this for initialization
	void Start () {
		TheDestroyer.SetActive (false);
		MyBody = GetComponent<Rigidbody2D> ();
		ThePlayer = FindObjectOfType<PlayerController> ();
		TheObstacleSpawner = FindObjectOfType<ObstacleSpawner> ();
		TheTime = FindObjectOfType<TimeManager> ();
		//TheInfo = FindObjectOfType<InfoController> ();
		TheTriple = false;
		DisablePause = false;
		lastQuestion = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Jika player terkena Strike Amulet maka akan generate Strike dan question akan muncul
		if (ThePlayer.Strike > 0) {
			if (ThePlayer.Strike == 1) {
				CreateStrike ();
				ThePlayer.Strike = 0;
			} else if (ThePlayer.Strike == 3) {
				//Ini jika player trekena Star amulet. Prinsip sama seprti strike amulet namun langsung menampilkan 3 pertanyaan berturut2
				CreateTripleStrike ();
				TheTriple = true;
				ThePlayer.Strike = 0;
			}
		}
		//Generator akan terus bergerak mengikuti player
		MyBody.transform.position =  new Vector3(ThePlayer.transform.position.x+15, MyBody.transform.position.y, MyBody.transform.position.z);
	}

	//Menampilkan pertanyaan 
	void CreateStrike()
	{
		//Random pertanyaan yang akan muncul diambil dari info yang sudah pernah muncul
		int no = Random.Range (0, TheInfo.OutInfos.Length);

		if (no == lastQuestion) {
			do {
				no = Random.Range (0, TheInfo.OutInfos.Length);
			} while(no == lastQuestion);
		}

		lastQuestion = no;

		int randQuestion = TheInfo.OutInfos [no];

		TheInfo.StopCoroutine ("Hide");

		//menonaktifkan info agar box kosong[Portrait]
		for (int i = 0; i < TheInfo.Info.Length; i++) {
			TheInfo.Info [i].SetActive (false);
		}
		TheInfo.Face.SetActive (false);

		//Menerapkan pertanyaan dan jawaban yang sudah terpilih dari random di atas[Portrait]
		TheQuestion.text = Questions [randQuestion];
		TheRightAnswer.text = RightAnswer [randQuestion];
		TheWrongAnswer.text = WrongAnswer [randQuestion];

		//Menampilkan pertanyaan dan jawaban yang sudah terpilih dari random di atas
		if (PlayerPrefs.GetInt ("Orientation") == 1) {
			TheQuestion.gameObject.SetActive (true);
			TheRightAnswer.gameObject.SetActive (true);
			TheWrongAnswer.gameObject.SetActive (true);
			JumpTitle.gameObject.SetActive (true);
			SlideTitle.gameObject.SetActive (true);
			Box.SetActive (true);
		} else {
			
		}

		//Random posisi jawaban [Kiri = jump, Kanan = Slide]
		int randPos = Random.Range (0, 100);

		if (randPos % 2 == 0) {
			if (PlayerPrefs.GetInt ("Orientation") == 1) {
				TheRightAnswer.alignment = TextAnchor.MiddleLeft;
				TheWrongAnswer.alignment = TextAnchor.MiddleRight;
			} else {
				TheRightAnswerLandscape.alignment = TextAnchor.MiddleLeft;
				TheWrongAnswerLandscape.alignment = TextAnchor.MiddleRight;
			}
		} else {
			if (PlayerPrefs.GetInt ("Orientation") == 1) {
				TheRightAnswer.alignment = TextAnchor.MiddleRight;
				TheWrongAnswer.alignment = TextAnchor.MiddleLeft;
			} else {

			}
		}

		ThePlayer.Strike = 0;
		//membersihkan obstacles sekitar player
		TheDestroyer.SetActive (true);
		if (TheTriple == false) {
			StartCoroutine ("ClearPath");
		}

		//Menentukan tempat posisi roket akan muncul, dari bawah atau tas
		if (randPos  % 2 == 0) {
			SpawnerPos = Low;
		} else {
			SpawnerPos = High;
		}

		//melakukan slow mo selama beberapa detik
		TheTime.SlowMo ();
		//Generate rocket
		Instantiate (Rocket, SpawnerPos.transform.position, SpawnerPos.transform.rotation);

	}

	//Sama seperti Yang atas namun beda sedikit.
	void TripleStrike()
	{
		int no = Random.Range (0, TheInfo.OutInfos.Length);
		int randQuestion = TheInfo.OutInfos [no];

		TheInfo.Face.SetActive (false);
		TheInfo.StopCoroutine ("Hide");

		for (int i = 0; i < TheInfo.Info.Length; i++) {
			TheInfo.Info [i].SetActive (false);
		}


		TheQuestion.text = Questions [randQuestion];
		TheRightAnswer.text = RightAnswer [randQuestion];
		TheWrongAnswer.text = WrongAnswer [randQuestion];


		if (PlayerPrefs.GetInt ("Orientation") == 1) {
			TheQuestion.gameObject.SetActive (true);
			TheRightAnswer.gameObject.SetActive (true);
			TheWrongAnswer.gameObject.SetActive (true);
			JumpTitle.gameObject.SetActive (true);
			SlideTitle.gameObject.SetActive (true);
			Box.SetActive (true);
		} else {

		}

		int randPos = Random.Range (0, 100);

		if (randPos % 2 == 0) {
			if (PlayerPrefs.GetInt ("Orientation") == 1) {
				TheRightAnswer.alignment = TextAnchor.MiddleLeft;
				TheWrongAnswer.alignment = TextAnchor.MiddleRight;
			} else {
				TheRightAnswerLandscape.alignment = TextAnchor.MiddleLeft;
				TheWrongAnswerLandscape.alignment = TextAnchor.MiddleRight;
			}
		} else {
			if (PlayerPrefs.GetInt ("Orientation") == 1) {
				TheRightAnswer.alignment = TextAnchor.MiddleRight;
				TheWrongAnswer.alignment = TextAnchor.MiddleLeft;
			} else {

			}
		}

		ThePlayer.Strike = 0;
		TheDestroyer.SetActive (true);
		if (randPos  % 2 == 0) {
			SpawnerPos = Low;
		} else {
			SpawnerPos = High;
		}
			
		TheTime.SlowMo ();
		Instantiate (Rocket, SpawnerPos.transform.position, SpawnerPos.transform.rotation);

	}

	void CreateTripleStrike()
	{
		StartCoroutine ("Warning");
	}

	//membersihkan obstacle sekitar player
	public IEnumerator ClearPath()
	{
		yield return new WaitForSeconds (0.01f);
		TheDestroyer.SetActive (false);
	}

	//triple strike[Star amulet]
	public IEnumerator Warning()
	{
		DisablePause = true;
		if (PlayerPrefs.GetInt ("Orientation") == 1) {
			TheWarning.SetActive (true);
		} else {
			TheWarningLandscape.SetActive (true);
		}
		TheDestroyer.SetActive (true);

		TripleStrike ();
		yield return new WaitForSecondsRealtime (5.8f);
		ThePlayer.transform.position = new Vector2 (ThePlayer.transform.position.x, 0.09f);

		TripleStrike ();
		yield return new WaitForSecondsRealtime (5.8f);
		ThePlayer.transform.position = new Vector2 (ThePlayer.transform.position.x, 0.09f);

		TripleStrike ();
		yield return new WaitForSecondsRealtime (2);
		TheDestroyer.SetActive (false);

		TheTriple = false;
		TheWarning.SetActive (false);
		DisablePause = false;
	}
}
