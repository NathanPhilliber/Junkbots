using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimbot : MonoBehaviour {

	public GameObject Missile;
	public float speed = 2.0f;
	private float NextFire;
	float FireRate = 1.0f;
	public GameObject target;
	public float tooFar = 5;

	// Use this for initialization
	new void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * speed * Time.deltaTime);

		if (Vector3.Distance (target.transform.position, transform.position) < tooFar && Time.time > NextFire) {
			NextFire = Time.time + FireRate;
			shoot();
		}
	}

	void shoot() {
		GameObject missile = Instantiate (Missile, transform.position, Quaternion.identity);
	}
}
