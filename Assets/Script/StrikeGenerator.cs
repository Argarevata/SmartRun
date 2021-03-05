using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikeGenerator : MonoBehaviour {

	//Digunakan untuk generate pertanyaan random yang akan muncul setelah player mengambil strike amulet

	//Roket yang akan muncul
	public GameObject Rocket;
	public RocketTimer theRocketTimer;

	//Posisi munculnya roket
	public GameObject High;
	public GameObject Low;
	public GameObject SpawnerPos;

	public GameObject spawnedRocket1, spawnedRocket2;

	public GameObject BadThumbs, BadThumbsLandscape;
	public Text correctAnswer;
	public GameObject GoodThumbs, GoodThumbsLandscape;

	private PlayerController ThePlayer;
	private ObstacleSpawner TheObstacleSpawner;
	public Rigidbody2D MyBody;
	public GameObject TheDestroyer;
	public TimeManager TheTime;

	//Kumpulan Pertanyaan dan jawaban yang akan di-random
	public string[] Questions;
	public string[] RightAnswer;
	public string[] WrongAnswer, WrongAnswer2, WrongAnswer3;
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
	public GameObject buttonGroup;
	public Text textBtn1, textBtn2, textBtn3, textBtn4;

	[SerializeField]
	private int lastQuestion;

	public RectTransform[] questionButtons, questionBtnPos;
	public int[] filledPos;
	public ScoreController TheScore;

	public bool sequential, totallyRandom;
	public int sequentNumber;

	// Use this for initialization
	void Start () {
		sequentNumber = 0;
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
		int randQuestion = 0;
		TheInfo.IsShowingInfo = true;

		buttonGroup.SetActive(true);

		if (!sequential && !totallyRandom)
		{
			//Random pertanyaan yang akan muncul diambil dari info yang sudah pernah muncul
			int no = Random.Range(0, TheInfo.OutInfos.Length);

			if (no == lastQuestion)
			{
				do
				{
					no = Random.Range(0, TheInfo.OutInfos.Length);
				} while (no == lastQuestion);
			}

			lastQuestion = no;

			randQuestion = TheInfo.OutInfos[no];
		}
		else if (sequential)
		{
			randQuestion = sequentNumber;
			sequentNumber++;
			if (sequentNumber > Questions.Length - 1)
			{
				sequentNumber = 0;
				print("ALL QUESTION ANSWERED");
			}
		}
		else if (totallyRandom)
		{
			randQuestion = Random.Range(0, Questions.Length);
		}

		TheInfo.StopCoroutine ("Hide");

		//menonaktifkan info agar box kosong[Portrait]
		for (int i = 0; i < TheInfo.Info.Length; i++) {
			TheInfo.Info [i].SetActive (false);
		}
		TheInfo.Face.SetActive (false);

		//Menerapkan pertanyaan dan jawaban yang sudah terpilih dari random di atas[Portrait]
		TheQuestion.text = Questions [randQuestion];
		//TheRightAnswer.text = RightAnswer [randQuestion];
		//TheWrongAnswer.text = WrongAnswer [randQuestion];
		TheRightAnswer.text = "";
		TheWrongAnswer.text = "";

		textBtn1.text = RightAnswer[randQuestion];
		correctAnswer.text = "Jawaban yang benar adalah\r\n" + RightAnswer[randQuestion];
		textBtn2.text = WrongAnswer[randQuestion];
		textBtn3.text = WrongAnswer2[randQuestion];
		textBtn4.text = WrongAnswer3[randQuestion];

		ScrambleQuestionBtnPos();
		theRocketTimer.gameObject.SetActive(true);


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
		//TheTime.SlowMo ();
		Time.timeScale = 0;
		//Generate rocket
		Vector2 spawnPos = new Vector2(High.transform.position.x, ThePlayer.transform.position.y+3);
		spawnedRocket1 = Instantiate (Rocket, spawnPos, SpawnerPos.transform.rotation);
		//spawnedRocket2 = Instantiate(Rocket, Low.transform.position, SpawnerPos.transform.rotation);

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

	public void Answer(bool status)
	{
		if (status)
		{
			//benar
			TheScore.MyScore += 20;
			if (PlayerPrefs.GetInt("Orientation") == 1)
			{
				GoodThumbs.SetActive(true);
			}
			else if (PlayerPrefs.GetInt("Orientation") == 0)
			{
				GoodThumbsLandscape.SetActive(true);
			}
			Destroy(spawnedRocket1);
			//Destroy(spawnedRocket2);
		}
		else
		{
			//salah
		}
		theRocketTimer.gameObject.SetActive(false);
		TheQuestion.gameObject.SetActive(false);
		Box.SetActive(false);
		TheInfo.IsShowingInfo = false;
		Time.timeScale = 1;
	}

	public void ScrambleQuestionBtnPos()
	{
		for (int i = 0; i < 4; i++)
		{
			filledPos[i] = -1;
		}

		for (int i = 0; i < 4; i++)
		{
			int x = 0;
			do
			{
				x = Random.Range(0, 4);
			}
			while (x == filledPos[0] || x == filledPos[1] || x == filledPos[2] || x == filledPos[3]);
			questionButtons[i].position = questionBtnPos[x].position;
			filledPos[i] = x;
		}
	}
}
