using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour {

	public GameObject[] objectsToTrigger;	//	The things that are affected by this button
	public LayerMask mask;				//	Objects with this layermask can interact with this button
	public bool toggle;

	public Sprite unpressed, pressed;

	public SpriteRenderer erlIcon, isaIcon, physicsIcon;

	private SpriteRenderer renderer;
	private SoundManager sounds;
	void Start(){
		renderer = GetComponent<SpriteRenderer> ();
		sounds = Camera.main.GetComponent<SoundManager> ();

		if (mask != (mask | (1 << LayerMask.NameToLayer("Erl")))) {
			erlIcon.enabled = false;
		}
		if (mask != (mask | (1 << LayerMask.NameToLayer("Isa")))) {
			isaIcon.enabled = false;
		}
		if (mask != (mask | (1 << LayerMask.NameToLayer("PhysicsObjects")))) {
			physicsIcon.enabled = false;
		}
	}

	/*
	 * Called when this button is pressed
	 */
	void OnTriggerEnter2D(Collider2D other){

		if (mask == (mask | (1 << other.gameObject.layer))) {
			if (Vector3.Distance (Camera.main.transform.position, transform.position) < 80) {
				sounds.PlaySound (5);
			}
		}

		if (toggle == true && mask == (mask | (1 << other.gameObject.layer))) {
			for (int i = 0; i < objectsToTrigger.Length; i++) {
				objectsToTrigger [i].GetComponent<IInteractable>().TriggerAction (true);
			}

			if (renderer.sprite == unpressed) {
				renderer.sprite = pressed;
			} else {
				renderer.sprite = unpressed;
			}
		}

	}

	void OnTriggerStay2D(Collider2D other){
		
		if (toggle == false && mask == (mask | (1 << other.gameObject.layer))) {
			for (int i = 0; i < objectsToTrigger.Length; i++) {
				objectsToTrigger [i].GetComponent<IInteractable>().TriggerAction (false);
			}

			renderer.sprite = pressed;

		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (toggle == false && mask == (mask | (1 << other.gameObject.layer))) {

			renderer.sprite = unpressed;
			if (Vector3.Distance (Camera.main.transform.position, transform.position) < 80) {
				sounds.PlaySound (6);
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
