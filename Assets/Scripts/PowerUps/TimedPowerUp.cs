using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPowerUp : MonoBehaviour {
	public bool isActive;
	public float activeTimer;

	// Use this for initialization
	void Start () {
		isActive = false;
		activeTimer = 10.0F;
	}

	// Update is called once per frame
	void Update () {
		if (isActive && activeTimer > 0) {
			activeTimer -= Time.deltaTime;
		}
	}

	void OnTriggerEvent2D(Collider2D other) {
		if (other.name == "Isa" || other.name == "Erl") {
			isActive = true;
			Destroy (gameObject, 0.5F);
		}
	}
}
