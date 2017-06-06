using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCameraX : MonoBehaviour {

	public float perXUnit;

	public Transform pivot;



	void Start () {
		
		lastX = pivot.position.x;
	}
	

	private float lastX;
	void Update () {
		float delX = pivot.position.x - lastX;
		transform.Translate (new Vector2(delX * perXUnit, 0));
		lastX = pivot.position.x;
	}
}
