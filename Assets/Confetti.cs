using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Confetti : MonoBehaviour {

	[SerializeField]
	private Rigidbody2D myBody;

	[SerializeField]
	private float coolDown = 0.3f;
	[SerializeField]
	private float destroyCoolDown=8f;
	[SerializeField]
	private float actualCoolDown=0f;

	// Use this for initialization
	void Start () {
		//myBody = GetComponent<Rigidbody2D> ();
		int forceUp = Random.Range (900000, 1300000);
		int forceRight = Random.Range (-400000, 400000);
		myBody.AddForce (transform.up * forceUp * 300);
		myBody.AddForce (transform.right * forceRight * 150);
	}
	
	// Update is called once per frame
	void Update () {
		actualCoolDown += Time.unscaledDeltaTime;

		if (actualCoolDown <= coolDown) {
			//int forceUp = Random.Range (500000, 1500000);
			//int forceRight = Random.Range (-1000000, 1000000);
			//myBody.AddForce (transform.up * forceUp);
			//myBody.AddForce (transform.right * forceRight);
		} else {
			int mass = Random.Range (250, 401);
			int gravity = Random.Range (250, 401);
			myBody.mass = mass;
			myBody.gravityScale = gravity;
		}

		if (actualCoolDown >= destroyCoolDown) {
			Destroy (gameObject);
		}
	}
}
