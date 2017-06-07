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
		target = GameObject.FindWithTag(targetTag).transform.position; //target the player

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
