using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour {

	public GameObject[] objectsToTrigger;	//	The things that are affected by this button
	public LayerMask mask;				//	Objects with this layermask can interact with this button
	public bool toggle;

	/*
	 * Called when this button is pressed
	 */
	void OnTriggerEnter2D(Collider2D other){
		if (toggle == true && mask == (mask | (1 << other.gameObject.layer))) {
			for (int i = 0; i < objectsToTrigger.Length; i++) {
				objectsToTrigger [i].GetComponent<IInteractable>().TriggerAction (true);
			}

		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (toggle == false && mask == (mask | (1 << other.gameObject.layer))) {
			for (int i = 0; i < objectsToTrigger.Length; i++) {
				objectsToTrigger [i].GetComponent<IInteractable>().TriggerAction (false);
			}

		}
	}
}

/*
 * Anything that works with buttons should have this interface
 */
public interface IInteractable{
	void TriggerAction(bool toggle);	//	Called on button press
}
