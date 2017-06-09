using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeHoverBot : FlyingController2D {

	public float maxSpeed = 15f;
	public float tooFar = 50;
	public float tooClose = 10;
	public float crashSpeed = 30f;
	public float crashDelay = 3f;

	private float crashTime = 0;

	Vector3 targetPos;

	Vector3 velocity;

	public GameObject target;

	// Use this for initialization
	new void Start () {
		base.Start ();

		target = GameObject.FindWithTag("Erl");

	}

	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(target.transform.position, transform.position);

		if (distance < tooFar) {
			if (crashTime == 0) {
				crashTime = Time.time + crashDelay;
			} else if (Time.time > crashTime) {
				Vector3 targetV = target.transform.position - transform.position;
				targetV = targetV.normalized * crashSpeed;

				velocity = targetV;

				Move (velocity * Time.deltaTime);
			} else {
				targetPos = Vector3.LerpUnclamped (target.transform.position, transform.position, tooClose / distance);

				Vector3 targetV = (targetPos - transform.position);
				if (targetV.magnitude > maxSpeed) {
					targetV = targetV.normalized * maxSpeed;
				}

				velocity = targetV;


				Move (velocity * Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((collisionMask & (1 << other.gameObject.layer)) > 0)
		{
			Destroy(gameObject);
		}


	}
}
