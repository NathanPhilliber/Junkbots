using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class PlayerManager : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
 	public float moveSpeed = 6;

    public GunController weapon;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;


	Controller2D controller;
	Animator anim;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
		//anim = GetComponent<Animator> ();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	
	// Update is called once per frame
	void Update () {

		if((controller.collisions.above || controller.collisions.below) && !controller.collisions.slidingDownMaxSlope) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below && !controller.collisions.slidingDownMaxSlope) {
			velocity.y = jumpVelocity;
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, 
			(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);

		if (input.x > 0 && !controller.facingRight) {
			controller.Flip ();
		}
		else if (input.x < 0 && controller.facingRight) {
			controller.Flip ();
		}

        if (Input.GetMouseButtonDown(0) && weapon != null)
        {
            weapon.Fire();
        }

		//anim.SetFloat ("Speed", Mathf.Abs(input.x));
	}

	
}
