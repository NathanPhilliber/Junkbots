using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class DropOn : RaycastController {

	public GameObject Bomb;
	public float speed = 2.0f;
	private float NextFire;
	float FireRate = 1.0f;

	// Use this for initialization
	new void Start () {
		// Assumes Layer names are "Bomb" and "AI"
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bomb"),LayerMask.NameToLayer("AI"));

		base.Start ();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * speed * Time.deltaTime);
		UpdateRaycastOrigins ();

		if (Under() && Time.time > NextFire)
		{        
			//If fired, reset the NextFire time to a new point in the future.
			NextFire = Time.time + FireRate;
			GameObject bomb = Instantiate (Bomb, transform.position, Quaternion.identity);
		}
	}

	bool Under() {
		float rayLength = 15;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = raycastOrigins.bottomLeft;

			rayOrigin += Vector2.right * verticalRaySpacing * i;
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, -Vector2.up * rayLength, Color.red);

			if (hit) {
				return true;
			}
		}

		return false;
	}
}
