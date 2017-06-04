using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

    public Transform target;
    public Controller2D controller;

    Vector3 origPos;

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
                transform.right = (Vector2)transform.position - (Vector2)target.position;
            }
            else
            {
                transform.right = (Vector2)target.position - (Vector2)transform.position;
            }
        }
        else
        {
            transform.right = origPos;
        }
        
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
