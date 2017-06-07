using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

	public int healthBoost;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Isa" || other.name == "Erl")
		{
			Damageable damageable = other.GetComponent<Damageable> ();

			if (damageable != null)
			{
				GiveHealth (damageable);
			}

			Destroy (gameObject, 0.0F);
		}
	}

	void GiveHealth(Damageable player)
	{
		int newHealth = player.health + healthBoost;

		if (newHealth > player.maxHealth) {
			newHealth = player.maxHealth;
		}

		player.health = newHealth;
	}
			
			
}
