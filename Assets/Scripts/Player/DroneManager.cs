using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (FlyingController2D))]
public class DroneManager : MonoBehaviour {

	public float accelerationTime = .1f;
 	public float acceleration = .5f;
    public float maxSpeed = 15;

    public Device primary;
    public Device secondary;

    Vector3 velocity;
	float velocitySmoothing;
	bool facingRight = true;

    private Vector2 mouse;

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

        mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 input = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 targetV = (input - (Vector2)transform.position) * acceleration;
        if (targetV.magnitude > maxSpeed)
        {
            targetV = targetV.normalized * maxSpeed;
        }
        

		velocity.x = Mathf.SmoothDamp (velocity.x, targetV.x, ref velocitySmoothing, 
			accelerationTime);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetV.y, ref velocitySmoothing,
            accelerationTime);
        velocity = targetV;

        controller.Move (velocity * Time.deltaTime, input);

        if (primary != null)
        {
            primary.ToggleOrEnable(gameObject, Input.GetMouseButtonDown(0), Input.GetMouseButton(0));
        }

        if (secondary != null)
        {
            secondary.ToggleOrEnable(gameObject, Input.GetMouseButtonDown(1), Input.GetMouseButton(1));
        }

        //anim.SetFloat ("Speed", Mathf.Abs(input.x));
    }
}
