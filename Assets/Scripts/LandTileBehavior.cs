using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandTileBehavior : MonoBehaviour {

	public Sprite sprite;	//	The sprite for this platform

	/*
	 * Initialize sprite and collider size
	 */
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = sprite;
		GetComponent<BoxCollider2D> ().size = sprite.rect.size / sprite.pixelsPerUnit;
	}
}
