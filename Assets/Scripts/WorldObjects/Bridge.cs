using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour, IInteractable {

	public float length;
	public GameObject bridge;

	public float speed;

	private bool goRight;

	void Start () {
		bridge.GetComponent<BoxCollider2D>().size = new Vector2(length, bridge.GetComponent<BoxCollider2D>().size.y);
		bridge.GetComponent<SpriteRenderer>().size = new Vector2(length, bridge.GetComponent<SpriteRenderer>().size.y);
	}

	public void Reload(){
		bridge.GetComponent<BoxCollider2D>().size = new Vector2(length, bridge.GetComponent<BoxCollider2D>().size.y);
		bridge.GetComponent<SpriteRenderer>().size = new Vector2(length, bridge.GetComponent<SpriteRenderer>().size.y);
	}
	

	void FixedUpdate () {
		if (goRight && bridge.transform.localPosition.x < length/2) {
			bridge.transform.Translate(new Vector3(speed*Time.deltaTime, 0, 0));
		}
		else if (!goRight && bridge.transform.localPosition.x > -length/2) {
			bridge.transform.Translate(new Vector3(-speed*Time.deltaTime, 0, 0));
		}
	}

	public void TriggerAction(){
		goRight = !goRight;
	}
}
