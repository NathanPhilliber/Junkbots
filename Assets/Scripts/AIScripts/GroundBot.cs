using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBot : Controller2D {

    public float accelerationTime = .1f;
    public float acceleration = .5f;
    public float maxSpeed = 15f;
    public float tooFar = 30;
    public float tooClose = 10;
    public Device weapon;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;


    float gravity;
    float jumpVelocity;

    Vector3 targetPos;

    Vector3 velocity;
    float velocitySmoothing;

    GameObject target;
    float rayLength = 2;

    RotateTowards weaponJoint;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        target = GameObject.FindWithTag("Isa");

        weaponJoint = GetComponentInChildren<RotateTowards>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if ((collisions.above || collisions.below) && !collisions.slidingDownMaxSlope)
        {
            velocity.y = 0;
        }

        if ((collisions.left || collisions.right) && !collisions.slidingDownMaxSlope && collisions.below){
            Jump();
        }

        if (distance < tooFar)
        {
            weaponJoint.target = target.transform;
            weapon.Enable(gameObject);
            targetPos = target.transform.position;

            Vector3 targetV = (targetPos - transform.position);

            if (targetV.magnitude > maxSpeed)
            {
                targetV = targetV.normalized * maxSpeed;
            }

            velocity.x = targetV.x;
            velocity.y += gravity * Time.deltaTime;

            Move(velocity * Time.deltaTime);

            if (targetPos.x > transform.position.x && !facingRight)
            {
                Flip();

            }
            else if (targetPos.x < transform.position.x && facingRight)
            {
                Flip();
            }
        }
        else
        {
            weaponJoint.target = null;
            weapon.Disable(gameObject);
        }
    }

    void Jump()
    {
        //print("JUMMMMPP");
        velocity.y = jumpVelocity;
    }
}
