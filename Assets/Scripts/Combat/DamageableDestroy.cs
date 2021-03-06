﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableDestroy : Damageable {

    public GameObject damageEffect;
	public float damageRotation;

    public override void OnHealthDecreased(int amount)
    {
		if (damageEffect != null) {
			GameObject effect = (GameObject)Instantiate (damageEffect, new Vector2 (transform.position.x, transform.position.y), Quaternion.Euler(0,0, damageRotation));
			Destroy (effect, 1);
		}
    }

    public override void OnHealthZero()
    {
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
