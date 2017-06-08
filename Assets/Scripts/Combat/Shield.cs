using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Damageable))]
public class Shield : Device {

    public int maxCharge = 1000;
    public int drainRate = 2;
    public int rechargeRate = 1;

    public int currentCharge = 60;


    Collider2D parentCollider;
    new Collider2D collider;
    new Renderer renderer;
    Damageable damageable;
    

    // Use this for initialization
    void Start () {
        parentCollider = transform.root.GetComponentInChildren<Collider2D>();
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();
        damageable = GetComponentInParent<Damageable>();

        float ratioX = parentCollider.bounds.extents.x / collider.bounds.extents.x;
        float ratioY = parentCollider.bounds.extents.y / collider.bounds.extents.y;

        float ratio = ratioX + ratioY;

        transform.localScale = new Vector3(ratio, ratio, ratio);
        isToggle = true;
        collider.enabled = isEnabled;
        renderer.enabled = isEnabled;
    }
	

    void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        
        if (projectile != null)
        {
            Damager damager = other.GetComponent<Damager>();
            if (damager!= null)
            {
                if ((damager.victimMask & (1 << gameObject.layer)) != 0)
                    Destroy(other);
            }
           
        }

    }

    public override void OnEnabled(GameObject activator)
    {
        damageable.defense += 1000;
        collider.enabled = isEnabled;
        renderer.enabled = isEnabled;
    }

    public override void OnDisabled(GameObject activator)
    {
        damageable.defense -= 1000;
        collider.enabled = isEnabled;
        renderer.enabled = isEnabled;
    }

    public override void UpdateWhileEnabled()
    {
        currentCharge -= drainRate;
        if (currentCharge <= 0)
        {
            currentCharge = 0;
            Disable(gameObject);
        }
    }

    public override void UpdateWhileDisabled()
    {
        if (currentCharge < maxCharge)
        {
            currentCharge += rechargeRate;
            if (currentCharge > maxCharge)
                currentCharge = maxCharge;
        }
        
    }
}
