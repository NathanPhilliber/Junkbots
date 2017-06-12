using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (FlyingController2D))]
public class IsaManager : MonoBehaviour {

	public float accelerationTime = .1f;
 	public float acceleration = .5f;
    public float maxSpeed = 15;

    public Device primary;
    public Device secondary;
    ToggleIsaCam isaControlToggle;

    Vector3 velocity;
	float velocitySmoothing;
	bool facingRight = true;

    private Vector2 mouse;

	FlyingController2D controller;
	Animator anim;

    Transform leftThruster;
    Transform rightThruster;
    Transform leftEye;
    Transform rightEye;

	// Use this for initialization
	void Start () {
		controller = GetComponent<FlyingController2D> ();

        leftThruster = transform.Find("Wing/LeftThruster");
        rightThruster = transform.Find("Wing/RightThruster");
        leftEye = transform.Find("Head/LeftEye");
        rightEye = transform.Find("Head/RightEye");

        isaControlToggle = GetComponent<ToggleIsaCam>();
        //anim = GetComponent<Animator> ();
    }
	
	// Update is called once per frame
	void Update () {

        
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 input = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 targetV = (input - (Vector2)transform.position) * acceleration;

        if (!isaControlToggle.isEnabled)
        {
            targetV = Vector2.zero;
        }

            if (targetV.magnitude > maxSpeed)
        {
            targetV = targetV.normalized * maxSpeed;
        }


        velocity.x = Mathf.SmoothDamp(velocity.x, targetV.x, ref velocitySmoothing,
            accelerationTime);
        velocity.y = Mathf.SmoothDamp(velocity.y, targetV.y, ref velocitySmoothing,
            accelerationTime);
        velocity = targetV;

        controller.Move(velocity * Time.deltaTime, input);

        if (primary != null)
        {
            primary.ToggleOrEnable(gameObject, Input.GetButtonDown("Grabber_Isa"), Input.GetButton("Grabber_Isa"));
        }

        if (secondary != null)
        {
            secondary.ToggleOrEnable(gameObject, Input.GetButtonDown("Shield_Isa"), Input.GetButton("Shield_Isa"));
        }
        

        if (Input.GetKeyDown(KeyCode.LeftControl) && isaControlToggle != null)
        {
            isaControlToggle.Toggle(gameObject);
        }

        float thrusterRotation = velocity.x / maxSpeed;

        leftThruster.up = new Vector2(thrusterRotation, 1 - Mathf.Abs(thrusterRotation));
        rightThruster.up = new Vector2(thrusterRotation, 1 - Mathf.Abs(thrusterRotation));
    }
}
