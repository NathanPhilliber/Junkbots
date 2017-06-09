using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDiamond : MonoBehaviour {

	public GameObject pump1, pump2, pump3, pump4;
	public GameObject roomParticles;
	public GameObject endingGate;

	public GameObject enemy1;

	private bool p1 = false, p2 = false, p3 = false, p4 = false;
	private int en = 0;

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

		if (!p1 && pump1 == null) {
			p1 = true;
			en++;
			if(en < 4)
				Instantiate (enemy1, transform.position, Quaternion.identity);
		}
		if (!p2 && pump2 == null) {
			p2 = true;
			en++;
			if(en < 4)
				Instantiate (enemy1, transform.position, Quaternion.identity);
		}
		if (!p3 && pump3 == null) {
			p3 = true;
			en++;
			if(en < 4)
				Instantiate (enemy1, transform.position, Quaternion.identity);
		}
		if (!p4 && pump4 == null) {
			p4 = true;
			en++;
			if(en < 4)
				Instantiate (enemy1, transform.position, Quaternion.identity);
		}

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
