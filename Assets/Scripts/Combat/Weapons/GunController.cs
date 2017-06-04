using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : Device {

    public Transform barrelEnd;
    public int fireDelay = 20;

    public Projectile projectile;

    int currentDelay = 0;


    private float angleMod;

    // Use this for initialization
    void Start ()
    {

	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        float size = .3f;

        Gizmos.DrawLine(barrelEnd.position - Vector3.up * size, barrelEnd.position + Vector3.up * size);
        Vector3 barrelAim = new Vector3(size * 3 * Mathf.Cos((90 +barrelEnd.eulerAngles.z) * Mathf.Deg2Rad),
                                        size * 3 * Mathf.Sin((90 + barrelEnd.eulerAngles.z) * Mathf.Deg2Rad));
        Gizmos.DrawLine(barrelEnd.position, barrelEnd.position + barrelAim);
    }

    public override void OnEnabled(GameObject activator)
    {
        currentDelay = 0;
    }

    public override void OnDisabled(GameObject activator)
    {
        
    }

    public override void UpdateWhileEnabled()
    {
        if (currentDelay-- <= 0)
        {
            
            Projectile newProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation);
            currentDelay = fireDelay;
        }
    }
}
