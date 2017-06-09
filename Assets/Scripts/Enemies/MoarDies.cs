using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoarDies : MonoBehaviour, IInteractable {

	public GameObject[] explosions;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < explosions.Length; i++) {
			print (i);
			ParticleSystem exp = explosions [i].GetComponent<ParticleSystem>();
			if (exp != null) {
				print (exp.IsAlive ());
				print (exp.isEmitting);
			}		
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TriggerAction(bool toggle) {
		print ("toggle: " + toggle);

		if (toggle) {
			for (int i = 0; i < explosions.Length; i++) {
				ParticleSystem exp = explosions [i].GetComponent<ParticleSystem>();
				if (exp != null) {
					exp.Play ();
					print ("playing explosion");
				}
			}
		}
	}
}
