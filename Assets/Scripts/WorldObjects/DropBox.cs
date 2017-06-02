using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour, IInteractable {

	public GameObject leftDoor, rightDoor;
	public float pivotOffset;
	public float openSpeed;
	public int closeDelay;

	private Vector3 rightPos, leftPos;
	private bool isOpening = false;
	private bool isClosing = false;
	private Quaternion leftRot, rightRot;

	private float openAmount = 0;

	private int closeDelayCur = 0;

	void Start(){
		leftPos =  leftDoor.transform.position;
		rightPos =  rightDoor.transform.position;
		leftRot = leftDoor.transform.rotation;
		rightRot = rightDoor.transform.rotation;
	}

	public void Update(){

		if (closeDelayCur > 0) {
			closeDelayCur--;
		} else {
			if (isClosing) {
				leftDoor.transform.RotateAround (new Vector3 (leftPos.x + pivotOffset, leftPos.y, leftPos.z), Vector3.forward, openSpeed);
				rightDoor.transform.RotateAround (new Vector3 (rightPos.x - pivotOffset, rightPos.y, rightPos.z), Vector3.forward, -openSpeed);

				openAmount -= openSpeed;

				if (openAmount <= 0) {

					leftDoor.transform.rotation = leftRot;
					rightDoor.transform.rotation = rightRot;

					openAmount = 0;

					isClosing = false;

				}
			}
			else if (isOpening) {
				leftDoor.transform.RotateAround (new Vector3 (leftPos.x + pivotOffset, leftPos.y, leftPos.z), Vector3.forward, -openSpeed);
				rightDoor.transform.RotateAround (new Vector3 (rightPos.x - pivotOffset, rightPos.y, rightPos.z), Vector3.forward, openSpeed);

				openAmount += openSpeed;

				if (openAmount > 90) {
					closeDelayCur = closeDelay;
					isOpening = false;
					isClosing = true;
				}
			}


		}
	}



	public void TriggerAction(bool toggle){
		
		isOpening = true;
	}
}
