using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeHover : MonoBehaviour {

	public float amount, speed;


	private Image img;
	private bool goIn = false;
	private float curAmount = 0;
	private float aspectRatio;
	private float startX;

	void Start () {
		img = GetComponent<Image> ();
		aspectRatio = img.rectTransform.rect.width / img.rectTransform.rect.height;
		startX = img.transform.localScale.x;
	}
	

	void Update () {
		if (goIn) {
			if (curAmount < amount) {
				curAmount += speed;
				img.transform.localScale = new Vector2 (img.transform.localScale.x - speed*Time.deltaTime, img.transform.localScale.y);
			} else {
				goIn = false;
				curAmount = 0;
				img.transform.localScale = new Vector2 (startX, img.transform.localScale.y);
			}

		} else{
			if (curAmount < amount) {
				curAmount += speed;
				img.transform.localScale = new Vector2 (img.transform.localScale.x + speed*Time.deltaTime, img.transform.localScale.y);
			} else {
				goIn = true;
				curAmount = 0;

			}

		}
	}
}
