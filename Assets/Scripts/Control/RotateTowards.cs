using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

    public Transform target;
    public Controller2D controller;

    Vector3 origPos;

    public float offset = 0;

    public bool limit = false;
    public float from;
    public float to;

    // Use this for initialization
    void Start () {
        origPos = transform.right;
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            if (controller != null && !controller.facingRight)
            {
                transform.right = ((Vector2)transform.position - (Vector2)target.position);
                
            }
            else
            {
                transform.right = ((Vector2)target.position - (Vector2)transform.position);

            }
            transform.right = Quaternion.AngleAxis(offset, Vector3.forward) * transform.right;

        }
        else
        {
            transform.right = origPos;
        }

        if (limit)
        {
            transform.right = DegreeToVector2(ClampAngle(Vector2ToDegree(transform.right), from, to));
        }
        
    }


    float ClampAngle(float angle, float from, float to)
    {
        if (angle > 180) angle = 360 - angle;
        angle = Mathf.Clamp(angle, from, to);
        if (angle < 0) angle = 360 + angle;


        return angle;
    }

    public Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public float Vector2ToRadian(Vector2 vector)
    {
        return Mathf.Atan(vector.y / vector.x) * Mathf.Sign(vector.x);
    }

    public float Vector2ToDegree(Vector2 vector)
    {
        return Vector2ToRadian(vector) * Mathf.Rad2Deg;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float size = .3f;


        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position - Vector3.up * size, transform.position + Vector3.up * size);
        Vector3 aim = new Vector3(Mathf.Cos((transform.eulerAngles.z) * Mathf.Deg2Rad),
                                        Mathf.Sin((transform.eulerAngles.z) * Mathf.Deg2Rad));
        Gizmos.DrawLine(transform.position, transform.position + aim * size * 3);
    }
}
