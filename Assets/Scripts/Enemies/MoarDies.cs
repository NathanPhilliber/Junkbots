using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoarDies : MonoBehaviour, IInteractable {

	public GameObject[] explosions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TriggerAction(bool toggle) {
		if (toggle) {
			for (int i = 0; i < explosions.Length; i++) {
				
			}
		}
	}
}
