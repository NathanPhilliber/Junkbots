using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimbot : RaycastController {

	public GameObject Missile;
	public float speed = 1.25f;
	private float NextFire;
	float FireRate = 1.0f;
	public GameObject target;
	public float tooFar = 6;
	public int clockwise = 1;
	float rayLength = 2;

	// Use this for initialization
	new void Start () {
		base.Start ();
	}

	// Update is called once per frame
	void Update () {
		//transform.Translate(Vector3.left * speed * Time.deltaTime);

		GameObject[] players = new GameObject[2];
		players[0] = GameObject.FindWithTag("Erl");
		players[1] = GameObject.FindWithTag("Isa");
		int i = 0;
		float closest = tooFar;
		while (i < players.Length) {
			if (players[i] != null && Vector3.Distance(players[i].transform.position, transform.position) < closest) {
				target = players [i];
				// Aggro scripts
				/*aggLost = Time.time + loseAggro;
				aggro = true;*/
				closest = Vector3.Distance(players[i].transform.position, transform.position);
			}
			i++;
		}

		if (target != null && Vector3.Distance (target.transform.position, transform.position) < tooFar) {
			transform.RotateAround (target.transform.position, new Vector3 (0, 0, 1), speed * clockwise);
			if (Vector3.Distance (target.transform.position, transform.position) > tooFar * 3 / 5) {
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, 0.4f * Time.deltaTime);
			} else {
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, -0.4f * Time.deltaTime);
			}
			transform.rotation = Quaternion.identity;

			if (Time.time > NextFire) {
				NextFire = Time.time + FireRate;
				shoot();
			}
		}

		UpdateRaycastOrigins ();

		if (Under ()) {
			clockwise *= -1;
		}
	}

	void shoot() {
		GameObject missile = Instantiate (Missile, transform.position, Quaternion.identity);
	}

	bool Under() {
		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = raycastOrigins.bottomLeft;

			rayOrigin += Vector2.right * verticalRaySpacing * i;
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, -Vector2.up * rayLength, Color.red);

			if (hit) {
				return true;
			}
		}

		return false;
	}
}
