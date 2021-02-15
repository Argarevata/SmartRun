using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour {

	//Digunakan untuk kontrol swipe pada smartphone

	public bool Tap, Swipe_Down, Swipe_Up, Swipe_Right;
	private bool IsDragging = false;
	private Vector2 StartTouch, Swipe_Delta;

	// Update is called once per frame
	void Update () {
		Tap = Swipe_Down = Swipe_Up = Swipe_Right = false;

		#region Standalone Inputs
		if(Input.GetMouseButtonDown(0))
		{
			Tap=true;
			IsDragging = true;
			StartTouch = Input.mousePosition;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			IsDragging = false;
			Reset();
		}
		#endregion


		#region Mobile Inputs
		if(Input.touches.Length > 0)
		{
			if(Input.touches[0].phase == TouchPhase.Began)
			{
				Tap=true;
				IsDragging = true;
				StartTouch = Input.touches[0].position;
			}	
			else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase ==TouchPhase.Canceled)
			{
				IsDragging = false;
				Reset();
			}
		}
		#endregion

		//Calculate Touches
		Swipe_Delta = Vector2.zero;
		if (IsDragging) 
		{
			if (Input.touches.Length > 0) {
				Swipe_Delta = Input.touches [0].position - StartTouch;
			} else if (Input.GetMouseButton (0)) {
				Swipe_Delta = (Vector2)Input.mousePosition - StartTouch;
			}
		}


		//Is Crossing
		if(Swipe_Delta.magnitude > 100)
		{
			float x = Swipe_Delta.x;
			float y = Swipe_Delta.y;

			if (Mathf.Abs (x) > Mathf.Abs (y)) {
				if (x > 0) {
					Swipe_Right = true;
				}
			} else {
				if (y < 0) {
					Swipe_Down = true;
				} else {
					Swipe_Up = true;
				}
			}
		}
	}

	public void Reset(){
		StartTouch = Swipe_Delta = Vector2.zero;
		IsDragging = false;
	}

	public bool SwipeLeft{get {return Swipe_Down;}}
	public bool SwipeRight{get {return Swipe_Up;}}
	public Vector2 SwipeDelta{get{ return Swipe_Delta;}}
}
