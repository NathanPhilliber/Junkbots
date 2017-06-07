using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class DropOn : RaycastController {

	public GameObject Bomb;
	public float speed = 3.0f;
	private float NextFire;
	float FireRate = 1.0f;
	public GameObject target;
	// How long until aggro lost
	private float loseAggro = 2.5f;
	// Time when aggro will be lost
	private float aggLost;
	private bool aggro = false;
	private float above = 4;
	float rayLength = 15;

	public float tooFar = 5;

	// Use this for initialization
	new void Start () {
		base.Start ();
	}

	// Update is called once per frame
	void Update () {
		//GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] players = new GameObject[2];
		players[0] = GameObject.FindWithTag("Erl");
		players[1] = GameObject.FindWithTag("Isa");

		if (Time.time > aggLost) {
			aggro = false;
		} 

		Vector3 dir = Vector2.left;
		//transform.Translate(Vector3.left * speed * Time.deltaTime);
		int i = 0;
		float closest = tooFar;
		while (i < players.Length) {
			if (players[i] != null && Math.Abs(players[i].transform.position.x - transform.position.x) < closest) {
				target = players [i];
				aggLost = Time.time + loseAggro;
				aggro = true;
				closest = Math.Abs (players [i].transform.position.x - transform.position.x);
			}
			i++;
		}

		/*if (!aggro) {
			speed = 0;
		} else */if (aggro && target != null) {
			//speed = 2;
			/*
			dir = new Vector3 (target.transform.position.x - transform.position.x, 
			 target.transform.position.y - transform.position.y + above, 0);
			dir.Normalize ();*/
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0,above,0), speed * Time.deltaTime);
		}
		//transform.Translate(dir * speed * Time.deltaTime);

		UpdateRaycastOrigins ();

		if (Under() && Time.time > NextFire)
		{        
			//If fired, reset the NextFire time to a new point in the future.
			NextFire = Time.time + FireRate;
			GameObject bomb = Instantiate (Bomb, transform.position, Quaternion.identity);
		}
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
