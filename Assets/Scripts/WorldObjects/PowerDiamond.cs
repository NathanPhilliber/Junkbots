using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDiamond : MonoBehaviour {

	public GameObject pump1, pump2, pump3, pump4;
	public GameObject roomParticles;
	public GameObject endingGate;

	private SpriteRenderer renderer;
	private int darkenAmount = 20;
	private int cooldown = 4;
	private Rigidbody2D rb;
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();	
		Physics2D.IgnoreLayerCollision (8, 15);
		Physics2D.IgnoreLayerCollision (13, 15);
		rb = GetComponent<Rigidbody2D> ();
	}
	

	void Update () {
		if (pump1 == null && pump2 == null && pump3 == null && pump4 == null && cooldown-- <= 0 && darkenAmount-- > 0) {

			if (darkenAmount == 19) {
				rb.isKinematic = false;
				Destroy (roomParticles);
				endingGate.GetComponent<IInteractable>().TriggerAction (true);
			}

			renderer.color = new Color (renderer.color.r * .95f, renderer.color.g * .95f, renderer.color.b * .95f);
			cooldown = 4;

		}
	}
}
