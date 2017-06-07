using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour {

	public GameObject target;
	public float tooFar = 8;
	public float defaultSpeed = 3;
	public float moveSpeed;
	public string targetTag = "Player";
	private Vector3 prevDir = new Vector3(0,0,0);

	public LayerMask collisionMask;

	// Use this for initialization
	void Start () {
		//target = GameObject.FindWithTag(targetTag); //target the player
		moveSpeed = defaultSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Vector3.Distance (target.transform.position, transform.position) > tooFar) {
			moveSpeed = 0;
		} else {
			moveSpeed = 3;
		}*/
		//GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
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

		if (closest < tooFar / 2) {
			moveSpeed = defaultSpeed * 2;
		}

		if (target != null) {
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, moveSpeed * Time.deltaTime);
			prevDir = target.transform.position - transform.position;
			prevDir.Normalize ();
			prevDir *= moveSpeed * Time.deltaTime;
		} else {
			transform.Translate (prevDir.x,prevDir.y,prevDir.z);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((collisionMask & (1 << other.gameObject.layer)) > 0)
			Destroy(gameObject);
	}
}
