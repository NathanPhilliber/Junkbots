using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour {

	public GameObject[] objectsToTrigger;	//	The things that are affected by this button
	public string tagTrigger;				//	Objects with this tag can interact with this button

	/*
	 * Called when this button is pressed
	 */
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag (tagTrigger)) {
			for (int i = 0; i < objectsToTrigger.Length; i++) {
				objectsToTrigger [i].GetComponent<IInteractable>().TriggerAction ();
			}

		}
	}
}

/*
 * Anything that works with buttons should have this interface
 */
public interface IInteractable{
	void TriggerAction();	//	Called on button press
}
