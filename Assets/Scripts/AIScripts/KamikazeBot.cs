using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBot : Projectile {

	public float acceleration = 1f;
	public float maxSpeed = 50f;
	public float tooFar = 30;

	public GameObject target;
	public float rotationSpeed = .3f;
	Vector3 targetV;

	// Use this for initialization
	void Start () {
		base.Start();
		target = GameObject.FindWithTag("Erl");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Vector3.Distance (target.transform.position, transform.position) < tooFar) {
			
			if (speed < maxSpeed) {
				speed += acceleration * Time.deltaTime + 0.0001f;
			} else {
				speed = maxSpeed;
			}

			targetV = target.transform.position - transform.position;

			rb.velocity = Vector3.RotateTowards (rb.velocity, targetV, rotationSpeed, rb.velocity.magnitude);
			rb.velocity = rb.velocity.normalized * speed;

		}

		transform.right = rb.velocity;
		transform.right = Quaternion.AngleAxis (-90, Vector3.forward) * transform.right;
	}
}
