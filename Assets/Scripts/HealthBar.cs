using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public float scaleY, scaleZ;
	public Health health;

	// Use this for initialization
	void Start () {
		scaleY = transform.localScale.y;
		scaleZ = transform.localScale.z;
	}
	
	// Update is called once per frame
	void Update () {
		float scaleX = (float) health.health / (float) health.maxHealth;
		transform.localScale = new Vector3 (scaleX, scaleY, scaleZ);
	}
}
