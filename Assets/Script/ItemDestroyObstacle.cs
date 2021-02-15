using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyObstacle : MonoBehaviour {

	//Jika kena dengan obstacle, obstacle dihancurkan agar tidak tumpang tindih
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Obstacle") {
			Destroy (other.gameObject);
		}
	}
}
