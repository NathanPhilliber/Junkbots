using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableDestroy : Damageable {
    public GameObject deathEffect;

    public override void OnHealthDecreased(int amount)
    {
        // Nothing yet
    }

    public override void OnHealthZero()
    {
        if (deathEffect != null)
            GameObject.Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
