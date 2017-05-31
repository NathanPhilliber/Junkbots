using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour {

	public Vector3 target;
	public float tooFar = 5;
	public float moveSpeed = 3;
	public string targetTag = "Player";

	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag(targetTag).transform.position; //target the player

		Vector3 relPos = target - transform.position;
		Quaternion rotation = Quaternion.LookRotation (relPos);
		transform.rotation = rotation;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime);
	}
}
