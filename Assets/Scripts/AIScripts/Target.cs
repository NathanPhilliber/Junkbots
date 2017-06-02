using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public GameObject target;
	public float tooFar = 5;
	public float moveSpeed = 3;
	public string targetTag = "Player";

	public LayerMask collisionMask;

	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag(targetTag); //target the player
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Vector3.Distance (target.transform.position, transform.position) > tooFar) {
			moveSpeed = 0;
		} else {
			moveSpeed = 3;
		}*/

		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((collisionMask & (1 << other.gameObject.layer)) > 0)
			Destroy(gameObject);
	}
}
