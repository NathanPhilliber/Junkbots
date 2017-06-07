using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBallBehavior : MonoBehaviour {

	public float xVel, yVel;
	public float maxDistance;
	public GameObject laserBox;

	private float distance = 0;

	private float xVelStart, yVelStart;

	// Use this for initialization
	void Start () {
		xVelStart = xVel;
		yVelStart = yVel;
	}

	void FixedUpdate(){
		if (distance < maxDistance) {
			transform.Translate (new Vector2 (xVel, yVel));
			distance += xVel + yVel;
		} else if(xVel != 0 || yVel != 0){
			Stop ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == 10 && other.gameObject != laserBox) {
			Stop ();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.layer == 10 && other.gameObject != laserBox) {
			xVel = xVelStart;
			yVel = yVelStart;
		}
	}

	public void Deactivate(){
		Destroy (gameObject);
	}

	void Stop(){
		xVel = 0;
		yVel = 0;
	}
}
