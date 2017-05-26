using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour {

	public GameObject[] objectsToTrigger;

	public string tagTrigger;

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag (tagTrigger)) {
			for (int i = 0; i < objectsToTrigger.Length; i++) {
				objectsToTrigger [i].GetComponent<IInteractable>().TriggerAction ();
			}

		}
	}
}

public interface IInteractable{
	void TriggerAction();
}
