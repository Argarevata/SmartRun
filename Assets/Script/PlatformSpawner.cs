using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

	//Digunakan untuk generate platform agar gameplay menjadi endless
	//Platform yang digenerate akan ada beberapa jenis dan akan di random
	//Setiap beberapa detik platform baru akan di-generate

	//Jenis jenis platform yang ingin digenerate
	public GameObject[] Platforms;
	public GameObject Gate;

	[SerializeField]
	public float CoolDown = 5f;
	[SerializeField]
	private float ActualCoolDown=5f;

	private PlayerController ThePlayer;

	public Rigidbody2D MyBody;
	private bool createGate;

	// Use this for initialization
	void Start () {
		MyBody = GetComponent<Rigidbody2D> ();
		ThePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//ActualCoolDown += Time.deltaTime;
		//if (ActualCoolDown >= CoolDown) {
		//	int num = Random.Range (0, Platforms.Length);
		//	Instantiate (Platforms [num], transform.position, transform.rotation);
		//	ActualCoolDown = 0;
		//}

		//MyBody.velocity = new Vector2 (ThePlayer.Speed, MyBody.velocity.y);
		MyBody.transform.position = new Vector2(ThePlayer.transform.position.x + 26f, MyBody.transform.position.y);

		//if (ThePlayer.NormalSpeed > 8 && ThePlayer.NormalSpeed <=10) {
		//	CoolDown = 3f;
		//}
		//else if (ThePlayer.NormalSpeed > 10 && ThePlayer.NormalSpeed <=12) {
		//	CoolDown = 2f;
		//}
		//else if (ThePlayer.NormalSpeed >15) {
		//	CoolDown = 1f;
		//}
			
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "PlatformEnd") {
			if (ThePlayer.TheScore.MyScore <= 100)
			{
				Instantiate(Platforms[0], transform.position, transform.rotation);
			}
			else
			{
				if (!createGate)
				{
					Instantiate(Gate, transform.position, transform.rotation);
					createGate = true;
				}
				Instantiate(Platforms[1], transform.position, transform.rotation);
			}
		}
	}
}
