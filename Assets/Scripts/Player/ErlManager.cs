﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Controller2D))]
public class ErlManager : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
 	public float moveSpeed = 6;
    public float jumpAnimationLength = .1f;

    public LayerMask oobMask;

    public Device weapon;

	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

    int jumpAnimationCurrent = 0;
    float jumpSmoothing;
    float jumpExtendHeight = 0.2f;
    public float currentExtension = 0f;
    bool isExtending = false;
    bool isContracting = false;


	Controller2D controller;
	Animator anim;

    public GameObject body;

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

		if (weapon != null) {
            weapon.ToggleOrEnable(gameObject, Input.GetKeyDown(KeyCode.Space), Input.GetKey(KeyCode.Space));
		}

        if (input.y > 0 && controller.collisions.below && !controller.collisions.slidingDownMaxSlope)
        {
            Jump();
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        if (isExtending)
        {
            if (currentExtension >= jumpExtendHeight - .05f)
            {
                isExtending = false;
                isContracting = true;
            }
            else
            {
                currentExtension = Mathf.SmoothDamp(currentExtension, jumpExtendHeight, ref jumpSmoothing, jumpAnimationLength);
                body.transform.localPosition = Vector3.up * currentExtension;
            }
        }
        else if (isContracting)
        {
            if (currentExtension <= 0)
            {
                isContracting = false;
            }
            else
            {
                currentExtension = Mathf.SmoothDamp(currentExtension, 0, ref jumpSmoothing, jumpAnimationLength * 3);
                body.transform.localPosition = Vector3.up * currentExtension;
            }
        }

        //anim.SetFloat ("Speed", Mathf.Abs(input.x));
    }

    void Jump()
    {
        velocity.y = jumpVelocity;
        isExtending = true;
    }
}