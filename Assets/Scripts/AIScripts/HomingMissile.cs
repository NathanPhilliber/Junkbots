using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Projectile {

    public GameObject target;
    public float rotationSpeed = .1f;
    Vector3 targetV;


	// Use this for initialization
	void Start () {
        base.Start();
		target = GameObject.FindWithTag("Isa");
		Destroy (gameObject, 10);
    }

	// Update is called once per frame
	void Update () {

		if (target != null) {
			targetV = target.transform.position - transform.position;

			rb.velocity = Vector3.RotateTowards (rb.velocity, targetV, rotationSpeed, rb.velocity.magnitude);
			rb.velocity = rb.velocity.normalized * speed;
			transform.right = rb.velocity;
			transform.right = Quaternion.AngleAxis (-90, Vector3.forward) * transform.right;
		}
    }
}
