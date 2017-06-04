using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grabber : Device {
    public float castDistance = .2f;
    public LayerMask pickupLayer;

    public Rigidbody2D targetObject;
    Vector2 relativePos;

    public RotateTowards rotationAxis;

    void Start()
    {

        rotationAxis = GetComponentInParent<RotateTowards>();
        rotationAxis.target = null;
    }

    public override void OnDisabled(GameObject activator)
    {
        targetObject = null;
        rotationAxis.target = null;
    }

    public override void OnEnabled(GameObject activator)
    {
        var hit = Physics2D.Raycast(transform.position, -transform.up, castDistance, pickupLayer);
        if (hit)
        {
            targetObject = hit.rigidbody;
            rotationAxis.target = hit.rigidbody.transform;
            relativePos = targetObject.position - (Vector2)transform.position;
        }
    }

    public override void UpdateWhileEnabled()
    {
        if (targetObject != null)
        {
            var hit = Physics2D.Raycast(transform.position, -transform.up, castDistance, pickupLayer);
            if (hit && hit.rigidbody == targetObject)
            {
                targetObject.MovePosition((Vector2)transform.position + relativePos);
            }
            else
            {
                Disable(gameObject);
            }
            
        }
        else
        {
            Disable(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + -transform.up * castDistance);
    }
}
