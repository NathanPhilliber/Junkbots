using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

	public float healthBoost;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(BoxCollider2D other) {
		if (other.name == "Isa" || other.name == "Erl") {
			Destroy (gameObject, 0.5F);
		}
	}
}
