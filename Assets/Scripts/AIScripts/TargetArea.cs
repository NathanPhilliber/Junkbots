using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour {

	public Vector3 target;
	public float moveSpeed = 3;
	public string targetTag = "Player";

	public LayerMask collisionMask;

	// Use this for initialization
	void Start () {
		//target = GameObject.FindWithTag(targetTag).transform.position; //target the player
		GameObject[] players = new GameObject[2];
		players[0] = GameObject.FindWithTag("Erl");
		players[1] = GameObject.FindWithTag("Isa");
		int i = 0;
		float closest = 9999999;
		while (i < players.Length) {
			if (players[i] != null && Vector3.Distance(players[i].transform.position, transform.position) < closest) {
				target = players[i].transform.position;
				// Aggro scripts
				/*aggLost = Time.time + loseAggro;
				aggro = true;*/
				closest = Vector3.Distance(players[i].transform.position, transform.position);
			}
			i++;
		}

		Vector3 relPos = target - transform.position;
		Quaternion rotation = Quaternion.LookRotation (relPos);
		transform.rotation = rotation;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * moveSpeed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((collisionMask & (1 << other.gameObject.layer)) > 0)
			Destroy(gameObject);
	}
}
