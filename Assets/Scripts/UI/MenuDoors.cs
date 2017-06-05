using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDoors : MonoBehaviour {

	public bool open = false;
	public bool close = false;

	public float speed, openWidth;

	public GameObject leftDoor, rightDoor;

	private float leftStartX, rightStartX;
	private float curWidth = 0;

	private bool isOpening = false;
	private bool isClosing = false;

	// Use this for initialization
	void Start () {
		leftStartX = leftDoor.transform.position.x;
		rightStartX = rightDoor.transform.position.x;
	}

	public void Close(){
		close = true;
	}
	public void Open(){
		open = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (open) {
			open = false;
			isOpening = true;
			isClosing = false;
		}

		if (close) {
			close = false;
			isClosing = true;
			isOpening = false;
		}

		if (isOpening) {
			leftDoor.transform.Translate (new Vector2(-speed, 0));
			rightDoor.transform.Translate (new Vector2(-speed, 0));

			curWidth += speed;

			if (curWidth > openWidth) {
				isOpening = false;
			}
		}

		if (isClosing) {
			leftDoor.transform.Translate (new Vector2(speed, 0));
			rightDoor.transform.Translate (new Vector2(speed, 0));

			curWidth -= speed;

			if (curWidth <= 0) {
				leftDoor.transform.position = new Vector2 (leftStartX, leftDoor.transform.position.y);
				rightDoor.transform.position = new Vector2 (rightStartX, rightDoor.transform.position.y);
				isClosing = false;
			}
		}
	}
}
