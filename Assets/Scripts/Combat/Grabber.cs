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
    new Renderer renderer;

    int grabAttemptCooldown = 10;
    int currentCooldown;

    void Start()
    {

        rotationAxis = GetComponentInParent<RotateTowards>();
        rotationAxis.target = null;
        renderer = GetComponent<Renderer>();
        renderer.enabled = isEnabled;
        
    }

    public override void OnDisabled(GameObject activator)
    {
        targetObject = null;
        rotationAxis.target = null;
        renderer.enabled = isEnabled;
    }

    public override void OnEnabled(GameObject activator)
    {
        renderer.enabled = isEnabled;
        var hit = Physics2D.Raycast(transform.position, -transform.up, castDistance, pickupLayer);
        if (hit)
        {
            targetObject = hit.rigidbody;
            rotationAxis.target = hit.rigidbody.transform;
            relativePos = targetObject.position - (Vector2)transform.position;
        }
        else
        {
            locked = true;
            currentCooldown = grabAttemptCooldown;
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
        else if (locked)
        {    
            if (currentCooldown-- <= 0)
            {
                locked = false;
                Disable(gameObject);
            }
        }
        else
        {
            Disable(gameObject);
        }
    }

    public override void UpdateWhileDisabled()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + -transform.up * castDistance);
    }
}
