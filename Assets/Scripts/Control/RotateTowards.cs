using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour {

    public Transform target;
    public Controller2D controller;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (controller != null && !controller.facingRight)
        {
            transform.right = transform.position - target.position;
        }
        else
        {
            transform.right = target.position - transform.position;
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
