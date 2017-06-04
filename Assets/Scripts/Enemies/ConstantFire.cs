using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFire : MonoBehaviour {
    public float range;
    public Transform target;

    public GunController gun;

	
	// Update is called once per frame
	void Update () {
        if (target != null && (Vector3.Distance(target.position, transform.position) < range))
        {
            gun.Enable(gameObject); 
        }
        else
        {
            gun.Disable(gameObject);
        }
	}
}
