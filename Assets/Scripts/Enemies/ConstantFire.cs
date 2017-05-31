using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFire : MonoBehaviour {

    public float delay;
    public float range;
    public Transform target;

    public GunController gun;

    float currentDelay = 0;

	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            if (currentDelay <= 0)
            {
                if (Vector3.Distance(target.position, transform.position) < range)
                {
                    gun.Fire();
                }
                currentDelay = delay;
            }
            else
            {
                currentDelay--;
            }
        }
	}
}
