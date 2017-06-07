using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IInteractable {

	public float length;		//	How long the bridge is
	public GameObject bridge;	//	The actual bridge sub-piece
	public float speed;			//	How fast the bridge moves

	public bool goRight;		//	Which direction the bridge moves in

	private int pressedTime = 0;

	void Start () {
		Reload ();
	}

	/*
	 * Resizes the sprite size and the collider size according to the bridge's length
	 */
	public void Reload(){
		bridge.GetComponent<BoxCollider2D>().size = new Vector2(length, bridge.GetComponent<BoxCollider2D>().size.y);
		bridge.GetComponent<SpriteRenderer>().size = new Vector2(length, bridge.GetComponent<SpriteRenderer>().size.y);
	}
	

	void FixedUpdate () {
		if (pressedTime > 0) {
			pressedTime--;
		} else {
			if (!goRight && bridge.transform.localPosition.x < length/2) {				//	If we need to go right, then move the bridge right
				bridge.transform.Translate(new Vector3(speed*Time.deltaTime, 0, 0));
			}
			else if (goRight && bridge.transform.localPosition.x > -length/2) {		//	If we need to go left, then move the bridge left
				bridge.transform.Translate(new Vector3(-speed*Time.deltaTime, 0, 0));
			}
		}
	}

	/*
	 * This bridge's action is to flip the move state
	 */
	public void TriggerAction(bool toggle){
		//goRight = !goRight;
		pressedTime++;
		Move();

		if (toggle) {
			goRight = !goRight;
		}
	}

	void Move(){
		if (goRight && bridge.transform.localPosition.x < length/2) {				//	If we need to go right, then move the bridge right
			bridge.transform.Translate(new Vector3(speed*Time.deltaTime, 0, 0));
		}
		else if (!goRight && bridge.transform.localPosition.x > -length/2) {		//	If we need to go left, then move the bridge left
			bridge.transform.Translate(new Vector3(-speed*Time.deltaTime, 0, 0));
		}


	}
}
