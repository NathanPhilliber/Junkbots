using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public float MaxHealth, Health;
	public float scaleY, scaleZ;

	// Use this for initialization
	void Start () {
		Health = MaxHealth;
		scaleY = transform.localScale.y;
		scaleZ = transform.localScale.z;
	}
	
	// Update is called once per frame
	void Update () {
		// if hit by enemy
		if ()
			DecreaseHealth();
	}

	void DecreaseHealth() {
		if (Health > 0)
			Health = Health - 5;

		float scaleX = Health / MaxHealth;
		transform.localScale = new Vector3 (scaleX, scaleY, scaleZ);
	}
}
