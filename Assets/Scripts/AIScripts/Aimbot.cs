using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimbot : FlyingController2D {

    public float accelerationTime = .1f;
    public float acceleration = .5f;
    public float maxSpeed = 15f;
	public float tooFar = 30;
    public float tooClose = 10;
    public Device weapon;

    Vector3 targetPos;

    Vector3 velocity;
    float velocitySmoothing;

    public GameObject target;
	float rayLength = 2;
    GameObject[] players = new GameObject[2];

    RotateTowards weaponJoint;

    // Use this for initialization
    new void Start () {
		base.Start ();

        players[0] = GameObject.FindWithTag("Erl");
        players[1] = GameObject.FindWithTag("Isa");

        weaponJoint = GetComponentInChildren<RotateTowards>();
    }

	// Update is called once per frame
	void Update () {
        target = null;
		float closest = tooFar;
        float distance;

        foreach (GameObject player in players) {
			if (player != null &&  (distance = Vector3.Distance(player.transform.position, transform.position)) < closest) {
				target = player;

                closest = distance;
			}
		}

        

		if (target != null) {
            weaponJoint.target = target.transform;
            weapon.Enable(gameObject);
            targetPos = Vector3.LerpUnclamped(target.transform.position, transform.position, tooClose / closest);

            Vector3 targetV = (targetPos - transform.position);
            if (targetV.magnitude > maxSpeed)
            {
                targetV = targetV.normalized * maxSpeed;
            }

            velocity = targetV;


            Move(velocity * Time.deltaTime);


		} else
        {
            weaponJoint.target = null;
            weapon.Disable(gameObject);
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        float size = .3f;

        Gizmos.DrawLine(targetPos - Vector3.up * size, targetPos + Vector3.up * size);
        Gizmos.DrawLine(targetPos - Vector3.left * size, targetPos + Vector3.left * size);
    }
}
