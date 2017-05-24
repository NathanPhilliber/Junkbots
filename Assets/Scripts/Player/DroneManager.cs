using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (FlyingController2D))]
public class DroneManager : MonoBehaviour {

	public float accelerationTime = .1f;
 	public float moveSpeed = 10;

	Vector3 velocity;
	float velocitySmoothing;
	bool facingRight = true;

	FlyingController2D controller;
	Animator anim;

	// Use this for initialization
	void Start () {
		controller = GetComponent<FlyingController2D> ();
		//anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 input = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 targetV = (input - (Vector2)transform.position) * moveSpeed;

        

		velocity.x = Mathf.SmoothDamp (velocity.x, targetV.x, ref velocitySmoothing, 
			accelerationTime);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetV.y, ref velocitySmoothing,
            accelerationTime);
        velocity = targetV;

        controller.Move (velocity * Time.deltaTime, input);

		if (targetV.x > 0 && !facingRight) {
			Flip ();
		}
		else if (targetV.x < 0 && facingRight) {
			Flip ();
		}

		//anim.SetFloat ("Speed", Mathf.Abs(input.x));
	}

	void Flip() {
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
