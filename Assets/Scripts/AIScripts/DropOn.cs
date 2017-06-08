using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class DropOn : FlyingController2D {

	public GameObject Bomb;
    public float bombingHeight = 10;
	public float maxSpeed = 10f;
	private float NextFire;
	float FireRate = 1.0f;
	GameObject target;
	float rayLength;

	public float tooFar = 50;

    public Vector3 velocity;
    Vector3 targetPos;

    // Use this for initialization
    new void Start () {
		base.Start ();

        target = GameObject.FindWithTag("Erl");

        rayLength = bombingHeight * 1.5f;
    }

	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < tooFar)
        {

            targetPos = target.transform.position + Vector3.up * bombingHeight;

            Vector3 targetV = (targetPos - transform.position);
            if (targetV.magnitude > maxSpeed)
            {
                targetV = targetV.normalized * maxSpeed;
            }

            velocity = targetV;


            Move(velocity * Time.deltaTime);

        }

        if (/*Under() && Time.time > NextFire*/ true)
		{        
			//If fired, reset the NextFire time to a new point in the future.
			NextFire = Time.time + FireRate;
			GameObject bomb = Instantiate (Bomb, transform.position, Quaternion.identity);
		}
	}

	bool Under() {
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        float size = .3f;

        Gizmos.DrawLine(targetPos - Vector3.up * size, targetPos + Vector3.up * size);
        Gizmos.DrawLine(targetPos - Vector3.left * size, targetPos + Vector3.left * size);
    }
}
