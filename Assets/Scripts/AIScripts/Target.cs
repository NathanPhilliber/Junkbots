using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public GameObject target;
	public float tooFar = 5;
	public float moveSpeed = 3;

	// Use this for initialization
	void Start () {
		//target = GameObject.FindWithTag("Player").transform; //target the player
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
}
